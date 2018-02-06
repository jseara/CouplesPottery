using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Clay : MonoBehaviour
{
    private Vector3[] current_verts;
    private Queue<Vector3[]> vert_queue;

    private void Start()
    {
        vert_queue = new Queue<Vector3[]>();
    }

    public void SetUp(Vector3[] verts)
    {
        current_verts = verts.Clone() as Vector3[];
    }

    public Vector3[] NextTurn(Vector3[] verts)
    {
        vert_queue.Enqueue(verts.Clone() as Vector3[]);
        if (vert_queue.Count > 1)
        { 
            Vector3[] old_verts = vert_queue.Dequeue();
            for (int i = 0; i < current_verts.Length; i++)
            {
                current_verts[i] = (current_verts[i] + old_verts[i]) / 2;
            }
            return current_verts;
        }
        else
        {
            return current_verts;
        }
    }
}
