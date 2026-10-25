//*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System.Runtime.CompilerServices;


public class Dialogue_script: MonoBehaviour
{
    public int floorScore;
   public int riddlenum = 1;

    //for the text display
public GameObject uiCanvas;
 public TextMeshProUGUI maintext;
 public TMP_Text[]  doorTexts;
 //

 // for the riddle bank
public RiddleData[] riddles;
public int[] doorOutcomes = new int[3];
 //
 private List<RiddleData> availableRiddles = new List<RiddleData>();
 public RiddleData currentRiddle;
 public int currRiddleIndex;

 void Start()
    {
        availableRiddles = new List<RiddleData>(riddles);
    }


    private void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Player"))
        {
            uiCanvas.SetActive(true);
            StartCoroutine(PlayIntro());

            GetComponent<Collider>().enabled=false;
        }
    }
    //this plays the intro and puts waits bewtween each text
    IEnumerator PlayIntro()
    {
        maintext.text = "Welcome to the Wrath floor...";
        yield return new WaitForSeconds(3.0f);

        maintext.text= "To proceed....";

        yield return new WaitForSeconds(3.0f);
        maintext.text= "riddle me this?";
        yield return new WaitForSeconds(3.0f);
        RunRiddle();
    }
    // everything past this is for the riddles

    void RunRiddle()
    {
        if (availableRiddles.Count == 0)
        {
            availableRiddles = new List<RiddleData>(riddles);
        }
        // first pick a random riddle from the riddle bank
        int randomIndex= Random.Range(0, availableRiddles.Count);
      currentRiddle = availableRiddles[randomIndex];

      maintext.text = "Riddle" + riddlenum + ":\n" + currentRiddle.question;

// shuffles the answers 
        List<int>outcomes = new List<int>{0,1,2};
  for(int i =0; i < outcomes.Count; i++)
        {
            int temp = outcomes[i]; 
            int rand = Random.Range(i,outcomes.Count);
            outcomes[i] = outcomes[rand];
            outcomes[rand]=temp;
        }
        // asigne the shuffled answers to doors
for(int i = 0 ; i<3; i++)
        {
            doorOutcomes[i] = outcomes[i];
            if (outcomes[i] == 2)
            {
                doorTexts[i].text = currentRiddle.correctAnswer;

            }
            else if(outcomes[i] == 1)
            {
                doorTexts[i].text = currentRiddle.slightwrongAnswer;
            }
            else if(outcomes[i] == 0)
            {
                doorTexts[i].text = currentRiddle.dumbAnswer;
            }
            doorTexts[i].gameObject.SetActive(true);
        }
    }
     public void removeRiddle()
{
    if( availableRiddles.Contains(currentRiddle))
    {
        availableRiddles.Remove(currentRiddle);
    }
}

}
[System.Serializable]
public class RiddleData
    {
        public string question;
        public string correctAnswer;
        public string slightwrongAnswer;
        public string dumbAnswer;
    }

   

    // make sure riddles dont repeat as you progress
    //looping system
    //tracking proggression
    //
    