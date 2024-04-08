using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneCameraControl : MonoBehaviour
{
    public Transform player;
    Coroutine pivotLerper;
    public Transform focusPoint;

    public static CraneCameraControl instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Mulitple camera controls!!!");
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        focusPoint = player;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, focusPoint.position, Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, focusPoint.rotation, Time.deltaTime);
    }


    public void ReassignCamera(Transform newPoint)
    {
        focusPoint = newPoint;
        if(pivotLerper != null)
            StopCoroutine(pivotLerper);
        pivotLerper = StartCoroutine(PivotReset());
    }

    private IEnumerator PivotReset()
    {
        yield return new WaitForSeconds(.5f);
        focusPoint = player;
    }

}
