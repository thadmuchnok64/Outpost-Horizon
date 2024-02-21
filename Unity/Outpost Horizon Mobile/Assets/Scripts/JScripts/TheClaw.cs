using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TheClaw : MonoBehaviour
{
    public GameObject claw;
    public GameObject stick;
    Vector3 prevclaw;
    Vector3 speed;
    float[] stickPos = new float[3];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        prevclaw = claw.transform.position;
        if (claw.transform.localPosition.y <= 3.59f && claw.transform.localPosition.y >= .33f)
            claw.transform.Translate(stick.transform.localPosition * Time.deltaTime);
        else if (claw.transform.localPosition.y > 3.59f)
            claw.transform.localPosition = new Vector3(0, 3.59f, 0);
        else if (claw.transform.localPosition.y < .33f)
            claw.transform.localPosition = new Vector3(0, .33f, 0);
        speed = claw.transform.position - prevclaw;
        stickPos[0] = speed.x;
        stickPos[1] = speed.y * 100000;
        stickPos[2] = speed.z;
        Debug.Log(stickPos[1]);
        UIManager.instance.SendNumberToServer(stickPos);
    }
}
