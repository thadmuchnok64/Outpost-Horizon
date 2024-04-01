using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraButtonManager : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] Camera cam;

    [SerializeField] GameObject uiButtonPrefab;

    public static CameraButtonManager instance;
    List<GameObject> uiButtons;
    List<CameraButton> buttons;

    bool initialized = false;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Multiple camera button managers!!!");
            Destroy(this);
        }
        uiButtons = new List<GameObject>();
        buttons = new List<CameraButton>();
    }

    public void AddButton(CameraButton button)
    {
        buttons.Add(button);
        GameObject g = Instantiate(uiButtonPrefab, transform);
        g.GetComponent<ButtonRelay>().button = button;
        uiButtons.Add(g);
    }


    private void Update()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            var spagheti = cam.WorldToScreenPoint(buttons[i].transform.position);
            (uiButtons[i].transform as RectTransform).anchoredPosition = new Vector3(image.rectTransform.rect.width * (spagheti.x / 128), (image.rectTransform.rect.height * spagheti.y / 192) - 120, 0);
        }
    }
}
