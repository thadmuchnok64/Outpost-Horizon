using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCar : MonoBehaviour
{
    Vector3 MousePos;
    Vector3 thisPos;
    Camera cam;
    bool movable = true;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnMouseDown()
    {
        thisPos = transform.position;
    }
    private void OnMouseDrag()
    {
        Camera cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        if (movable == true)
        {
            MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (gameObject.CompareTag("Horizontal"))
            {
                MousePos = new Vector3(MousePos.x, thisPos.y, 0);
            }
            else if (gameObject.CompareTag("Vertical") || gameObject.CompareTag("StartR"))
            {
                MousePos = new Vector3(thisPos.x, MousePos.y, 0);
            }
            rb.MovePosition(MousePos);
        }
    }
    private void Update()
    {
        rb.velocity = Vector3.zero;
    }
}
