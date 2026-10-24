using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   private bool isPaused = false;
   public void PlayGame()
   {
    Time.timeScale = 1f;
    isPaused = false;
    SceneManager.LoadSceneAsync("Gluttony_Level");
   }
    
    public void Menu()
   {
    Time.timeScale = 1f;
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    SceneManager.LoadSceneAsync("Main_Menu");
   }
   
   public void Credits()
   {
    Time.timeScale = 1f;
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    SceneManager.LoadSceneAsync("Credits");
   }
   
   public void Score()
   {
    Time.timeScale = 1f;
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    SceneManager.LoadSceneAsync("Scoreboard");
   }
   
   public void Howto()
   {
    Time.timeScale = 1f;
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    SceneManager.LoadSceneAsync("Instructions");
   }


   public void QuitGame()
   {
    Application.Quit();
   }
}
