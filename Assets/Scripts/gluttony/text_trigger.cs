using UnityEngine;
using UnityEngine.Events;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    [SerializeField] private float showDuration = 3f;
    [SerializeField] private bool triggerOnce = true;

    [Header("Monster")]
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform monsterSpawnPoint;

    public UnityEvent onTriggered;

    private bool used;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (triggerOnce && used) return;
        used = true;

        textObject.SetActive(true);
        SpawnMonster();
        onTriggered.Invoke();
        Invoke(nameof(HideText), showDuration);
    }

    void SpawnMonster()
    {
        if (monsterPrefab == null || monsterSpawnPoint == null)
        {
            Debug.LogError("Monster Prefab or Spawn Point is not assigned.");
            return;
        }

        Instantiate(monsterPrefab, monsterSpawnPoint.position, monsterSpawnPoint.rotation);
    }

    void HideText()
    {
        textObject.SetActive(false);
    }
}