using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ClayInteract : MonoBehaviour 
{
	public float sculpt_strength;

	private MeshFilter mf;
	private Mesh mesh;
	private Vector3[] verts;

	private Camera cam_main;

	private void Start()
	{
		mf = GetComponent<MeshFilter>();
		mesh = mf.mesh;
		verts = mesh.vertices;

		cam_main = Camera.main;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			RaycastHit hit;
			Vector3 sculpt_pos = Vector3.zero;
			Debug.DrawRay(cam_main.transform.position, cam_main.transform.forward * 10f, Color.red);
			if (Physics.Raycast(cam_main.transform.position, cam_main.transform.forward, out hit))
			{
				sculpt_pos = (hit.point - cam_main.transform.forward).normalized;

			}

			for (int i = 0; i < verts.Length; i++)
			{
				Vector3 world_pos = transform.localToWorldMatrix.MultiplyPoint3x4(verts[i]);
				if (Vector3.Distance(sculpt_pos, world_pos) <= 1f)
				{
					verts[i] += sculpt_pos.normalized * Time.deltaTime * .01f * sculpt_strength;
				}
				mesh.vertices = verts;
				mesh.RecalculateNormals();
			}

		}
	}
}
