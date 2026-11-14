using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string OpenChestText = "[E] to Open";
    private Animator controller;

    private bool IsOpen = false;

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
        IsOpen = true;
        controller.SetBool("IsOpen", IsOpen);
    }

}
