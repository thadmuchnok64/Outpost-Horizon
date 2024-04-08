using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using System.Linq;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    public GameObject show;
    GameObject button;
    public string notif;
    public List<GameObject> menuitems = new List<GameObject>();
    public List<GameObject> menubuttons = new List<GameObject>();
    public GameObject backButton;
    public GameObject clawUI;
    public Camera clawcam, regcam;
    public int doorAdminCode = 0;

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
        if(menuint != 6 && menuint != 2)
        {
            backButton.SetActive(true);
        }
        else
        {
            backButton.SetActive(false);

        }
        clawUI.SetActive(false);
        if (show.name == "Claw")
        {
            clawUI.SetActive(true);
            clawcam.gameObject.SetActive(true);
            regcam.gameObject.SetActive(false);

        } else
        {
            clawcam.gameObject.SetActive(false);
            regcam.gameObject.SetActive(true);
        }
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
    public void DebugRunLocalFunction()
    {
        Invoke("SkipIntro", .2f);
    }

    public void CallAdminOnDoor(int code)
    {
        DoorButton[] doors = FindObjectsOfType<DoorButton>();
        doors.Where(x => x.ID == code).First().AdminNeeded();
    }
    public void TrackElevator(int id, int z)
    {
        ElevatorButton[] elevators = FindObjectsOfType<ElevatorButton>();
        var elev = elevators.Where(x => x.ID == id).First();
        elev.transform.position = new Vector3(elev.transform.position.x, z / 100, elev.transform.position.z);
        CraneCameraControl.instance.ReassignCamera(elev.cameraPoint);
    }
}
