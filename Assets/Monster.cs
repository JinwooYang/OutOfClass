using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour 
{
    public Transform playerTransform;
    public float chaseSpeed;

    Rigidbody2D rb;

    PathFinder pathFinder;

    BoxCollider2D enemyCollider;
    CircleCollider2D enemySight;

	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
        pathFinder = GetComponent<PathFinder>();
        enemyCollider = GetComponent<BoxCollider2D>();
        enemySight = GetComponent<CircleCollider2D>();

        StartCoroutine(GoToPlayer());
	}
	

    IEnumerator GoToPlayer()
    {
        while(true)
        {
            pathFinder.SetDestination(playerTransform.position, chaseSpeed);
            yield return new WaitForSeconds(10f);
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


    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            rb.velocity = Vector2.zero;
        }
    }
}
