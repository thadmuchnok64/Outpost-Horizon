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
    int seq = 0;
    int compChallenge;
    int[] numOfChallenge;
    string[] currentText;
    Dictionary<int, string> challengeDictionary;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Multiple Challenge Parsers!");
            Destroy(this);
        }
        instance = this;
		challengeDictionary = new Dictionary<int, string>();

	}
	// Start is called before the first frame update
	void Start()
    {
        currentText = (challengeList.text.Split('\n'));
		int i = 0;
		foreach (string gs in currentText)
        {
            challengeDictionary.Add(i, gs);
            // A lamda expression? I'm so impressed JJ! :D
            //numOfChallenge[i] = currentText[i].Count(t => t == ',');
            //currentText[i] = currentText[i].Replace(',', '\n');
            i++;
        }
        challenges.text = currentText[seq];
    }

    public void OnChallengeComplete(int chnum)
    {
        successGraphic.SetActive(true);
        compChallenge++;
        if (compChallenge == numOfChallenge[seq])
        {
            OnChallengeSetComplete();
        }
    }

    public void TryCompleteChallenge(int challengeId)
    {
        if (challengeId <= seq)
            return;
        seq = challengeId;
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
		challenges.text = currentText[seq];
	}
}
