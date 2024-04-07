using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public static WorldBuilder Instance;
    [SerializeField] Transform world;
    public GameObject floor;
    public GameObject wall;
    public GameObject door;
    public GameObject elevator;
    public GameObject elevatorPort;
    public GameObject controlRoom;
    public GameObject crate;
    public GameObject engine;
    public GameObject craneControl;
    public GameObject halfWallLeft, halfWallRight;

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
    public void DestroyTheEntireGoddamnWorld()
    {
        foreach (Transform child in world.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SpawnWorldFloorTile(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(floor, position, Quaternion.Euler(rotation),world);
    }

    public void SpawnWall(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(wall, position, Quaternion.Euler(rotation),world);
    }

    public void SpawnDoor(int index, Vector3 position, Vector3 rotation)
    {
        var d = Instantiate(door, position, Quaternion.Euler(rotation), world);
        d.GetComponentInChildren<DoorButton>().ID = index;
    }

    public void SpawnElevator(int index, Vector3 position, Vector3 rotation)
    {
        var e = Instantiate(elevator, position, Quaternion.Euler(rotation), world);
        e.GetComponent<ElevatorButton>().ID = index;
    }

    public void SpawnElevatorPort(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(elevatorPort, position, Quaternion.Euler(rotation), world);
    }

    public void SpawnControlRoom(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(controlRoom, position, Quaternion.Euler(rotation), world);
    }

    public void SpawnCrate(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(crate, position, Quaternion.Euler(rotation), world);
    }
    public void SpawnEngine(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(engine, position, Quaternion.Euler(rotation), world);
    }
    public void SetCraneOrientation(int index, Vector3 position, Vector3 rotation)
    {
        CraneHandler.instance.SetCraneOrigin(position, rotation);
    }

    public void SpawnCraneControl(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(craneControl, position, Quaternion.Euler(rotation), world);
    }

    public void SpawnHalfWallLeft(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(halfWallLeft, position, Quaternion.Euler(rotation), world);
    }

    public void SpawnHalfWallRight(int index, Vector3 position, Vector3 rotation)
    {
        Instantiate(halfWallRight, position, Quaternion.Euler(rotation), world);
    }
}
