using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonInitializer : MonoBehaviour
{
    [SerializeField] RodBehaviour rodbehavinst;
    [SerializeField] TheClaw claw;

    private void Start()
    {
        rodbehavinst.gameObject.SetActive(true);
        claw.gameObject.SetActive(true);

        Invoke("ReDisable", .1f);
    }

    private void ReDisable()
    {
        rodbehavinst.gameObject.SetActive(false);
        claw.gameObject.SetActive(false);

    }
}
