using UnityEngine;

public class RicardioAI : MonoBehaviour, IDamageable
{
    [Header("Movement & Health")]
    public float moveSpeed = 3.5f;
    public float rotationSpeed = 6f;
    public float facingAngleOffset = 0f;
    public int health = 2;

    [Header("Attack Settings")]
    public int damage = 20;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;

    private Transform player;
    private PlayerHealth playerHealth;
    private Rigidbody rb;
    private float nextAttackTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerObj.TryGetComponent<PlayerHealth>(out playerHealth);
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        if (toPlayer.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(toPlayer.normalized) * Quaternion.Euler(0f, facingAngleOffset, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (toPlayer.magnitude <= attackRange && playerHealth != null && Time.time >= nextAttackTime)
        {
            playerHealth.TakeDamage(damage);
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void FixedUpdate()
    {
        if (player == null || rb == null) return;

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        float verticalVel = Mathf.Min(rb.linearVelocity.y, 0f);

        if (toPlayer.magnitude > attackRange)
        {
            Vector3 direction = toPlayer.normalized;
            rb.linearVelocity = new Vector3(direction.x * moveSpeed, verticalVel, direction.z * moveSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector3(0f, verticalVel, 0f);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}