using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadtreeRoadGen : MonoBehaviour
{
    [SerializeField] int grid_height;
    [SerializeField] int grid_width;
    [SerializeField] int no_positions;
    [SerializeField] GameObject position_marker;
    [SerializeField] int max_depth;

    [SerializeField] GameObject node_parent;

    private List<Vector3> positions;
    [SerializeField] GameObject node;


    void Start ()
    {
        positions = new List<Vector3>();

        GeneratePositions();

        GenerateInitialNode();
	}


    void GeneratePositions()
    {
        for(int i = 0; i < no_positions; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0, grid_width), 0, Random.Range(0, grid_height));

            Instantiate(position_marker, pos, Quaternion.identity);

            positions.Add(pos);
        }
    }


    void GenerateInitialNode()
    {
        Vector3 pos  = Vector3.zero;
        float size_x = grid_width;
        float size_z = grid_height;

        var node_obj = Instantiate(node, pos, Quaternion.identity);

        node_obj.GetComponent<Node>().Initialise(Vector3.zero, size_x, size_z, positions, max_depth, node, node_parent.transform);
    }
}
