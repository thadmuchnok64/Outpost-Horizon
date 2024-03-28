using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public static WorldBuilder Instance;
    public GameObject floor;

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

    public void SpawnWorldTile(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(floor, position, Quaternion.Euler(rotation));

    }
}
