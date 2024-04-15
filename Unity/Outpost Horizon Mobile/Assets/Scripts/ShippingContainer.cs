using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShippingContainer : MonoBehaviour
{

    Coroutine cor;
    public void SetOrientation(Vector3 pos, Vector3 rot)
    {
        if(cor != null)
            StopCoroutine(cor);
        cor = StartCoroutine(LerpOr(pos,rot));
    }

    public IEnumerator LerpOr(Vector3 pos, Vector3 rot)
    {
        float timer = 0;
        var initialpos = transform.position;
        var initialrot = transform.rotation;
        while (timer < .195f)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(initialpos,pos,timer/.195f);
            transform.rotation = Quaternion.Lerp(initialrot,Quaternion.Euler(rot), timer / .195f);
            yield return new WaitForEndOfFrame();
        }
        transform.position = Vector3.Lerp(transform.position, pos, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rot), 1);
    }
}
