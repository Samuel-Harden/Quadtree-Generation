using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadtreeRoadGen : MonoBehaviour
{
    [SerializeField] int grid_height;
    [SerializeField] int grid_width;
    [SerializeField] int no_positions;
    [SerializeField] int max_depth;

    [SerializeField] int divide_count = 2;
    [SerializeField] int perlin_noise = 10;

    [SerializeField] bool show_positions;

    [SerializeField] GameObject node;
    [SerializeField] GameObject node_parent;

    private List<Vector3> positions;
    private List<Vector3> new_positions;
    private PerlinPopGen pop_gen;


    void Start ()
    {
        positions = new List<Vector3>();
        new_positions = new List<Vector3>();

        pop_gen = this.gameObject.GetComponent<PerlinPopGen>();

        pop_gen.SetPerlinNoise(perlin_noise);

        GeneratePositions();

        GenerateInitialNode();
	}


    void GeneratePositions()
    {
        /*
        for(int i = 0; i < no_positions; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0, grid_width), 0,
                Random.Range(0, grid_height));

            population.Add(pos);
        }*/

        pop_gen.GeneratePopData(grid_width, grid_height, positions);
        //population = pop_gen.GeneratePopData(grid_width, grid_height);

        for (int i = 0; i < positions.Count; i++)
        {
            int pop = pop_gen.GetPop(i);

            if(pop > 90) // Guranteed position
            {
                new_positions.Add(positions[i]);
            }

            else if (pop > 80)
            {
                //75% CHANCE
                if(Random.Range(0, 100) > 25)
                {
                    new_positions.Add(positions[i]);
                }
            }

            else if (pop > 70)
            {
                //50% CHANCE
                if (Random.Range(0, 100) > 50)
                {
                    new_positions.Add(positions[i]);
                }
            }

            else if (pop > 60)
            {
                //25% CHANCE
                if (Random.Range(0, 100) > 75)
                {
                    new_positions.Add(positions[i]);
                }
            }
        }

    }


    void GenerateInitialNode()
    {
        Vector3 pos  = Vector3.zero;
        float size_x = grid_width;
        float size_z = grid_height;

        var node_obj = Instantiate(node, pos, Quaternion.identity);

        node_obj.GetComponent<Node>().SetDivideCount(divide_count);

        node_obj.GetComponent<Node>().Initialise(Vector3.zero,
            size_x, size_z, new_positions, max_depth, node, node_parent.transform, 0);

        //node_obj.GetComponent<Node>().AddFuzz();
    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        if (!show_positions)
            return;

        Gizmos.color = Color.white;

        foreach (Vector3 pos in new_positions)
        {
            Gizmos.DrawWireSphere(pos, 1);
        }
    }
}
