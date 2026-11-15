using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Mono.Cecil.Cil;
using UnityEngine.UIElements;


public class Sloth_ChoreList : MonoBehaviour
{
    // intro
    public GameObject instructionPanel;
    public GameObject ChoresPanel;
 // items in room
 public TMP_Text   CactusText;
  public TMP_Text   GuitarText;
   public TMP_Text   toothbrushText;  
   public TMP_Text   green_plantText;

   //floor clearing requirements
   public int ChoresNeeded;
   private int ChoresDone;
   // to block the player from leaving early
   public GameObject exitBarrier;

void Start()
    {
        
        if(instructionPanel != null)
        { // turns on instructions and freezes the room/gameplay
            instructionPanel.SetActive(true);
            Time.timeScale =0f;
        }
        if(ChoresPanel != null)
        {
            ChoresPanel.SetActive(false);
        }
        if(CactusText != null ) {
            CactusText.text = "-Cactus";
        }
        if(GuitarText.text != null ) {
            GuitarText.text = "-Guitar";
        }
        if(toothbrushText != null ) {
            toothbrushText.text = "-ToothBrush";
        }
        if(green_plantText != null ) {
            CactusText.text = "-Green plant";
        }
    }

public void Update()
    {
        if(instructionPanel != null){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                closePage();
            }
        }
    }

    public void closePage()
    {
        //closes panel
        instructionPanel.SetActive(false);
        // resumes game time
        Time.timeScale=1f;
        ChoresPanel.SetActive(true);

    }
     public void checkChore(GameObject other )

    {
        // deletes item
        Destroy(other);
        // adds 1 to total chore count
        ChoresDone++;
        
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "cactus":
            if(CactusText!= null)
                {
                    CactusText.text = "<s>-Cactus</s>";
                    checkChore(other.gameObject);
                    
                }
                    break;
                case "Guitar":
                if(GuitarText != null)
                {
                    GuitarText.text = "<s>-Guitar</s>"; 
                    checkChore(other.gameObject);
                }
                    break;

                case "toothbrush":
                if(toothbrushText != null)
                {
                    toothbrushText.text = "<s>toothbrush</s>";
                    checkChore(other.gameObject);
                }
                    break;
                    
                    case "green_plant":
                if(green_plantText != null)
                {
                    green_plantText.text = "<s>green_plant</s>";
                    checkChore(other.gameObject);
                }
                    break;
                }
        }
    }

   

