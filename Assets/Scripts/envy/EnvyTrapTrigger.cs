using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnvyTrapTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GhostRunner ghostRunner;
    [SerializeField] private Transform teleportDestination;

    [Header("Optional Environment Toggles")]
    [SerializeField] private GameObject fakeMirrorPlatforms;
    [SerializeField] private GameObject realSubLevelPit;

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private bool matchDestinationRotation = true;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag(playerTag)) return;

        triggered = true;

        // Unlink ghost runner so it leaves the player behind
        if (ghostRunner != null)
        {
            ghostRunner.TriggerTrap();
        }

        // Optional: toggle platforms if you still want them to switch
        if (fakeMirrorPlatforms != null)
        {
            fakeMirrorPlatforms.SetActive(false);
        }

        if (realSubLevelPit != null)
        {
            realSubLevelPit.SetActive(true);
        }

        // Teleport the player to the harder section
        if (teleportDestination != null)
        {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform player)
    {
        // Unity's CharacterController overrides transform.position unless temporarily disabled
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        // Reset momentum if the player uses a Rigidbody
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        player.position = teleportDestination.position;

        if (matchDestinationRotation)
        {
            player.rotation = teleportDestination.rotation;
        }

        if (controller != null)
        {
            controller.enabled = true;
        }
    }
}