using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TheClaw : MonoBehaviour
{
    public static TheClaw instance;

    public GameObject claw;
    public StickMove joystick;
    public float maxDistance = 2;
    public GameObject craneError;
    bool samecraneerror = false;
    Vector3 prevclaw;
    Vector3 speed;
    // Start is called before the first frame update - singleton
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("multiple claws");
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Done: Update this so that unreal sends the position of the object back through unity
       prevclaw = claw.transform.position;
       if (claw.transform.localPosition.y <= .38f && claw.transform.localPosition.y >= -.38f)
            claw.transform.Translate(joystick.transform.localPosition * joystick.joystickMaximum * Time.deltaTime);
       else if (claw.transform.localPosition.y > .38f)
           claw.transform.localPosition = new Vector3(0, .38f, 0);
       else if (claw.transform.localPosition.y < -.38f)
           claw.transform.localPosition = new Vector3(0, -.38f, 0);
       speed = claw.transform.position - prevclaw;
       UIManager.instance.ClawGame(joystick.GetControlValue());
    }
    public void MoveClawToLocation(Vector2 vec)
    {
        claw.transform.localPosition = vec * maxDistance;
    }
    public void CraneError()
    {
        if (samecraneerror == false)
        {
            samecraneerror = true;
            craneError.SetActive(true);
            StartCoroutine(waiteshow());
        }
    }
    IEnumerator waiteshow()
    {
        yield return new WaitForSeconds(5);
        craneError.SetActive(false);
        samecraneerror = false;
    }
}