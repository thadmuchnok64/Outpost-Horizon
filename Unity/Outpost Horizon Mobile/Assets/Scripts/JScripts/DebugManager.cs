using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DebugManager : MonoBehaviour
    /* IMPORTANT: The debug manager must be updated regularly with other scripts.
    Its main function is to allow testing without the use of VR, to speed up
    development. */
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
    public void OnDebugSend()
    {
        if (ClientSendMessage._csmsg[0] == '/')
        {
            ClientSendMessage._csmsg = ClientSendMessage._csmsg.Replace("/","");
            //UPDATE this with ClientHandle.ReadMessage
            switch(ClientSendMessage._csmsg)
            {
                case "RodOnFloor!":
                    RodBehaviour.instance.ShowError();
                    break;
                case "CraneError!":
                    TheClaw.instance.CraneError();
                    break;
            }
        }
        else
            UIManager.instance.SendToServer(ClientSendMessage._csmsg);
    }
}
