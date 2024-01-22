using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    public GameObject connect;
    public GameObject send;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (connect.gameObject.activeSelf == false)
        {
            send.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
        }
    }
}
