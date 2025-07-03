using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Wandering,
    Chasing,
    Attacking
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    [Header("General Settings")]
    public float wanderRadius = 10f;
    public float idleDuration = 2f;
    public float attackRange = 2f;

    [Header("Vision Settings")]
    public float viewDistance = 10f;
    public float fieldOfView = 120f;
    public LayerMask playerLayer;
    public LayerMask obstacleMask;

    private NavMeshAgent agent;
    private Animator anim;
    private Transform player;
    private EnemyState currentState = EnemyState.Idle;

    private float idleTimer = 0f;
    private bool destinationSet = false;
    private Vector3 destination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogError("Player with tag 'Player' not found!");

        idleTimer = idleDuration;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Vision check
        bool canSeePlayer = CanSeePlayer();

        // State transitions
        if (distanceToPlayer <= attackRange)
        {
            currentState = EnemyState.Attacking;
            destinationSet = false;
        }
        else if (canSeePlayer)
        {
            currentState = EnemyState.Chasing;
            destinationSet = false;
        }
        else if (currentState != EnemyState.Wandering && currentState != EnemyState.Idle)
        {
            currentState = EnemyState.Idle;
            idleTimer = idleDuration;
            destinationSet = false;
        }

        // State behavior
        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Wandering:
                Wander();
                break;
            case EnemyState.Chasing:
                Chase();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
        }
    }

    void Idle()
    {
        agent.SetDestination(transform.position); // Stop
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);

        // Optional look-around effect
        transform.Rotate(0, 30 * Time.deltaTime, 0);

        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0)
        {
            currentState = EnemyState.Wandering;
        }
    }

    void Wander()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);

        if (!destinationSet || agent.remainingDistance < 1f)
        {
            Vector3 randomDir = Random.insideUnitSphere * wanderRadius + transform.position;
            if (NavMesh.SamplePosition(randomDir, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
            {
                destination = hit.position;
                agent.SetDestination(destination);
                destinationSet = true;
                anim.SetBool("isWalking", true);
            }
        }
    }

    void Chase()
    {
        if (player == null) return;

        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", true);
        anim.SetBool("isAttacking", false);

        agent.SetDestination(player.position);
    }

    void Attack()
    {
        if (player == null) return;

        agent.SetDestination(transform.position); // Stop
        transform.LookAt(player);

        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", true);

        // TODO: Add damage or attack logic here
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if (angle < fieldOfView / 2f && distToPlayer <= viewDistance)
        {
            // Raycast to check line of sight
            Vector3 origin = transform.position + Vector3.up * 1.5f;
            if (!Physics.Raycast(origin, dirToPlayer, distToPlayer, obstacleMask))
            {
                return true; // Player is visible
            }
        }
        return false;
    }

    // Debug vision cone
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 left = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, fieldOfView / 2, 0) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, left * viewDistance);
        Gizmos.DrawRay(transform.position, right * viewDistance);
    }
}
