using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform player;
    private NavMeshAgent agent;
    private int currentWaypoint = 0;
    public float detectionRange = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PatrolToNextPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            agent.SetDestination(player.position);  // Persigue al jugador
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            PatrolToNextPoint();  // Sigue patrullando
        }
    }

    void PatrolToNextPoint()
    {
        if (waypoints.Length == 0) return;

        agent.SetDestination(waypoints[currentWaypoint].position);
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }
}
