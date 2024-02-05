using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    public GameObject show;
    public List<GameObject> menuitems = new List<GameObject>();
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Multiple GM Behaviour!");
            Destroy(this);
        }
        instance = this;
    }
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
    public void RodActivation()
    {
        foreach (GameObject g in menuitems)
        {
            g.SetActive(false);
        }
        menuitems[2].SetActive(true);
    }
}
