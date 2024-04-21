using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IncineratorError : MonoBehaviour
{
    public TextMeshProUGUI errormsg;
    public GameObject bluescreen;
    public GameObject error;
    bool errormode;
    Animator cani;
    // Start is called before the first frame update
    void Start()
    {
        cani = GameObject.Find("Canvas").GetComponent<Animator>();
        cani.Play("BootUp");
        StartCoroutine(FirstDiag());
    }

    // Update is called once per frame
    void Update()
    {
        if (errormode == true)
        {
            int size = 50;
            for (int i = 0; i < size; i++)
            {
                float x = 32 + Mathf.Round(Random.value * 96.0f);
                char newmsg = (char)x;
                errormsg.text = errormsg.text + newmsg;
            }
        }
    }
    public void FinalDiag()
    {
        error.gameObject.SetActive(false);
        bluescreen.gameObject.SetActive(false);
        cani.Play("BootUp");
        StartCoroutine(GReveal());
    }
    IEnumerator GReveal()
    {
        yield return new WaitForSeconds(9f);
        error.gameObject.SetActive(true);
        errormsg.text = "You have been fired.";
    }
        IEnumerator FirstDiag()
    {
        errormsg.text = "";
        string fs = "Running Diagnostics...";
        bluescreen.gameObject.SetActive(false);
        for (int i = 0; i < fs.Length; i++)
        {
            char newmsg = fs[i];
            yield return new WaitForSeconds(0.1f);
            errormsg.text = errormsg.text + newmsg;
        }
        errormsg.text = "";
        bluescreen.gameObject.SetActive(true);
        int c = 0;
        float t = 0.3f;
        while (c < 15)
        {
            for (int i = 0; i < 4; i++)
            {
                char newmsg = fs[i];
                yield return new WaitForSeconds(t);
                errormsg.text = errormsg.text + newmsg;
            }
            errormsg.text = "";
            if (t > 0.1f) 
            {
                t = t - 0.05f;
            }
            c++;
            bluescreen.gameObject.SetActive(false);
        }
        errormode = true;
        bluescreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        errormode = false;
    }
}
