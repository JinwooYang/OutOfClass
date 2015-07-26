using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour 
{
    public MapManager mapManager;

    Transform cachedTransform;
    
    Stack<Vector2> movePosStack = new Stack<Vector2>();

    float moveSpeed = 0f;

    bool startCoroutine = false;

    public void SetDestination(Vector2 targetPos, float speed)
    {
        movePosStack.Clear();
        mapManager.GetPath(movePosStack, cachedTransform.position, targetPos);
        moveSpeed = speed;
        if (!startCoroutine)
        {
            StartCoroutine(FollowPath());
            startCoroutine = true;
        }
    }

    IEnumerator FollowPath()
    {
        while (movePosStack.Count != 0)
        {
            Vector3 targetPos = movePosStack.Pop();

            //while (movePosStack.Count != 0)
            {
                //cachedTransform.position = Vector2.MoveTowards(cachedTransform.position, targetPos, moveSpeed * Time.deltaTime);
                cachedTransform.position = targetPos;
                yield return new WaitForSeconds(1f);
            }
        }
        yield return null;
    }

    void Awake()
    {
        cachedTransform = base.transform;
    }
}
