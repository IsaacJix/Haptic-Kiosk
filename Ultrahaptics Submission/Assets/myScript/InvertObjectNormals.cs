using UnityEngine;
using System.Collections;

public class InvertObjectNormals : MonoBehaviour
{
    public GameObject FlipObjectNormals;

    void Awake()
    {
        if (FlipObjectNormals == null)
        {
            FlipObjectNormals = this.gameObject;
        }

        InvertSphere();
    }

    void InvertSphere()
    {
        Vector3[] normals = FlipObjectNormals.GetComponent<MeshFilter>().sharedMesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        FlipObjectNormals.GetComponent<MeshFilter>().sharedMesh.normals = normals;

        int[] triangles = FlipObjectNormals.GetComponent<MeshFilter>().sharedMesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int t = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = t;
        }

        FlipObjectNormals.GetComponent<MeshFilter>().sharedMesh.triangles = triangles;
    }
}