using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Vector3 position;

    private float size_x;
    private float size_z;

    private bool divided;

    private List<GameObject> child_nodes;

    private int no_divisions = 4;
    private int no_build_depth = 3;

    private int divide_count;

    Vector3 bottom_left_pos;
    Vector3 bottom_right_pos;
    Vector3 top_left_pos;
    Vector3 top_right_pos;

    [SerializeField] GameObject building;

    private void Start()
    {
        child_nodes = new List<GameObject>();
    }


    public void Initialise(Vector3 _position, float _size_x, float _size_z,
        List<Vector3> _positions, int _depth, GameObject _node, Transform _parent_node, int _division)
    {
        child_nodes = new List<GameObject>();

        size_x = _size_x;
        size_z = _size_z;

        position = _position;

        float offset_x = size_x / 10;
        float offset_z = size_z / 10;

        bottom_left_pos  = new Vector3(position.x + offset_x, position.y, position.z + offset_z);

        bottom_right_pos = new Vector3(position.x + size_x - offset_x, position.y, position.z + offset_z);

        top_right_pos    = new Vector3(position.x + size_x - offset_x, position.y, position.z + size_z - offset_z);

        top_left_pos     = new Vector3(position.x + offset_x, position.y, position.z + size_z - offset_z);

        transform.parent = _parent_node.transform;

        if (_depth > 0)
        {
            // Check if this node needs spliting
            if(DivideCheck(_positions))
            {
                divided = true;
                _division++;
                Divide(_positions, _depth, _node, _parent_node, _division);
            }
        }


        // if this node hasn't been divided, and it deeper than the second division
        if (!divided && _division > no_build_depth)
            GenerateBuilding();
    }


    bool DivideCheck(List<Vector3> _positions)
    {
        int count = 0;

        foreach(Vector3 pos in _positions)
        {
            // is this position within bounds of node
            if(pos.x >= position.x && pos.x < (position.x + size_x) &&
                pos.z >= position.z && pos.z < (position.z + size_z))
            {
                count++;
            }


            if(count >= divide_count)
            {
                return true;
            }
        }

        return false;
    }


    void Divide(List<Vector3> _positions, int _depth, GameObject _node, Transform _parent_node, int _division)
    {
        // Each recursion this should go down one
        _depth -= 1;

        Vector3 new_position = position;

        float new_size_x = size_x / 2;
        float new_size_z = size_z / 2;

        int count = 0;

        // Order (Bottom left, Bottom right, Top left, Top right)
        for (int i = 0; i < no_divisions; i++)
        {
            var node_obj = Instantiate(_node, new_position, Quaternion.identity);

            child_nodes.Add(node_obj);

            node_obj.GetComponent<Node>().SetDivideCount(divide_count);

            node_obj.GetComponent<Node>().Initialise(new_position, new_size_x, new_size_z, _positions, _depth, _node, _parent_node.transform, _division);

            new_position.x += size_x / 2;

            count++;

            if (count > 1)
            {
                new_position.x = position.x;
                new_position.z += size_z / 2;
                count = 0;
            }
        }
    }


    /*
    public void AddFuzz()
    {
        // Take each corner and add 10% fuzz

        float fuzz_x = size_x / 10;
        float fuzz_z = size_z / 10;

        bottom_left_pos.x += Random.Range((bottom_left_pos.x - fuzz_x), (bottom_left_pos.x + fuzz_x));




        if (child_nodes.Count == 0)
            return;

        foreach(GameObject child in child_nodes)
        {
            child.GetComponent<Node>().AddFuzz();
        }
    }*/


    private void GenerateBuilding()
    {
        var build = Instantiate(building, position, Quaternion.identity);

        float scale = size_x / 10 * 6;

        build.GetComponent<ProceduralMesh>().Initialise(scale);

        build.transform.position = new Vector3(position.x + (size_x / 2), scale, (position.z + size_z / 2));
    }


    public void SetDivideCount(int _count)
    {
        divide_count = _count;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Bottom Left to Bottom Right
        Gizmos.DrawLine(position, new Vector3(position.x + size_x, 0, position.z));

        // Bottom Right to Top Right
        Gizmos.DrawLine(new Vector3(position.x + size_x, 0, position.z), new Vector3(position.x + size_x, 0, position.z + size_z));

        // Top Right to Top Left
        Gizmos.DrawLine(new Vector3(position.x + size_x, 0, position.z + size_z), new Vector3(position.x , 0, position.z + size_z));

        // Top Left to Bottom Left
        Gizmos.DrawLine(new Vector3(position.x, 0, position.z + size_z), position);

        // if this node has not been divided we have a building area...
        if (!divided)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(bottom_left_pos, bottom_right_pos);
            Gizmos.DrawLine(bottom_right_pos, top_right_pos);
            Gizmos.DrawLine(top_right_pos, top_left_pos);
            Gizmos.DrawLine(top_left_pos, bottom_left_pos);
        }
    }
}
