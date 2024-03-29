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

    [Header("Debug Settings")]
    public bool skipIntro = false;
    public bool debugMode = false;
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Multiple GM Behaviour!");
            Destroy(this);
        }
        instance = this;

        if(skipIntro)
        {
            Invoke("SkipIntro", .2f);
        }
        if (debugMode)
        {
            Invoke("debugStart", .2f);
        }
    }
    
    public void SkipIntro()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().Play("Idle");
    }
    public void debugStart()
    {
        GameObject.Find("Canvas").transform.Find("B_Debug_Page").gameObject.SetActive(true);
    }
    public void ShowMenuItem(int menuint)
    {
        foreach (GameObject g in menuitems)
        {
            g.SetActive(false);
        }
        show = menuitems[menuint];
        button = menubuttons[menuint];
        clawUI.SetActive(false);
        if (menuint == 4)
            clawUI.SetActive(true);
        show.gameObject.SetActive(true);
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("ErrorMessage"))
        {
            e.SetActive(false);
        };
        if (button != null)
            button.GetComponent<Image>().color = Color.white;
    }
    public void NotifAnnounce()
    {
        notif =  notif.Remove(0, 5);
        foreach (GameObject g in menubuttons)
        {
            string gname = g.name;
            gname = gname.Remove(0, 2);
            if (gname == notif)
            {
                g.GetComponent<Image>().color = Color.yellow;
            }
        }
    }
}
