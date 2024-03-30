using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager instance;
    public GameObject debugPage;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Multiple Debug managers!");
            Destroy(this);
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDebugClick()
    {
        if (debugPage.activeSelf == false)
            debugPage.SetActive(true);
        else if (debugPage.activeSelf == true)
            debugPage.SetActive(false);
    }
}
