using UnityEngine;
using TMPro;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private float Range = 5f;
    [SerializeField] private LayerMask LayerMask;

    [SerializeField] private TMP_Text InteractionText;

    private Camera mainCamera;
    private Animator controller;

    private void Start()
    {
        mainCamera = Camera.main;
        controller = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleRaycasting();
    }

    private void HandleRaycasting()
    {        
        RaycastHit hit;
        if (!Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Range, LayerMask))
        return;

        if (!hit.collider.TryGetComponent(out IInteractable interactable))
        {
            InteractionText.text = "";
            return;
        }

        InteractionText.text = interactable.InteractionText;
        if (Input.GetKeyDown(KeyCode.E))
        {
            controller.SetTrigger("Interact");
            interactable.Interact();
        }
    }
}
