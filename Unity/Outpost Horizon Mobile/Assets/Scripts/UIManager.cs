using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public TMPro.TMP_InputField textbox;

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
		startMenu.SetActive(false);
		//usernameField.interactable= false;
		TestClient.instance.ConnectToServer();
	}

	// Sends a test message to the server, which gets sent to unreal
    public void SendMessageToServer()
    {
        ClientSend.SendTestMessage(textbox.text);
    }
    public void SendToServer(string message)
    {
        ClientSend.SendTestMessage(message);
    }
    public void SendNumberToServer(float[] numarray)
    {
        ClientSend.SendNumber(numarray);
    }
}
