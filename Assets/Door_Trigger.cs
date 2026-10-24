using UnityEngine;

public class Door_Trigger : MonoBehaviour
{
    //helps determine which answer is right
      public int doorIndex;

public Dialogue_script dialogueScript;

public Transform loopedspawn;


public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player"))
        {
            int answer = dialogueScript.doorOutcomes[doorIndex];
            if(answer == 0 )
            {
                
             dialogueScript.floorScore += 2;
            }else if (answer == 1)
            { dialogueScript.removeRiddle();
                dialogueScript.floorScore += 1;
                 dialogueScript.removeRiddle();
            }else if (answer == 0)
            {
                dialogueScript.floorScore +=0;
            }

            // telepoting
            CharacterController cc = other.GetComponent<CharacterController>();
            other.transform.position= loopedspawn.position;

            if(cc != null) cc.enabled= true;
            dialogueScript.uiCanvas.SetActive(false);
        

        }  
        Transform targetSpawn = loopedspawn;  
        }

}
