using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class EndRush : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StartR"))
        {
            UIManager.instance.SendToServer("PickTrue");
            Debug.Log("Picked!");
        }
    }
}
