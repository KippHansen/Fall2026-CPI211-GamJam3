using System.Collections;
using UnityEngine;

public class Door_Trigger : MonoBehaviour
{
    //helps determine which answer is right
      public int doorIndex;

public Dialogue_script dialogueScript;

public Transform loopedspawn;


IEnumerator  OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            int answer = dialogueScript.doorOutcomes[doorIndex];
            if(answer == 2 )
            {
                
             dialogueScript.floorScore += 2;
             dialogueScript.removeRiddle();
             dialogueScript.riddlenum++;
             dialogueScript.maintext.text = "Correct";
             //wait
             
             yield return new WaitForSeconds(3.0f);
             dialogueScript.maintext.text = "proceed to the next question";
             yield return new WaitForSeconds(3.0f);

            
            }else if (answer == 1)
            { 
                dialogueScript.floorScore += 1;
                 dialogueScript.removeRiddle();
                dialogueScript.maintext.text = "A good answer....";
                yield return new WaitForSeconds(3.0f);
                dialogueScript.maintext.text = "But Wrong";
                yield return new WaitForSeconds(2.0f);


            }else if (answer == 0)
            {
                dialogueScript.floorScore += 0;
                 dialogueScript.maintext.text = "Incorrect";
                yield return new WaitForSeconds(3.0f);
               
                

            }

            // telepoting
            CharacterController cc = other.GetComponent<CharacterController>();
            if(cc != null)
            {
                cc.enabled = false;
            }
                        other.transform.position= loopedspawn.position;


            if(cc != null) cc.enabled= true;
            dialogueScript.uiCanvas.SetActive(false);
            for(int i =0; i < dialogueScript.doorTexts.Length; i++)
            {
                dialogueScript.doorTexts[i].gameObject.SetActive(false);
            }
        
dialogueScript.GetComponent<Collider>().enabled = true;
        
        }   
        }

}
