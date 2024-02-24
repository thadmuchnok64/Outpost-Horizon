using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatSendMessage : MonoBehaviour
{
    public RectTransform content;
    public TMP_InputField input;
    public TMP_Text output;
    string text;
    Vector2 newSize;
    public void SendToChat()
    {
        text = input.text;
        input.text = "";
        output.text = output.text + "\n"+ "You:" + text;
        output.text = output.text + "\n Helpy: The servers are currently undergoing maintenance, please try again later.";
        if (output.text.Length > 200)
        {
            newSize = content.sizeDelta + new Vector2(0,200);
            content.sizeDelta = newSize;
            output.gameObject.transform.localPosition += new Vector3 (0,100,0);
        }
    }
}
