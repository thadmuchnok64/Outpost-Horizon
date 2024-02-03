using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Appear : MonoBehaviour
{
    public GameObject show;
    public List<GameObject> menuitems = new List<GameObject>();
    public void Show()
    {
        foreach (GameObject g in menuitems)
        {
            g.SetActive(false);
        }
        show.gameObject.SetActive(true);
    }
    public void DebugS()
    {
        show = menuitems[0];
    }
    public void LocksS()
    {
        show = menuitems[1];
    }
    public void RodS()
    {
        show = menuitems[2];
    }
}
