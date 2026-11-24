using System.Collections.Generic;
using UnityEngine;

public class RicardioSpawner : MonoBehaviour, IDamageable
{
    [Header("Spawning")]
    public GameObject ricardioPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 4f;
    public int maxAliveRicardios = 8;

    [Header("Health")]
    public int health = 5;

    private float timer;
    private readonly List<GameObject> activeRicardios = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRicardio();
            timer = 0f;
        }
    }

    void SpawnRicardio()
    {
        activeRicardios.RemoveAll(r => r == null);
        if (activeRicardios.Count >= maxAliveRicardios) return;

        Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
        GameObject ricardio = Instantiate(ricardioPrefab, pos, Quaternion.identity);
        activeRicardios.Add(ricardio);
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