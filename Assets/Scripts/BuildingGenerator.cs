using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    [SerializeField] GameObject block_mesh_component;
    [SerializeField] GameObject building_root;


    public GameObject GenerateBuilding(Vector3 _parent_pos, float size_x, float size_z, float _scale)
    {
        var new_building_component1 = Instantiate(block_mesh_component, Vector3.zero, Quaternion.identity);
        var new_building_component2 = Instantiate(block_mesh_component, Vector3.zero, Quaternion.identity);

        var building_base = Instantiate(building_root, Vector3.zero, Quaternion.identity);

        new_building_component1.GetComponent<ProceduralMesh>().Initialise(_scale / 2);
        new_building_component2.GetComponent<ProceduralMesh>().Initialise(_scale / 3);

        float height_1 = new_building_component1.GetComponent<Renderer>().bounds.size.y / 2;
        float height_2 = new_building_component2.GetComponent<Renderer>().bounds.size.y / 2;

        float width_2 = new_building_component2.GetComponent<Renderer>().bounds.size.x;

        float pos_x = 0;

        if (Random.Range(0, 10) >= 5)
        {
            pos_x = new_building_component2.transform.position.x + width_2;
        }

        else
        {
            pos_x = new_building_component2.transform.position.x - width_2;
        }



        new_building_component1.transform.position = new Vector3(new_building_component1.transform.position.x, height_1, new_building_component1.transform.position.z);
        new_building_component2.transform.position = new Vector3(pos_x, height_2, new_building_component2.transform.position.z);

        new_building_component1.transform.parent = building_base.transform;

        new_building_component2.transform.parent = building_base.transform;

        building_base.GetComponent<MeshCombine>().CombineMeshes();

        building_base.transform.position = new Vector3(_parent_pos.x + size_x, 0, _parent_pos.z + size_z);
    
        return building_base;
    }
}
