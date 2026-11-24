using UnityEngine;

public class RicardioAI : MonoBehaviour, IDamageable
{
    [Header("Movement & Health")]
    public float moveSpeed = 3.5f;
    public float rotationSpeed = 6f;
    public int health = 2;

    [Header("Attack Settings")]
    public int damage = 20;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;

    private Transform player;
    private PlayerHealth playerHealth;
    private float nextAttackTime = 0f;

    void Start()
    {
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
        float distanceToPlayer = toPlayer.magnitude;

        if (distanceToPlayer > attackRange)
        {
            Vector3 direction = toPlayer.normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else if (playerHealth != null && Time.time >= nextAttackTime)
        {
            playerHealth.TakeDamage(damage);
            nextAttackTime = Time.time + attackCooldown;
        }
        if (toPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(toPlayer.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
}