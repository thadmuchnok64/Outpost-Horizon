using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public GameObject optmenu;
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
        }
        else if (optmenu.activeInHierarchy == false)
        {
            optmenu.SetActive(true);
        }
        StartCoroutine(enableDebug());
    }
    public void ResetOpt()
    {
        SceneManager.LoadScene(0);
    }
    public void DevSet()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator enableDebug()
    {
        c++;
        yield return new WaitForSeconds(3);
        if (c >= 10)
        {
            GameManagerScript.instance.debugStart();
            StopAllCoroutines();
        }
    }
}
