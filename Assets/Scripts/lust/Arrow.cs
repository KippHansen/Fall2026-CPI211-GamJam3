using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int amount);
}

public class Arrow : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;

        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}