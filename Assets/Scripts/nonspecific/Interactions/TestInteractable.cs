using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
   [SerializeField] private string InteractText = "[E] to Interact";

   public void Interact()
   {
    Debug.Log("Interacted");
   }
   public string InteractionText { get {return InteractText; } }
}
