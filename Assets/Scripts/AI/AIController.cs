using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public GameObject HandBoxCollider;
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
        _anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0.1f;
        _anim.applyRootMotion = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        GoToNextWaypoint();
    }

    void Update()
    {
        if (_anim.GetBool("isDead")) return;

        if (currentState != lastState)
        {
            Debug.Log("AI State changed to: " + currentState);
            lastState = currentState;
        }

        if (CanSeePlayer())
        {
            lastSeenPosition = player.position;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
                currentState = State.Attack;
            else
                currentState = State.Chase;
        }

        switch (currentState)
        {
            case State.Patrol: PatrolBehavior(); break;
            case State.Chase: ChaseBehavior(); break;
            case State.Search: SearchBehavior(); break;
            case State.Attack: AttackBehavior(); break;
        }

        UpdateAnimator();
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
                    return true;
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

        agent.SetDestination(transform.position);
        transform.LookAt(player);

        float distance = Vector3.Distance(transform.position, player.position);

        _anim.SetBool("isAttacking", true);
        HandBoxCollider.SetActive(true);

        if (distance > attackRange + 1f)
        {
            CancelAttack();
            currentState = State.Chase;
        }
    }

    public void CancelAttack()
    {
        _anim.SetBool("isAttacking", false);
        if (HandBoxCollider != null)
            HandBoxCollider.SetActive(false);
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
}
