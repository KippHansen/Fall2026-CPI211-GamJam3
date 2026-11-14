using System.Collections;
using UnityEngine;

public class KeyChestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string OpenChestText = "[E] to Open";
    private Animator controller;

    private bool IsOpen = false;
    [SerializeField] private GameObject gateObject;
    [SerializeField] private float openDelay = 0.5f;
    [SerializeField] private bool disableGate = true;
    [SerializeField] private float moveUpDistance = 5.0f;
    [SerializeField] private float moveSpeed = 2.0f;


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
        StartCoroutine(OpenChestAndGate());
    }
    private IEnumerator OpenChestAndGate()
    {
    
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