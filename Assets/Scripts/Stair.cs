using UnityEngine;
using System.Collections;

public class Stair : MonoBehaviour
{
    public Stair exitStair;

    const float gap = 200f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 tempPos = exitStair.transform.position;
            tempPos.x += gap;
            other.transform.position = tempPos;
            other.transform.rotation = Quaternion.identity;
        }
    }
}
