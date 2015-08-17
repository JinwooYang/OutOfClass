using UnityEngine;
using System.Collections;

public class MonsterHearingArea : MonoBehaviour
{
    public Monster monster;

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if(player.isRunning())
            {
                monster.Chasing();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        monster.ToIdle();
    }

}
