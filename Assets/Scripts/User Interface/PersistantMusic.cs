using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;
    private AudioSource audioSource;

    public string[] scenesToStopMusic;

    private void Awake()
    {
        // Prevent duplicate persistent music objects across reloads/scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            
            // Force play on first initialization if current scene allows it
            SceneManager.sceneLoaded += OnSceneLoaded;
            HandleMusicForScene(SceneManager.GetActiveScene());
        }
        else
        {
            Destroy(gameObject);
            return;
        }
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
            // Only start playing from the beginning if it was stopped completely 
            // (like coming from a scene where BGM wasn't allowed).
            // This will NOT restart the song if it's already playing during a reload.
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}