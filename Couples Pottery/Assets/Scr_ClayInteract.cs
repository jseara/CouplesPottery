using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ClayInteract : MonoBehaviour 
{
	public float sculpt_strength;
    public int sculpt_size;

	private MeshFilter mf;
	private Mesh mesh;
    private Vector3[] verts;
    private Color[] colors;

	private Camera cam_main;
    List<int> soft_selects = new List<int>();
    List<float> select_distances = new List<float>();


    private void Start()
	{
		mf = GetComponent<MeshFilter>();
		mesh = mf.mesh;
		verts = mesh.vertices;
        colors = new Color[verts.Length];

		cam_main = Camera.main;

    }

    private void Update()
	{
		Vector3 move_x = cam_main.transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * 10;
		Vector3 move_y = cam_main.transform.up * Input.GetAxis("Vertical") * Time.deltaTime * 10;
		cam_main.transform.position = Vector3.Lerp(cam_main.transform.position, cam_main.transform.position + move_x + move_y, 2);
        if (Input.GetKeyDown(KeyCode.Space))
		{
            Vector3 sculpt_pos = Input.mousePosition;
            sculpt_pos.x = (sculpt_pos.x - (Screen.width / 2)) / Screen.width;
            sculpt_pos.y = (sculpt_pos.y - (Screen.height / 2)) / Screen.height;
            Vector3 sculpt_distance = (sculpt_pos.x * cam_main.transform.right) + (sculpt_pos.y * cam_main.transform.up);
            sculpt_distance = transform.position + (sculpt_distance);

            select_distances.Clear();
            soft_selects.Clear() ;
            for (int i = 0; i < verts.Length; i++)
			{
				Vector3 world_pos = transform.localToWorldMatrix.MultiplyPoint3x4(verts[i]);
                float distance = Vector3.Distance(sculpt_distance, world_pos);

                if (soft_selects.Count < sculpt_size)
                {
                    soft_selects.Add(i);
                    select_distances.Add(distance);
                }
                else
                {
                    for (int j = 0; j < soft_selects.Count; j++)
                    {
                        if (distance < select_distances[j])
                        {
                            select_distances[j] = distance;
                            soft_selects[j] = i;
                            break;
                        }
                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 sculpt_pos = Input.mousePosition;
            sculpt_pos.x = (sculpt_pos.x - (Screen.width / 2)) / Screen.width;
            sculpt_pos.y = (sculpt_pos.y - (Screen.height / 2)) / Screen.height;

            for(int i = 0; i < soft_selects.Count; i++)
            {
                verts[soft_selects[i]] += sculpt_pos.x * cam_main.transform.right * Time.deltaTime * .01f * sculpt_strength * (1/select_distances[i]);
                verts[soft_selects[i]] += sculpt_pos.y * cam_main.transform.up * Time.deltaTime * .01f * sculpt_strength * (1/select_distances[i]);
                colors[soft_selects[i]] = Color.red;
            }
            mesh.vertices = verts;
            mesh.colors = colors;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mf.mesh = mesh;
        }
    }
    private void LateUpdate()
    {
        cam_main.transform.LookAt(transform.position);
    }
}
