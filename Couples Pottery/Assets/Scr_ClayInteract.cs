using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ClayInteract : MonoBehaviour 
{
	public float sculpt_strength;
    public float sculpt_size;

    private Scr_Clay clay;
	private MeshFilter mf;
    private MeshCollider mc;
    private Mesh mesh;
    private Vector3[] start_verts;
    private Vector3[] verts;
    private Vector3[] vert_speeds;
    private Vector3 start_pos;
    private Quaternion start_rot;
    private Renderer rend;

    private Camera cam_main;
    List<int> soft_selects = new List<int>();
    List<float> select_distances = new List<float>();


    private void Start()
	{
        Time.timeScale = 0;
        clay = GetComponent<Scr_Clay>();
		mf = GetComponent<MeshFilter>();
        mc = GetComponent<MeshCollider>();
		mesh = mf.mesh;
        start_verts = mesh.vertices;
        verts = mesh.vertices;
        vert_speeds = new Vector3[verts.Length];

		cam_main = Camera.main;
        start_pos = cam_main.transform.position;
        start_rot = cam_main.transform.rotation;
        clay.SetUp(verts);

        rend = GetComponent<Renderer>();
    }

    private void Update()
	{
        if (Time.timeScale != 0)
        {
            Vector3 move_x = cam_main.transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * 10;
            Vector3 move_y = cam_main.transform.up * Input.GetAxis("Vertical") * Time.deltaTime * 10;
            cam_main.transform.position = Vector3.Lerp(cam_main.transform.position, cam_main.transform.position + move_x + move_y, 2);


            if (Input.GetMouseButton(0))
            {
                Sculpt(1);
            }
            if (Input.GetMouseButton(1))
            {
                Sculpt(-1);
            }

            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                verts = clay.NextTurn(mesh.vertices);
                mesh.vertices = verts;
                mesh.RecalculateNormals();
                mesh.RecalculateBounds();
                cam_main.transform.position = start_pos;
                cam_main.transform.rotation = start_rot;
                Scr_UI.ChangeSeat();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                sculpt_size = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                sculpt_size = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                sculpt_size = 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                sculpt_size = 4;
            }
        }
    }
    private void LateUpdate()
    {
        cam_main.transform.LookAt(transform.position);
    }

    private void Sculpt(int sculpt_direction)
    {
        RaycastHit hit;
        Ray sculpt_ray = cam_main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(sculpt_ray, out hit))
        {
            Vector3 point = hit.point;
            point += hit.normal * .1f;
            Matrix4x4 localToWorld = transform.localToWorldMatrix;
            for (int i = 0; i < verts.Length; i++)
            {
                Vector3 vert = localToWorld.MultiplyPoint3x4(verts[i]);
                Vector3 pointToVertex = vert - point;
                float sculpt_modifier = 0;
                if (pointToVertex.sqrMagnitude < 7f + (sculpt_size - 1))
                {
                    sculpt_modifier = sculpt_strength;
                }
                float attenuatedForce = (sculpt_modifier) / (1f + (pointToVertex.sqrMagnitude));

                if (sculpt_direction == 1)
                {
                    float velocity = attenuatedForce * Time.deltaTime;
                    Vector3 displacement = start_verts[i] - verts[i];
                    if (displacement.magnitude < 1f)
                    {
                        if (displacement.magnitude * Mathf.Sign(displacement.magnitude) <= .5f)
                        {
                            displacement = pointToVertex.normalized * velocity;
                        }
                        Vector3 position = displacement.normalized * velocity;
                        verts[i] += position * Time.deltaTime;
                    }
                }
                else
                {
                    float velocity = attenuatedForce * Time.deltaTime;
                    Vector3 displacement = verts[i] - start_verts[i];
                    if (displacement.magnitude < 1f)
                    {
                        if (displacement.magnitude * Mathf.Sign(displacement.magnitude) <= .5f)
                        {
                            displacement = pointToVertex.normalized * velocity;
                        }
                        Vector3 position = displacement.normalized * velocity;
                        verts[i] -= position * Time.deltaTime;
                    }
                }

            }
            mesh.vertices = verts;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
    }
}
