using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneCameraControl : MonoBehaviour
{
    public Transform crane;
    private Vector3 relativeOffset;
    // Start is called before the first frame update
    void Start()
    {
        relativeOffset = transform.position - crane.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, crane.position + relativeOffset, Time.deltaTime);
    }
}
