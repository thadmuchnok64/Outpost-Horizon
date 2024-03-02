using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClientSendMessage : MonoBehaviour
{
    public TMP_InputField inputField;
    public static string _csmsg;
    public void ClearField()
    {
        _csmsg = inputField.text;
        inputField.text = "";
        Checkmsg(_csmsg);
    }
    void Checkmsg(string msg)
    {
        if (msg == "disconnect")
        {
            UIManager.instance.DisconnectFromServer();
        }
        else if (msg == "connect")
        {
            UIManager.instance.ConnectToServer();
        }
    }
}
