using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPlane : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    private bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        if (string.IsNullOrEmpty(nextSceneName))
        {
            // No name given: load the next scene in Build Settings
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}