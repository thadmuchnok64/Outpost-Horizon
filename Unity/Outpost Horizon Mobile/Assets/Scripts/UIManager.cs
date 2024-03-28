using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public GameObject mMenu;
    public TMP_InputField IPcode;
    public static bool connected = false;

	private void Awake()
	{
			if (instance == null) {
			instance = this;
		} else if ( instance != this)
		{
			Debug.Log("UI Manager already exists!");
			Destroy(this);
		}
	}

	public void ConnectToServer()
	{
        if (IPcode.text != "")
        {
            string[] separ = IPcode.text.Split(':', 2);
            TestClient.instance.ip = separ[0];
            TestClient.instance.port = Int32.Parse(separ[1]);
        }
        IPcode.gameObject.SetActive(false);
		startMenu.SetActive(false);
		//usernameField.interactable= false;
		TestClient.instance.ConnectToServer();
        connected = true;
	}
    public void DisconnectFromServer()
    {
        TestClient.instance.DisconnectFromServer();
        connected = false;
    }

    // Sends a test message to the server, which gets sent to unreal
    public void SendMessageToServer()
    {
        if (connected == true)
            ClientSend.SendTestMessage(ClientSendMessage._csmsg);
    }
    public void SendToServer(string message)
    {
        ClientSend.SendTestMessage(message);
    }
    public void TermsAccept()
    {
        mMenu.gameObject.SetActive(true);
    }
    // Should take in a float from -1 to 1
    public void ClawGame(float input)
    {
		int sensitivity = Mathf.RoundToInt(input * 1000f);
        ClientSend.SendNumber(sensitivity);
    }
}
