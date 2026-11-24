using UnityEngine;
using UnityEngine.SceneManagement; 
public class Move_level : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

        }
        
    }
}
