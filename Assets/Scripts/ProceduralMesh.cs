using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralMesh : MonoBehaviour
{
    private Mesh mesh;
    private List<Vector3> vertices;
    private List<int> triangles;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        MakeBuilding();
        UpdateMesh();
    }


    void MakeBuilding()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 6; i++)
        {
            MakeFace(i);
        }
    }


    void MakeFace(int _dir)
    {
        vertices.AddRange(CubeMeshData.FaceVertices(_dir));

        int v_count = vertices.Count;

        triangles.Add(v_count - 4);
        triangles.Add(v_count - 4 + 1);
        triangles.Add(v_count - 4 + 2);
        triangles.Add(v_count - 4);
        triangles.Add(v_count - 4 + 2);
        triangles.Add(v_count - 4 + 3);
    }


    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
}
