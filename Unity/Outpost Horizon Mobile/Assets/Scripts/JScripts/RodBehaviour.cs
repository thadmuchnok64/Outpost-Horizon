using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RodBehaviour : MonoBehaviour
{
    public static RodBehaviour instance;
    public TMP_Text text;
    public static List<int> rods = new List<int>();
    public GameObject roddiag;
    public static string rodstoinsert;
    public List<GameObject> rodstin;
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
        for (int i = 0; i < rods.Count; i++)
        {
            rodstoinsert = rodstoinsert + " ," + rods[i].ToString();
            rodstin[rods[i]].SetActive(true);
        }
        text.text = "Fuel Rods are Active. Please Insert Rod(s):" + rodstoinsert;
        roddiag.SetActive(true);
    }
}
