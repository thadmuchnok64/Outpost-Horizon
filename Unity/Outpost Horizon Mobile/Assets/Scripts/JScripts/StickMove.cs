using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMove : MonoBehaviour
{
    Vector3 MousePos;
    Vector3 thisPos;
    Camera cam;
    public float joystickMaximum = .7f;
    AudioSource audios;
    public AudioClip clup;
    public AudioClip cldwn;
    AudioClip lastcl;
    private void Start()
    {
        audios = GetComponent<AudioSource>();
    }
    private void OnMouseDown()
    {
        thisPos = transform.localPosition;
        audios.Play();
    }
    private void OnMouseDrag()
    {
        Camera cam = Camera.main;
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        MousePos = new Vector3(MousePos.x, thisPos.y, 0);
        transform.localPosition = MousePos;
    }
    private void Update()
    {
        lastcl = audios.clip;
        if (transform.localPosition.x > joystickMaximum)
        {
            transform.localPosition = new Vector3(joystickMaximum,transform.localPosition.y, 0);
        }
        else if (transform.localPosition.x < -joystickMaximum)
        {
            transform.localPosition = new Vector3( -joystickMaximum,transform.localPosition.y, 0);
        }
        if (transform.localPosition.x < 0.1 && transform.localPosition.x > -0.1)
            audios.Pause();
        else if (transform.localPosition.x > 0.1)
        {
            audios.clip = clup;
        }
        else if (transform.localPosition.x < -0.1)
        {
            audios.clip = cldwn;
        }
        if (lastcl != audios.clip)
            audios.Play();
    }

    public float GetControlValue()
    {
        return Mathf.Clamp(transform.localPosition.x / joystickMaximum,-1,1);
    }
}
