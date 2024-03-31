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

    Coroutine snapBackCoroutine;
    private void Start()
    {
        audios = GetComponent<AudioSource>();
    }
    private void OnMouseDown()
    {
        thisPos = transform.localPosition;
        audios.Play();
		if (snapBackCoroutine != null)
			StopCoroutine(snapBackCoroutine);
	}
    private void OnMouseDrag()
    {
        Camera cam = Camera.main;
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        MousePos = new Vector3(MousePos.x, thisPos.y, 0);
        transform.localPosition = MousePos;
	}
	private void OnMouseUp()
	{
        if (snapBackCoroutine != null)
            StopCoroutine(snapBackCoroutine);
        snapBackCoroutine = StartCoroutine("SnapBackToOrigin");
	}

    private IEnumerator SnapBackToOrigin()
    {
        yield return new WaitForSeconds(.5f);
        for(int i = 0; i < 30; i++)
        {
			transform.localPosition = Vector3.Lerp(new Vector3(transform.localPosition.x, transform.localPosition.y, 0),new Vector3(0, transform.localPosition.y, 0),(i/30.0f));
			yield return new WaitForEndOfFrame();
		}
		transform.localPosition = new Vector3(0, transform.localPosition.y, 0);

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
        if (Mathf.Abs(transform.localPosition.x / joystickMaximum) <= 0.125f)
            return 0;
        return Mathf.Clamp(transform.localPosition.x / joystickMaximum,-1,1);
    }
}
