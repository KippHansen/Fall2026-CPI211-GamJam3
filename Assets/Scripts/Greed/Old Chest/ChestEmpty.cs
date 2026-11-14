using System.Collections;
using UnityEngine;

public class ChestEmpty : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private Animation legacyAnimation;
    [SerializeField] private string animationClipName = "ChestAnim";

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
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true;

        if (legacyAnimation != null && !string.IsNullOrEmpty(animationClipName))
        {
            legacyAnimation.Play(animationClipName);
        }
    }
}