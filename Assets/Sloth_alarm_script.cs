

using UnityEngine;

public class Sloth_alarm_script : MonoBehaviour
{
    // alaram variables 
    public int neededpresses = 5;
    private int currentpress = 0;
    public float timeBetweenAlarms = 8f;
    private float countdown;
    public bool inRange;

    // alarm visuals
    public bool isRinging = false;
    public Color quiet = Color.white;
    public Color  loud = Color.red;
    // commented out bc i dont have audio yet
    public AudioSource alarmAudio;

    private Renderer clockRenderer; 
    private Vector3 originalpos;
// makes a list of all clocks in room to manage them
    private static Sloth_alarm_script[] room_clocks;
    private bool leaderClock = false;
    void Start()
    {
        //make renderer so we can color the clocks
     clockRenderer = GetComponentInChildren<Renderer>();
     // getting local positioning for the clock shaking
     originalpos = transform.localPosition;
     // locate all objects that have the alarm script to find the clocks
     room_clocks= FindObjectsByType<Sloth_alarm_script>(FindObjectsSortMode.None);


     if( room_clocks.Length > 0 && room_clocks[0] == this)
        {
            leaderClock = true;
            countdown =  timeBetweenAlarms;
            
        }
        endRinging();
    }

    void Update()
    {
        if(leaderClock && !activeRinging())
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0f)
            {
                triggerRandomRing();
                countdown = timeBetweenAlarms;
            }
           
        }
         if (isRinging)
            {
                transform.localPosition= originalpos + Random.insideUnitSphere * 0.05f ;
                if(inRange && Input.GetKeyDown(KeyCode.E))
            {
                currentpress++;

                Debug.Log(" press need" + currentpress);
            }
            if(currentpress>= neededpresses)
            {
                endRinging();
            }
                }
        
    }
// helper methods 

void startRinging()
    {
        isRinging = true;
        if(clockRenderer != null)
        {
            clockRenderer.material.color = loud;
            alarmAudio.Play();
        }
        Debug.Log(gameObject.name + " is Ringing");
    }
    void endRinging()
    {
        isRinging = false; 
        currentpress=0;
        transform.localPosition = originalpos;
        if(clockRenderer != null)
        {
            clockRenderer.material.color= quiet;
        alarmAudio.Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange= false;

        }
    }
// checks if any clocks are ringing in room
 public static bool activeRinging()
    {
        foreach (var clock in room_clocks)
        {
            if (clock != null && clock.isRinging)
            {
                return true;
            }
        }
        return false;
    }

   void triggerRandomRing()
    {
        if(room_clocks.Length  == 0)
        {
            return ;
        }
       
       // set off random clock
       int randomIndex = Random.Range(0,room_clocks.Length);
       room_clocks[randomIndex].startRinging();
    }
}
