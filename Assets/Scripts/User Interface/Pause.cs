using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject PauseMenu;
    public player_controller playerScript;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
         if (!isPaused)
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (playerScript != null) playerScript.can_move = false;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (playerScript != null) playerScript.can_move = true; 
    }
}
