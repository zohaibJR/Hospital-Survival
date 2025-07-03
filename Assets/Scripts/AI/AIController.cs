using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public SphereCollider handCollider;

    public Animator _anim;

    public NavMeshAgent agent;
    public Transform[] wayPoints;

    [Header("Movement Settings")]
    public float walkSpeed = 3.5f;
    public float runSpeed = 7f;
    public float waitTimeAtWaypoint = 3f;

    [Header("Vision Settings")]
    public float viewRadius = 15f;
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    [Header("Combat Settings")]
    public float attackRange = 2f;

    private Transform player;
    private Vector3 lastSeenPosition;
    private float waitTimer = 0f;
    private float lostPlayerTimer = 0f;
    private float lostPlayerWaitTime = 3f;

    private int currentWaypointIndex = 0;

    private enum State { Patrol, Chase, Search, Attack }
    private State currentState = State.Patrol;
    private State lastState = (State)(-1);

    void Start()
    {
        handCollider = GetComponent<SphereCollider>();
        if (handCollider != null)
        {
            handCollider.enabled = false;
        }
        _anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0.1f;
        _anim.applyRootMotion = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (Vector3.Distance(transform.position, wayPoints[currentWaypointIndex].position) < 1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Length;
        }

        GoToNextWaypoint();
    }

    void Update()
    {
        if (currentState != lastState)
        {
            Debug.Log("AI State changed to: " + currentState);
            lastState = currentState;
        }

        if (CanSeePlayer() && currentState != State.Attack)
        {
            lastSeenPosition = player.position;

            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                currentState = State.Attack;
                return;
            }

            currentState = State.Chase;
        }

        switch (currentState)
        {
            case State.Patrol:
                PatrolBehavior();
                break;

            case State.Chase:
                ChaseBehavior();
                break;

            case State.Search:
                SearchBehavior();
                break;

            case State.Attack:
                AttackBehavior();
                break;
        }

        UpdateAnimator(); // <-- Important!
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= viewRadius)
        {
            float angle = Vector3.Angle(transform.forward, dirToPlayer);
            if (angle <= viewAngle / 2f)
            {
                if (!Physics.Raycast(transform.position, dirToPlayer, distanceToPlayer, obstacleMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    void PatrolBehavior()
    {
        agent.speed = walkSpeed;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTimeAtWaypoint)
            {
                GoToNextWaypoint();
                waitTimer = 0f;
            }
        }
    }

    void ChaseBehavior()
    {
        if (player == null)
        {
            currentState = State.Patrol;
            return;
        }

        agent.speed = runSpeed;
        agent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            currentState = State.Attack;
        }
        else if (distance > viewRadius + 2f || Physics.Raycast(transform.position, (player.position - transform.position).normalized, distance, obstacleMask))
        {
            lostPlayerTimer = lostPlayerWaitTime;
            currentState = State.Search;
        }
    }

    void SearchBehavior()
    {
        agent.speed = walkSpeed;
        agent.SetDestination(lastSeenPosition);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            lostPlayerTimer -= Time.deltaTime;
            if (lostPlayerTimer <= 0f)
            {
                currentState = State.Patrol;
                GoToNextWaypoint();
            }
        }
    }

    void AttackBehavior()
    {
        if (player == null) return;

        // Stop agent movement and rotate toward player
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        float distance = Vector3.Distance(transform.position, player.position);

        // Play attack animation
        _anim.SetBool("isAttacking", true);
        ActivateHandCollider();


        if (distance > attackRange + 1f)
        {
            // Exit attack if player escapes
            _anim.SetBool("isAttacking", false);
            DeactivateHandCollider();
            currentState = State.Chase;
            return;
        }

        // Optional: You can add damage logic here
        Debug.Log("Attacking Player!");
    }

    void UpdateAnimator()
    {
        float speed = agent.velocity.magnitude;

        bool isWalking = speed > 0.1f && speed <= walkSpeed + 0.2f;
        bool isRunning = speed > walkSpeed + 0.2f;
        bool isAttacking = (currentState == State.Attack);

        _anim.SetBool("isWalking", isWalking);
        _anim.SetBool("isRunning", isRunning);
        _anim.SetBool("isAttacking", isAttacking);
    }


    void GoToNextWaypoint()
    {
        if (wayPoints.Length == 0) return;

        agent.destination = wayPoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Length;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 angleA = DirFromAngle(-viewAngle / 2);
        Vector3 angleB = DirFromAngle(viewAngle / 2);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + angleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + angleB * viewRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    Vector3 DirFromAngle(float angleInDegrees)
    {
        angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }


    public void ActivateHandCollider()
    {
        if (handCollider != null)
        {
            handCollider.enabled = true;
            Debug.Log("Hand collider activffated");
        }
    }


    public void DeactivateHandCollider()
    {
        if (handCollider != null)
        {
            handCollider.enabled = false;
            Debug.Log("Hand collider deactivated");
        }
    }


}
