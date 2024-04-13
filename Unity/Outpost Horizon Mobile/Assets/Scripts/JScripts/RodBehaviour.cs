using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RodBehaviour : MonoBehaviour
{
    public static RodBehaviour instance;
    public GameObject img;
    public TMP_Text text;
    public static List<int> rods = new List<int>();
    public GameObject roddiag;
    public static string rodstoinsert;
    public List<Animator> rodstin;
    

    bool samerodfloor = false;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Multiple Rod Behaviour!");
            Destroy(this);
        }
        instance = this;
    }

    // Update is called once per frame
    public void RodsTUpdate()
    {
        for(int i = 0; i< rodstin.Count; i++)
        {
            rodstin[i].SetBool("Working", true);
        }
        for (int i = 0; i < rods.Count-1; i++)
        {
            rodstoinsert = rodstoinsert + rods[i].ToString() +", ";
            rodstin[rods[i]-1].SetBool("Working",false);
        }
        rodstoinsert += "and " + rods[rods.Count - 1];
        rodstin[rods[rods.Count -1]-1].SetBool("Working", false);

        //        rodstin[rods.Count -1 ].Play("Bad");


        text.text = "ROD LOCATION AND TYPE:\n1. Curium - SN-54054\n2 & 7. Uranium - SN-45021\n6 & 3. Technetium - SN-45054\n5 & 4. Neptunium - SN-54021\nFuel Rods are malfunctioning. Please replace Rod(s): " + rodstoinsert+"\n\nPlease direct the onsite-employee to the fuel rod room.";
        roddiag.SetActive(true);
    }
    public void RodsComplete()
    {
        rodstoinsert = "";
        text.text = "All Fuel Rods are operational! Well done.";
        ChallengeParser.instance.TryCompleteChallenge(9);
        roddiag.SetActive(false);
        GeneratorButton[] genbuttons = FindObjectsOfType<GeneratorButton>();
        genbuttons.First().ClearUrgent();
    }
    public void ShowError()
    {
        if (samerodfloor == false)
        {
            samerodfloor = true;
            img.SetActive(true);
            StartCoroutine(waitshow());
        }
    }
    IEnumerator waitshow()
    {
        yield return new WaitForSeconds(5);
        img.SetActive(false);
        samerodfloor = false;
    }
}
