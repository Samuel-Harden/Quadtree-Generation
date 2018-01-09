using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadtreeRoadGen : MonoBehaviour
{
    [SerializeField] int grid_height;
    [SerializeField] int grid_width;
    [SerializeField] int no_positions;
    [SerializeField] int max_depth;

    [SerializeField] bool show_positions;

    [SerializeField] GameObject node;
    [SerializeField] GameObject node_parent;

    private List<Vector3> positions;


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
            Vector3 pos = new Vector3(Random.Range(0, grid_width), 0,
                Random.Range(0, grid_height));

            positions.Add(pos);
        }
    }


    void GenerateInitialNode()
    {
        Vector3 pos  = Vector3.zero;
        float size_x = grid_width;
        float size_z = grid_height;

        var node_obj = Instantiate(node, pos, Quaternion.identity);

        node_obj.GetComponent<Node>().Initialise(Vector3.zero,
            size_x, size_z, positions, max_depth, node, node_parent.transform);

        //node_obj.GetComponent<Node>().AddFuzz();
    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        if (!show_positions)
            return;

        Gizmos.color = Color.white;

        foreach (Vector3 pos in positions)
        {
            Gizmos.DrawWireSphere(pos, 1);
        }
    }
}
