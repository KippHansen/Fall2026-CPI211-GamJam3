using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;

    public void Spawn()
    {
        Instantiate(monsterPrefab, transform.position, transform.rotation);
    }
}