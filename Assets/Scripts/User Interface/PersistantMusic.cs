using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;
    private AudioSource audioSource;

    public string[] scenesToStopMusic;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // Prevent duplicate persistent music objects across reloads/scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        /*else
        {
            
        }*/
    }

    private void Start ()
    {
        HandleMusicForScene(SceneManager.GetActiveScene());

    }
    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleMusicForScene(scene);
    }

    private void HandleMusicForScene(Scene scene) 
{
    bool stopMusic = false;

    // Check if the current scene is a menu/stop scene
    foreach (string sceneName in scenesToStopMusic) 
    {
        if (scene.name == sceneName) 
        {
            stopMusic = true;
            break;
        }
    }

    if (stopMusic) 
    {
        if (audioSource.isPlaying) 
        {
            audioSource.Stop(); 
        }
    } 
    else 
    {
        // FIX: If we are in a level scene, make sure it is playing.
        // If it was stopped at the main menu, this forces it to start fresh.
        if (!audioSource.isPlaying) 
        {
            audioSource.Play(); 
        }
    }
}

}