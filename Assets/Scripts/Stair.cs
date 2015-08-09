using UnityEngine;
using System.Collections;

public class Stair : MonoBehaviour
{
    public Transform playerTransform;
    public FloorChanger floorChanger;
    public Stair exitStair;

    const int gap = 200;

    public void GoToExitStair()
    {
        Vector2 tempPos = exitStair.transform.position;
        tempPos.x += gap;
        playerTransform.position = tempPos;
        playerTransform.rotation = Quaternion.identity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            floorChanger.ChangeFloor(this);
        }
    }
}
