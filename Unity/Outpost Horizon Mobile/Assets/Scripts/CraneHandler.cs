using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneHandler : MonoBehaviour
{
    public List<Transform> shippingContainers = new List<Transform>();

    public void SetOrientationOfShippingContainer(int index, Vector3 position, Vector3 rotation)
    {
        shippingContainers[index].position = position;
        shippingContainers[index].eulerAngles = rotation;
    }
}
