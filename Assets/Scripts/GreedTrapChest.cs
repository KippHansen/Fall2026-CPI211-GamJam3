using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GreedTrapChest : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private Animation legacyAnimation;
    [SerializeField] private string animationClipName = "ChestAnim";

    [Header("UI Settings")]
    [SerializeField] private GameObject youFoolPanel;
    [SerializeField] private float textDisplayDuration = 2.0f;

    [Header("Scene Loading Settings")]
    [Tooltip("Exact name of the scene you want to load, e.g., Pride_Level")]
    [SerializeField] private string previousSceneName = "Pride_Level";

    private bool isOpened = false;

    private void Start()
    {
        if (legacyAnimation == null)
        {
            legacyAnimation = GetComponent<Animation>();
        }
        if (legacyAnimation != null && legacyAnimation.isPlaying)
        {
            legacyAnimation.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            StartCoroutine(TriggerGreedTrap());
        }
    }

    private IEnumerator TriggerGreedTrap()
    {
        isOpened = true;
        if (legacyAnimation != null && !string.IsNullOrEmpty(animationClipName))
        {
            legacyAnimation.Play(animationClipName);
        }
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