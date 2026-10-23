using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    void Start()
    {
        Debug.Log("PlayerSpawner is running on: " + gameObject.name);

        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab is EMPTY. Drag your prefab into the field.");
            return;
        }

        GameObject p = Instantiate(playerPrefab, transform.position, transform.rotation);
        Debug.Log("Spawned " + p.name + " at " + p.transform.position);
    }
}