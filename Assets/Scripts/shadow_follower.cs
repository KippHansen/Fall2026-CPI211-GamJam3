using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShadowFollower : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float recordSpacing = 0.15f;   // smaller = more accurate jump arcs
    [SerializeField] private float startDelay = 1f;
    [SerializeField] private float catchUpDistance = 10f;   // speeds up if it falls this far behind
    [SerializeField] private float catchUpMultiplier = 1.5f;

    private Transform player;
    private readonly Queue<Vector3> trail = new Queue<Vector3>();
    private Vector3 lastRecorded;
    private float timer;
    private bool caught;

    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        if (p == null)
        {
            Debug.LogError("ShadowFollower: no object tagged 'Player' found.");
            enabled = false;
            return;
        }

        player = p.transform;
        lastRecorded = player.position;
        trail.Enqueue(lastRecorded);
    }

    void LateUpdate()
    {
        if (caught) return;

        // 1. Record the player's path, including height (this is what captures jumps)
        if (Vector3.Distance(player.position, lastRecorded) >= recordSpacing)
        {
            lastRecorded = player.position;
            trail.Enqueue(lastRecorded);
        }

        // 2. Wait a moment after spawning, then walk the trail
        timer += Time.deltaTime;
        if (timer < startDelay) return;

        float currentSpeed = speed;
        if (Vector3.Distance(transform.position, player.position) > catchUpDistance)
            currentSpeed *= catchUpMultiplier;

        float moveBudget = currentSpeed * Time.deltaTime;

        // Use the full move budget, possibly passing several trail points in one frame
        while (moveBudget > 0f && trail.Count > 0)
        {
            Vector3 target = trail.Peek();
            Vector3 toTarget = target - transform.position;
            float dist = toTarget.magnitude;

            // Face the direction of travel (horizontal only)
            Vector3 flat = new Vector3(toTarget.x, 0f, toTarget.z);
            if (flat.sqrMagnitude > 0.0001f)
                transform.rotation = Quaternion.LookRotation(flat);

            if (dist <= moveBudget)
            {
                transform.position = target;
                moveBudget -= dist;
                trail.Dequeue();
            }
            else
            {
                transform.position += toTarget / dist * moveBudget;
                moveBudget = 0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (caught) return;
        if (!other.CompareTag("Player")) return;

        caught = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}