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
    public List<GameObject> rodstin;
    // Start is called before the first frame update
    void Awake()
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
        foreach (GameObject rodtin in rodstin)
        {
            rodtin.SetActive(false);
        }
        for (int i = 0; i < rods.Count-1; i++)
        {
            rodstoinsert = rodstoinsert + rods[i].ToString() +", ";
            rodstin[rods[i]].SetActive(true);
        }
        rodstoinsert += "and " + rods[rods.Count - 1];
        rodstin[rods[rods.Count - 1]].SetActive(true);

        text.text = "Fuel Rods are malfunctioning. Please reinsert Rod(s): " + rodstoinsert + "\n\nFor types of rods - please go to \"INFO\" tab";
        roddiag.SetActive(true);
    }
    public void RodsComplete()
    {
        rodstoinsert = "";
        text.text = "All Fuel Rods are operational! Well done.";
        roddiag.SetActive(false);
    }
    public void ShowError()
    {
        img.SetActive(true);
        StartCoroutine(waitshow());
    }
    IEnumerator waitshow()
    {
        yield return new WaitForSeconds(5);
        img.SetActive(false);
    }
}
