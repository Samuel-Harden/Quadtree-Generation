using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    [SerializeField] GameObject block_mesh_component;
    [SerializeField] GameObject building_root;


    public GameObject GenerateBuilding(Vector3 _parent_pos, float size_x, float size_z, float _scale)
    {
        var build_block_one = Instantiate(block_mesh_component, Vector3.zero, Quaternion.identity);
        var build_block_two = Instantiate(block_mesh_component, Vector3.zero, Quaternion.identity);

        var building_base = Instantiate(building_root, Vector3.zero, Quaternion.identity);

        building_base.AddComponent<BoxCollider>();
        building_base.GetComponent<BoxCollider>().size = new Vector3(size_x, 1, size_z);

        MeshFilter filter_one = build_block_one.gameObject.GetComponent<MeshFilter>();
        MeshFilter filter_two = build_block_two.gameObject.GetComponent<MeshFilter>();

        Mesh mesh_1 = filter_one.mesh;
        Mesh mesh_2 = filter_two.mesh;

        mesh_1.Clear();
        mesh_2.Clear();

        float length = size_x;// 1.0f;
        float width = size_z;// 1.0f;

        int size = Random.Range(0, 3);

        float height = size_x + size_z / 10 * Random.Range(10, 40);


        #region Vertices
        Vector3 pos_0 = new Vector3(-width * 0.5f, -height * 0.5f, length * 0.5f);
        Vector3 pos_1 = new Vector3(width * 0.5f, -height * 0.5f, length * 0.5f);
        Vector3 pos_2 = new Vector3(width * 0.5f, -height * 0.5f, -length * 0.5f);
        Vector3 pos_3 = new Vector3(-width * 0.5f, -height * 0.5f, -length * 0.5f);

        Vector3 pos_4 = new Vector3(-width * 0.5f, height * 0.5f, length * 0.5f);
        Vector3 pos_5 = new Vector3(width * 0.5f, height * 0.5f, length * 0.5f);
        Vector3 pos_6 = new Vector3(width * 0.5f, height * 0.5f, -length * 0.5f);
        Vector3 pos_7 = new Vector3(-width * 0.5f, height * 0.5f, -length * 0.5f);

        Vector3[] vertices = new Vector3[]
        {
            // Bottom
            pos_0, pos_1, pos_2, pos_3,

            // Left
            pos_7, pos_4, pos_0, pos_3,

            // Front 
            pos_4, pos_5, pos_1, pos_0,

            // Back
            pos_6, pos_7, pos_3, pos_2,

            // Right
            pos_5, pos_6, pos_2, pos_1,

            // Top
            pos_7, pos_6, pos_5, pos_4,
        };
        #endregion


        #region UVs

        Vector2 _00 = new Vector2(0.0f, 0.0f);
        Vector2 _10 = new Vector2(1.0f, 0.0f);
        Vector2 _01 = new Vector2(0.0f, 1.0f);
        Vector2 _11 = new Vector2(1.0f, 1.0f);

        Vector2[] uvs = new Vector2[]
        {
            // Bottom
            _11, _01, _00, _10,

            // Left
            _11, _01, _00, _10,

            // Front
            _11, _01, _00, _10,

            // Back
            _11, _01, _00, _10,

            // Right
            _11, _01, _00, _10,

            // Top
            _11, _01, _00, _10,
        };
        #endregion


        #region Normales
        Vector3 up    = Vector3.up;
        Vector3 down  = Vector3.down;
        Vector3 front = Vector3.forward;
        Vector3 back  = Vector3.back;
        Vector3 left  = Vector3.left;
        Vector3 right = Vector3.right;

        Vector3[] normals = new Vector3[]
        {
	        // Bottom
	        down, down, down, down,
 
	        // Left
	        left, left, left, left,
 
	        // Front
	        front, front, front, front,
 
	        // Back
	        back, back, back, back,
 
	        // Right
	        right, right, right, right,
 
	        // Top
	        up, up, up, up
        };
        #endregion


        #region Triangles
        int[] triangles = new int[]
        {
            // Bottom
            3, 1, 0,
            3, 2, 1,

  	        // Left
	        3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
            3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
 
	        // Front
	        3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
            3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
 
	        // Back
	        3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
            3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
 
	        // Right
	        3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
            3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
 
	        // Top
	        3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
            3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
        };
        #endregion


        mesh_1.vertices = vertices;
        mesh_1.normals = normals;
        mesh_1.uv = uvs;
        mesh_1.triangles = triangles;
        mesh_1.RecalculateBounds();

        mesh_2.vertices = vertices;
        mesh_2.normals = normals;
        mesh_2.uv = uvs;
        mesh_2.triangles = triangles;
        mesh_2.RecalculateBounds();

        float width_2 = build_block_two.GetComponent<Renderer>().bounds.size.x;

        float pos_x = 0;

        if (Random.Range(0, 10) >= 5)
        {
            pos_x = build_block_two.transform.position.x + width_2 / 10;
        }

        else
        {
            pos_x = build_block_two.transform.position.x - width_2 / 10;
        }

        float height_2 = build_block_two.GetComponent<Renderer>().bounds.size.y / 2;



        build_block_one.transform.position = new Vector3(build_block_one.transform.position.x, height_2, build_block_one.transform.position.z);
        build_block_two.transform.position = new Vector3(pos_x, height_2, build_block_two.transform.position.z);

        build_block_one.transform.parent = building_base.transform;

        build_block_two.transform.parent = building_base.transform;

        building_base.GetComponent<MeshCombine>().CombineMeshes();

        building_base.transform.position = new Vector3(_parent_pos.x + size_x, 0, _parent_pos.z + size_z);

        return building_base;
    }
}
