using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour
{
    private bool triggered;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched " + other.name);
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}