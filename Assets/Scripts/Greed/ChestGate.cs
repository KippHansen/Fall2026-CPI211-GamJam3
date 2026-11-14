using System.Collections;
using UnityEngine;

public class ChestGate : MonoBehaviour
{
    [Header("Chest Animation")]
    [SerializeField] private Animation legacyAnimation;
    [SerializeField] private string animationClipName = "ChestAnim";

    [Header("Gate Settings")]
    [Tooltip("Drag your Gate object here")]
    [SerializeField] private GameObject gateObject;
    [Tooltip("Delay in seconds before the gate opens after chest opens")]
    [SerializeField] private float openDelay = 0.5f;

    [Header("Gate Action Type")]
    [Tooltip("If true, disables the gate object. If false, moves it upward.")]
    [SerializeField] private bool disableGate = true;
    [SerializeField] private float moveUpDistance = 5.0f;
    [SerializeField] private float moveSpeed = 2.0f;

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
            StartCoroutine(OpenChestAndGate());
        }
    }

    private IEnumerator OpenChestAndGate()
    {
        isOpened = true;
        if (legacyAnimation != null && !string.IsNullOrEmpty(animationClipName))
        {
            legacyAnimation.Play(animationClipName);
        }
        yield return new WaitForSeconds(openDelay);
        if (gateObject != null)
        {
            if (disableGate)
            {
                gateObject.SetActive(false);
            }
            else
            {
                Vector3 startPos = gateObject.transform.position;
                Vector3 endPos = startPos + Vector3.up * moveUpDistance;
                float progress = 0f;

                while (progress < 1f)
                {
                    progress += Time.deltaTime * moveSpeed;
                    gateObject.transform.position = Vector3.Lerp(startPos, endPos, progress);
                    yield return null;
                }
            }
        }
        else
        {
            Debug.LogWarning("Gate Object is not assigned on " + gameObject.name);
        }
    }
}