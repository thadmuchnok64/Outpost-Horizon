using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneHandler : MonoBehaviour
{
    public List<Transform> shippingContainers = new List<Transform>();
    public Transform crane;
    public GameObject craneArea;

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
    }

    public void SetCraneOrientation(Vector3 pos,Vector3 euler)
    {
        crane.transform.localPosition = pos;
        crane.eulerAngles = euler;
    }

    public void SetOrientationOfShippingContainer(int index, Vector3 position, Vector3 rotation)
    {
        shippingContainers[index].localPosition = position;
        shippingContainers[index].eulerAngles = rotation;
    }
}
