using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRelay : MonoBehaviour
{
    public CameraButton button;
    public Sprite urgentSprite;
    [SerializeField] Image secondarySprite;

    public void UrgentTime(bool urgent)
    {
        secondarySprite.gameObject.SetActive(urgent);
    }

    public void DoAction()
    {
        if(button == null)
        {
            Debug.Log("Unassigned camera button!");
            Destroy(this);
        }
        button.DoAction();
    }


}
