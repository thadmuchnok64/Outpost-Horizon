using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CraneHandler : MonoBehaviour
{
    private List<ShippingContainer> shippingContainers = new List<ShippingContainer>();
    public int shippingContainersCount = 34;
    public Transform crane;
    public GameObject craneArea;
    public GameObject crate;
    public GameObject player;
    Coroutine cor;

    public static CraneHandler instance;

    private void Start()
    {
        if (instance != null)
        {
            Debug.Log("Multiple crane handlers!");
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        for(int i = 0; i < shippingContainersCount; i++)
        {
            shippingContainers.Add(Instantiate(crate, craneArea.transform.position, Quaternion.identity, craneArea.transform).GetComponent<ShippingContainer>());
        }
    }

    public void SetCraneOrigin(Vector3 pos, Vector3 rot)
    {
       // transform.position = transform.position+(pos - crane.position);
    }

    public void SetCraneOrientation(Vector3 pos,Vector3 euler)
    {
        if (cor != null)
            StopCoroutine(cor);
        cor = StartCoroutine(LerpOr(pos, euler));

    }

    public IEnumerator LerpOr(Vector3 pos, Vector3 rot)
    {
        float timer = 0;
        var firstpos = crane.position;
        var firstrot = crane.rotation;
        while (timer < .195f)
        {
            timer += Time.deltaTime;
            crane.position = Vector3.Lerp(firstpos, pos, timer / .195f);
            crane.rotation = Quaternion.Lerp(firstrot, Quaternion.Euler(rot), timer / .195f);
            yield return new WaitForEndOfFrame();
        }
        crane.position = Vector3.Lerp(crane.position, pos,1);
        crane.rotation = Quaternion.Lerp(crane.rotation, Quaternion.Euler(rot),1);
    }

    public void SetOrientationOfShippingContainer(int index, Vector3 position, Vector3 rotation)
    {
        shippingContainers[index].SetOrientation(position, rotation);
    }
}
