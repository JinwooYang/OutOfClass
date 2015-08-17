using UnityEngine;
using System.Collections;

public class MonsterSight : MonoBehaviour
{
    public Monster monster;
    public LayerMask collideLayer;

    Transform cachedTransform;

	// Use this for initialization
	void Awake ()
    {
        cachedTransform = base.transform;
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
                    monster.Chasing();
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (monster.IsChasing() && other.CompareTag("Player"))
        {
            monster.ToIdle();
        }
    }
}
