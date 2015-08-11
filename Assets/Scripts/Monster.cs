using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    public Transform playerTransform;
    public float walkSpeed;
    public float chaseSpeed;

    //Rigidbody2D rb;
    Transform cachedTransform;

    PathFinder pathFinder;

    BoxCollider2D enemyCollider;
    CircleCollider2D enemySight;
    SpriteRenderer sprRenderer;

    // Use this for initialization
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        cachedTransform = base.transform;
        pathFinder = GetComponent<PathFinder>();
        enemyCollider = GetComponent<BoxCollider2D>();
        enemySight = GetComponent<CircleCollider2D>();

        StartCoroutine(Patrol());
    }


    IEnumerator Patrol()
    {
        Transform movePos = null;

        while (true)
        {
            if (movePos == null)
            {
                if (Floor.monsterFloor != null && Floor.monsterFloor == Floor.playerFloor)
                {
                    movePos = Floor.monsterFloor.GetRandomMonsterMovePos();
                    while (movePos.position == cachedTransform.position)
                    {
                        movePos = Floor.monsterFloor.GetRandomMonsterMovePos();
                    }
                    pathFinder.SetDestination(movePos.position, walkSpeed, 90f);
                }
            }
            else if (movePos.position == cachedTransform.position)
            {
                yield return new WaitForSeconds(4f);
                break;
            }
            yield return null;
        }

        StartCoroutine(Patrol());
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