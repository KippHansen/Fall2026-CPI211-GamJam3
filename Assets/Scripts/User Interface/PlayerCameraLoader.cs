using UnityEngine;

public class PlayerCameraLoader : MonoBehaviour
{
    // Drag your player's cameras here in the exact same order as the dropdown
    public Camera[] playerCameras; 
    private const string CameraPrefKey = "SelectedCameraIndex";

    void Start()
    {
        // Retrieve the saved index (defaults to 0)
        int selectedIndex = PlayerPrefs.GetInt(CameraPrefKey, 0);

        for (int i = 0; i < playerCameras.Length; i++)
        {
            if (playerCameras[i] != null)
            {
                // Enable only the selected camera and disable the rest
                playerCameras[i].gameObject.SetActive(i == selectedIndex);
            }
        }
    }
}
