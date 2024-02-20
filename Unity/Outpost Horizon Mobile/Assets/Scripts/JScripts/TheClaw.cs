using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TheClaw : MonoBehaviour
{
    public GameObject claw;
    public GameObject stick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (claw.transform.localPosition.y <= 3.59f && claw.transform.localPosition.y >= .33f)
            claw.transform.Translate(stick.transform.localPosition * Time.deltaTime);
        else if (claw.transform.localPosition.y > 3.59f)
            claw.transform.localPosition = new Vector3(0, 3.59f, 0);
        else if (claw.transform.localPosition.y < .33f)
            claw.transform.localPosition = new Vector3(0, .33f, 0);
    }
}
