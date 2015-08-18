using UnityEngine;
using System.Collections;

public enum MonsterState { NONE, IDLE, PATROL, CHASE, CHECK };

public class Monster : MonoBehaviour
{
    public Player player;
    public float walkSpeed;
    public float chaseSpeed;
    public float patrolWaitSeconds;

    Transform playerTransform;

    public MonsterState state = MonsterState.IDLE;

    float idleTimer = 0f;

    Transform cachedTransform;

    PathFinder pathFinder;

    public void CheckPlayerPos()
    {
        state = MonsterState.CHECK;
        pathFinder.SetDestination(Floor.monsterFloor.mapManager, playerTransform.position, chaseSpeed, 90f, delegate ()
        {
            state = MonsterState.IDLE;
        });
    }

    void Awake()
    {
        playerTransform = player.transform;
        cachedTransform = base.transform;
        pathFinder = GetComponent<PathFinder>();
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
    }

    void Chase()
    {
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

            case MonsterState.CHECK:
                //업데이트에서 처리할 건 없음.
                break;

            default:
                Debug.Assert(false, "확인되지 않은 값이 Monster의 스위치케이스에서 발견");
                break;
        }
    }
}