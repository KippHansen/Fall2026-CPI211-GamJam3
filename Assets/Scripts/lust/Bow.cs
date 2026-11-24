using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("References")]
    public GameObject arrowPrefab;
    public Transform firePoint;

    [Header("Settings")]
    public float shootForce = 35f;
    public float fireCooldown = 0.4f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    void Shoot()
    {
        Quaternion spawnRotation = firePoint.rotation * Quaternion.Euler(90f, 0f, 0f);
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, spawnRotation);

        if (arrow.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);
        }
    }
}