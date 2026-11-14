using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TrapChestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string OpenChestText = "[E] to Open";
    private Animator controller;

    private bool IsOpen = false;   

    [SerializeField] private GameObject youFoolPanel;
    [SerializeField] private float textDisplayDuration = 2.0f;

    [Tooltip("Exact name of the scene you want to load, e.g., Pride_Level")]
    [SerializeField] private string previousSceneName = "Pride_Level"; 
    
  void Start()
    {
        controller = GetComponentInChildren<Animator>();
    }

    public string InteractionText
    {
        get
        {
            return !IsOpen ? OpenChestText : "";
        }
    }
  public void Interact()
    {
        if (IsOpen) return;
        IsOpen = true;
        controller.SetBool("IsOpen", IsOpen);
        StartCoroutine(TriggerGreedTrap());
    }
    private IEnumerator TriggerGreedTrap()
    {
        yield return new WaitForSeconds(3.0f);
        if (youFoolPanel != null)
        {
            youFoolPanel.SetActive(true);
        }
        yield return new WaitForSeconds(textDisplayDuration);
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogError("Previous Scene Name is not set in the Inspector!");
        }
    }
}
