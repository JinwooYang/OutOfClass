using UnityEngine;
using System.Collections;


public class Monster : MonoBehaviour
{
    public Transform playerTransform;
    public float walkSpeed;
    public float chaseSpeed;
    public float patrolWaitSeconds;

    //Rigidbody2D rb;
    enum MonsterState { NONE, IDLE, PATROL, CHASE };
    MonsterState state = MonsterState.IDLE;

    float idleTimer = 0f;

    Transform cachedTransform;

    PathFinder pathFinder;

    public void ToIdle()
    {
        state = MonsterState.IDLE;
    }

    public void Chasing()
    {
        state = MonsterState.CHASE;
    }

    public bool IsChasing()
    {
        return (state == MonsterState.CHASE);
    }

    // Use this for initialization
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        cachedTransform = base.transform;
        pathFinder = GetComponent<PathFinder>();
        // StartCoroutine(StateControl());
    }

    void Idle()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer > patrolWaitSeconds)
        {
            idleTimer = 0f;
            state = MonsterState.PATROL;
        }
    }

    void Patrol()
    {
        if (Floor.monsterFloor != null && !pathFinder.isMoving)
        {
            if(Floor.monsterFloor != Floor.playerFloor)
            {
                Invoke("MoveToPlayerFloor", Random.Range(5f, 30f));
                state = MonsterState.NONE;
                return;
            }

            Transform movePos = Floor.monsterFloor.GetRandomMonsterMoveTransform();
            while (movePos.position == cachedTransform.position)
            {
                movePos = Floor.monsterFloor.GetRandomMonsterMoveTransform();
            }

            pathFinder.SetDestination(Floor.monsterFloor.mapManager, movePos.position, walkSpeed, 90f, delegate()
            {
                state = MonsterState.IDLE;
            });
        }
        //StartCoroutine(Patrol());
    }

    void Chase()
    {
        print("chase");
        pathFinder.SetDestination(Floor.monsterFloor.mapManager, playerTransform.position, chaseSpeed, 90f);
    }

    void MoveToPlayerFloor()
    {
        cachedTransform.position = Floor.playerFloor.GetRandomMonsterMoveTransform().position;
        state = MonsterState.IDLE;
    }

    void Update()
    {
        switch (state)
        {
            case MonsterState.NONE:
                break;

            case MonsterState.IDLE:
                Idle();
                break;

            case MonsterState.PATROL:
                Patrol();
                break;

            case MonsterState.CHASE:
                Chase();
                break;

            default:
                break;
        }
    }

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        Vector2 moveDirection = (other.transform.position - transform.position).normalized;
    //        //rb.velocity = moveDirection * chaseSpeed;

    //        float radAngle = Mathf.Atan2(moveDirection.y, moveDirection.x);
    //        rb.velocity = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle)) * chaseSpeed;

    //        transform.rotation = Quaternion.Euler(0f, 0f, radAngle * Mathf.Rad2Deg + 90f);
    //    }
    //}


    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        rb.velocity = Vector2.zero;
    //    }
    //}
}