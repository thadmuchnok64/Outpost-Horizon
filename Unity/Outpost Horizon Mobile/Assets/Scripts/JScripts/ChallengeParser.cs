using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChallengeParser : MonoBehaviour
{
    public static ChallengeParser instance;
    public TextAsset challengeList;
    public TextMeshProUGUI challenges;
    public GameObject successGraphic;
    int seq;
    string[] currentText;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Multiple Challenge Parsers!");
            Destroy(this);
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentText = (challengeList.text.Split('\n'));
        foreach (string gs in currentText)
        {
            int i = 0;
            currentText[i] = currentText[i].Replace('~', '\n');
            i++;
        }
        challenges.text = currentText[seq];
    }

    // Update is called once per frame
    void Update()
    {
        challenges.text = currentText[seq];
    }
    public void OnChallengeSetComplete()
    {
        successGraphic.SetActive(true);
        StartCoroutine(waitProceed());
    }
    IEnumerator waitProceed()
    {
        yield return new WaitForSeconds(1);
        successGraphic.SetActive(false);
        seq++;
    }
}
