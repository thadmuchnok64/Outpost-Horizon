using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public static WorldBuilder Instance;
    public GameObject floor;
    public GameObject wall;


    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.Log("Mutliple world builders!!! What have you done?!");
        }
    }

    public void SpawnWorldFloorTile(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(floor, position, Quaternion.Euler(rotation));
    }

    public void SpawnWall(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(wall, position, Quaternion.Euler(rotation));
    }
}
