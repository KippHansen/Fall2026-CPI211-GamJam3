using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class Settingsmenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown cameraDropdown;
    private const string CameraPrefKey = "SelectedCameraIndex";

    public void SetMasterVolume (float masterVolume)
    {
        audioMixer.SetFloat("Volume", masterVolume);
    }
    public void SetMusicVolume (float musicVolume)
    {
        audioMixer.SetFloat("Music", musicVolume);
    }
    public void SetSFXVolume (float sfxVolume)
    {
        audioMixer.SetFloat("SFX", sfxVolume);
    }

    void Start()
    {
        // Load previously saved choice (defaults to 0 if none exists)
        int savedIndex = PlayerPrefs.GetInt(CameraPrefKey, 0);
        cameraDropdown.value = savedIndex;
        cameraDropdown.RefreshShownValue();

        // Save the new choice whenever the player changes the dropdown
        cameraDropdown.onValueChanged.AddListener(SaveCameraChoice);
    }
        public void SaveCameraChoice(int index)
    {
        PlayerPrefs.SetInt(CameraPrefKey, index);
        PlayerPrefs.Save();
    }
}
