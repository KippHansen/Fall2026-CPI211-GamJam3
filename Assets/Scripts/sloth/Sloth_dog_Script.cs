using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sloth_dog_Script : MonoBehaviour
{
    // the health and drain
    public float maxSleep = 100f;
    public float currentSleep;
    public float sleep_drain = 5f;
    public Slider sleepBar;

    //dog animations
    private Animator dogAnimation;
    public Transform player;
    public float sprintingSpeed = 3f;
    public float catchdist= 3f;
    private bool isAwake=false;
    public AudioSource growl;

    void Start()
    {
        currentSleep = maxSleep;
        dogAnimation=GetComponent<Animator>();

        if(sleepBar != null)
        {
            sleepBar.maxValue=maxSleep;
            sleepBar.value = currentSleep;
        }
    }

void Update()
    {
        // Sleeping phase
        if (!isAwake)
        {
            if (Sloth_alarm_script.activeRinging())
            {
                currentSleep -= sleep_drain *Time.deltaTime;
                currentSleep= Mathf.Max(0f, currentSleep);

                if(sleepBar != null)
                {
                    sleepBar.value = currentSleep;
                }
                if(currentSleep <= 0f)
                {
                    getUp();
                }

            } 
        }
        else
        {
            if(player != null)
            {
                Vector3 targetPos= new Vector3(player.position.x,transform.position.y,player.position.z);

                transform.LookAt(targetPos);
                transform.Rotate(0f,180f,0f);
                transform.position = Vector3.MoveTowards(transform.position,targetPos, sprintingSpeed * Time.deltaTime);

                //reset level if dead
                if(Vector3.Distance(transform.position, targetPos) <= catchdist)
                {
                    Debug.Log("Beast got you");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }

            }
        }
        
    }
    void getUp()
        {
            isAwake = true;
            if(dogAnimation != null)
            {
                // transisitons from lay to run
                dogAnimation.SetBool("isAwake", true);
            }
            Debug.Log("The dog is awake!");
            if(growl != null)
        {
            growl.Play();
        }
        }

    }

