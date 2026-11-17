using UnityEngine;

public class GhostRunner : MonoBehaviour
{
    [Header("Target References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator ghostAnimator;

    [Header("Parallel Path Offset")]
    [Tooltip("Fixed lateral distance along the X-axis between player and ghost.")]
    [SerializeField] private float xOffset = 8f;

    [Tooltip("How far ahead the ghost runs to appear smoother/effortless.")]
    [SerializeField] private float zLeadDistance = 1.5f;

    [Tooltip("Smooth time for horizontal and vertical adjustment.")]
    [SerializeField] private float smoothTime = 0.08f;

    [Header("Settings")]
    [SerializeField] private bool matchPlayerY = true;
    [SerializeField] private float fixedYPosition = 0f;

    private Vector3 currentVelocity;
    private Vector3 lastPlayerPosition;
    private bool isTrapped = false;

    private void Start()
    {
        if (playerTransform != null)
        {
            lastPlayerPosition = playerTransform.position;
        }
    }

    private void Update()
    {
        if (isTrapped || playerTransform == null) return;

        // Calculate synchronized target position
        float targetX = playerTransform.position.x + xOffset;
        float targetY = matchPlayerY ? playerTransform.position.y : fixedYPosition;
        float targetZ = playerTransform.position.z + zLeadDistance;

        Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);

        // Smoothly position ghost on parallel axis
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // Match forward orientation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), Time.deltaTime * 12f);

        // Pass calculated movement speed to Animator
        if (ghostAnimator != null)
        {
            float playerSpeed = (playerTransform.position - lastPlayerPosition).magnitude / Time.deltaTime;
            ghostAnimator.SetFloat("Speed", playerSpeed);
        }

        lastPlayerPosition = playerTransform.position;
    }

    /// <summary>
    /// Called when player crosses the boundary into the trap zone.
    /// </summary>
    public void TriggerTrap()
    {
        if (isTrapped) return;
        isTrapped = true;

        // Ghost keeps running forward into the distance while player falls
        if (ghostAnimator != null)
        {
            ghostAnimator.SetFloat("Speed", 5f); // Constant running speed
        }
    }
}