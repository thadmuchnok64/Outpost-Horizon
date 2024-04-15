using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraButtonManager : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] Camera cam;
    [SerializeField] RenderTexture texture;
    [SerializeField] GameObject uiWaypointPrefab;

    [SerializeField] GameObject uiButtonPrefab;

    public static CameraButtonManager instance;
    List<GameObject> uiButtons;
    List<CameraButton> buttons;
    List<WaypointUI> waypoints;
    List<GameObject> uiWaypoints;


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
        waypoints = new List<WaypointUI>();
        uiWaypoints = new List<GameObject>();
    }

    public void AddButton(CameraButton button,Sprite icon)
    {
        buttons.Add(button);
        GameObject g = Instantiate(uiButtonPrefab, transform);
        g.GetComponent<Image>().sprite = icon;
        g.GetComponent<ButtonRelay>().button = button;
        button.relay = g.GetComponent<ButtonRelay>();
        uiButtons.Add(g);
    }

    public void AddWaypoint(WaypointUI point, string message)
    {
        waypoints.Add(point);
        GameObject g = Instantiate(uiWaypointPrefab, transform);
        g.GetComponent<TMPro.TextMeshProUGUI>().text = message;
        uiWaypoints.Add(g);
    }

    private void Update()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            var spagheti = cam.WorldToScreenPoint(buttons[i].transform.position);
            (uiButtons[i].transform as RectTransform).anchoredPosition = new Vector3(image.rectTransform.rect.width * (spagheti.x / texture.width), (image.rectTransform.rect.height * spagheti.y / texture.height) - 120, 0);
            
            if (CraneCameraControl.instance.orthoOverride||cam.transform.position.y - buttons[i].transform.position.y > cam.farClipPlane || cam.transform.position.y < buttons[i].transform.position.y-3){
                uiButtons[i].gameObject.SetActive(false);
            }
            else
            {
                uiButtons[i].gameObject.SetActive(true);

            }
        }

        for (int i = 0; i < waypoints.Count; i++)
        {
            var spagheti = cam.WorldToScreenPoint(waypoints[i].transform.position);
            (uiWaypoints[i].transform as RectTransform).anchoredPosition = new Vector3(image.rectTransform.rect.width * (spagheti.x / texture.width), (image.rectTransform.rect.height * spagheti.y / texture.height), 0);

            if (CraneCameraControl.instance.orthoOverride || cam.transform.position.y - waypoints[i].transform.position.y > cam.farClipPlane || cam.transform.position.y < waypoints[i].transform.position.y - 3)
            {
                uiWaypoints[i].gameObject.SetActive(false);
            }
            else
            {
                uiWaypoints[i].gameObject.SetActive(true);

            }
        }
    }
}
