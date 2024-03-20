using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    public GameObject show;
    GameObject button;
    public string notif;
    public List<GameObject> menuitems = new List<GameObject>();
    public List<GameObject> menubuttons = new List<GameObject>();
    public GameObject clawUI;
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
        if (button != null)
            button.GetComponent<Image>().color = Color.white;
    }
    public void DebugS()
    {
        show = menuitems[0];
        button = menubuttons[0];
        clawUI.SetActive(false);

    }
    public void LocksS()
    {
        show = menuitems[1];
        button = menubuttons[1];
        clawUI.SetActive(false);

    }
    public void RodS()
    {
        show = menuitems[2];
        button = menubuttons[2];
        clawUI.SetActive(false);

    }
    public void InfoS()
    {
        show = menuitems[3];
        button = menubuttons[3];
        clawUI.SetActive(false);

    }
    public void ClawS()
    {
        show = menuitems[4];
        button = menubuttons[4];
        clawUI.SetActive(true);

    }
    public void ChatS()
    {
        show = menuitems[5];
        button = menubuttons[5];
        clawUI.SetActive(false);

    }
    public void RodActivation()
    {
        foreach (GameObject g in menuitems)
        {
            g.SetActive(false);
        }
        menuitems[2].SetActive(true);
        clawUI.SetActive(false);
    }
    public void NotifAnnounce()
    {
        notif.Remove(0, 4);
        foreach (GameObject g in menubuttons)
        {
            if (g.name == notif)
            {
                g.GetComponent<Image>().color = Color.yellow;
            }
        }
    }
}
