using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public GameObject optmenu;
    public GameObject optBG;
    int c = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void MenuOpen()
    {
        if (optmenu.activeInHierarchy == true)
        {
            optmenu.SetActive(false);
            optBG.SetActive(false);
        }
        else if (optmenu.activeInHierarchy == false)
        {
            optmenu.SetActive(true);
            optBG.SetActive(true);
        }
        StartCoroutine(enableDebug());
    }
    public void ResetOpt()
    {
        string localip = TestClient.instance.ip;
        int localport = TestClient.instance.port;
        TestClient.instance.Disconnect();
        SceneManager.LoadScene(0);
        TestClient.instance.ip = localip;
        TestClient.instance.port = localport;
    }
    public void DevSet()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator enableDebug()
    {
        c++;
        yield return new WaitForSeconds(1);
        if (c >= 5)
        {
            GameManagerScript.instance.debugStart();
            StopAllCoroutines();
        }
        else
        {
            c = 0;
            Debug.Log("reset");
        }
    }
}
