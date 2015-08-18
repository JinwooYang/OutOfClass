using UnityEngine;
using System.Collections;

public class MonsterHearingArea : MonoBehaviour
{
    Transform cachedTransform;
    Monster monster;

    void Awake()
    {
        cachedTransform = base.transform;
        monster = cachedTransform.parent.GetComponent<Monster>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (player.isRunning())
            {
                monster.CheckPlayerPos();
            }
        }
    }
}
