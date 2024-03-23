using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneHandler : MonoBehaviour
{
    private List<Transform> shippingContainers = new List<Transform>();
    public int shippingContainersCount = 34;
    public Transform crane;
    public GameObject craneArea;
    public GameObject crate;
    public GameObject player;

    public static CraneHandler instance;

    private void Start()
    {
        if (instance != null)
        {
            Debug.Log("Multiple crane handlers!");
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        for(int i = 0; i < shippingContainersCount; i++)
        {
            shippingContainers.Add(Instantiate(crate, craneArea.transform.position, Quaternion.identity, craneArea.transform).transform);
        }
    }

    public void SetCraneOrientation(Vector3 pos,Vector3 euler)
    {
        crane.transform.localPosition = pos;
        crane.eulerAngles = euler;
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        player.transform.localPosition= pos;
    }

    public void SetOrientationOfShippingContainer(int index, Vector3 position, Vector3 rotation)
    {
        shippingContainers[index].localPosition = position;
        shippingContainers[index].eulerAngles = rotation;
    }
}
