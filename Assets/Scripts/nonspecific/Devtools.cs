using UnityEngine;
using UnityEngine.SceneManagement;

public class Devtools : MonoBehaviour
{
    private static Devtools instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Awake()
    {
        // Singleton pattern: prevent duplicate Devtools when returning to the initial scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
       #if UNITY_EDITOR || DEVELOPMENT_BUILD
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        //Next scene
        if (Input.GetKeyDown(KeyCode.Period))
        {
            int nextIndex = currentIndex + 1;
            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextIndex);
            }
            else
            {
                Debug.LogWarning("Devtools: Already at the last scene in Build Settings.");
            }
        }
        //previous scene
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            int prevIndex = currentIndex - 1;
            if (prevIndex >= 0)
            {
                SceneManager.LoadScene(prevIndex);
            }
            else
            {
                Debug.LogWarning("Devtools: Already at the first scene in Build Settings.");
            }
        }
        //Restart scene
         if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentIndex);
        }
#endif        
    }
}
