using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker Instance;
    [SerializeField] GameObject player;
    Coroutine lerper;


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Multiple player trackers!!!");
            Destroy(this);
        }
    }

    public void SetOrientation(Vector3 position,Vector3 rotation)
    {
        if(lerper != null)
            StopCoroutine(lerper);
        lerper = StartCoroutine(LerpOrientation(position,rotation));
    }

    private IEnumerator LerpOrientation(Vector3 position, Vector3 rotation)
    {
        float t = 0;
        var oldpos = player.transform.position;
       // var oldrot = player.transform.rotation;
        while (t < .2f)
        {
            yield return new WaitForFixedUpdate();
            t += Time.fixedDeltaTime;
            player.transform.position = Vector3.Lerp(oldpos, position, t / .2f);
     //       player.transform.rotation = Quaternion.Lerp(oldrot, Quaternion.Euler(rotation), t / .2f);
        }
        player.transform.position = Vector3.Lerp(oldpos, position, 1);
       // player.transform.rotation = Quaternion.Lerp(oldrot, Quaternion.Euler(rotation), 1);
    }

}
