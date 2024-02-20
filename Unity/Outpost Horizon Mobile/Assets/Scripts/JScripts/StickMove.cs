using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMove : MonoBehaviour
{
    Vector3 MousePos;
    Vector3 thisPos;
    public Vector3 stickPos;
    Camera cam;
    private void OnMouseDown()
    {
        thisPos = transform.localPosition;
    }
    private void OnMouseDrag()
    {
        Camera cam = Camera.main;
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        MousePos = new Vector3(thisPos.x, MousePos.y, 0);
        transform.position = MousePos;
    }
    private void Update()
    {
        if (transform.localPosition.y >= 0.7f)
        {
            transform.localPosition = new Vector3(0, .69f, 0);
        }
        else if (transform.localPosition.y <= -0.7f)
        {
            transform.localPosition = new Vector3(0, -.69f, 0);
        }
        stickPos = transform.localPosition;
    }
}
