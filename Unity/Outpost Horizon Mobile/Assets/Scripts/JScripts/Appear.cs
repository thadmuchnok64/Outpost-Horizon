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
        show.gameObject.SetActive(true);
    }
    public void DebugS()
    {
        show = menuitems[0];
        foreach (GameObject g in menuitems)
        {
            g.SetActive(false);
        }
    }
    public void LocksS()
    {
        show = menuitems[1];
        foreach (GameObject g in menuitems)
        {
            g.SetActive(false);
        }
    }
}
