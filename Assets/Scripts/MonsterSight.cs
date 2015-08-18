using UnityEngine;
using System.Collections;

public class MonsterSight : MonoBehaviour
{
    public LayerMask collideLayer;

    Transform cachedTransform;
    Monster monster;

    void Awake ()
    {
        cachedTransform = base.transform;
        monster = cachedTransform.parent.GetComponent<Monster>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            RaycastHit2D hit;

            Debug.DrawRay(cachedTransform.position, other.transform.position - cachedTransform.position);
            hit = Physics2D.Raycast(cachedTransform.position, other.transform.position - cachedTransform.position, Mathf.Infinity, collideLayer);

            if(hit.collider != null)
            {
                print(hit.collider.gameObject.name);
                if(hit.collider.CompareTag("Player"))
                {
                    monster.state = MonsterState.CHASE;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (monster.state == MonsterState.CHASE && other.CompareTag("Player"))
        {
            monster.state = MonsterState.IDLE;
        }
    }
}
