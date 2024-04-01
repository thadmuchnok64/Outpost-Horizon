using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public static WorldBuilder Instance;
    public GameObject floor;
    public GameObject wall;
    public GameObject door;
    public GameObject elevator;
    public GameObject elevatorPort;
    public GameObject controlRoom;




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

    public void SpawnDoor(int index, Vector3 position, Vector3 rotation)
    {
        var d = Instantiate(door, position, Quaternion.Euler(rotation));
        d.GetComponent<DoorButton>().ID = index;
    }

    public void SpawnElevator(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(elevator, position, Quaternion.Euler(rotation));
    }

    public void SpawnElevatorPort(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(elevatorPort, position, Quaternion.Euler(rotation));
    }

    public void SpawnControlRoom(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(controlRoom, position, Quaternion.Euler(rotation));
    }
}
