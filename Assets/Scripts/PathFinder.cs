using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour 
{
    public MapManager mapManager;

    Transform cachedTransform;
    
    Stack<Vector2> movePosStack = new Stack<Vector2>();

    float moveSpeed = 0f;

    bool startFollowPathCoroutine = false;

    public void SetDestination(Vector2 targetPos, float speed)
    {
        movePosStack.Clear();
        mapManager.GetPath(movePosStack, cachedTransform.position, targetPos);
        moveSpeed = speed;
        if (!startFollowPathCoroutine)
        {
            StartCoroutine(FollowPath());
            startFollowPathCoroutine = true;
        }
    }

    IEnumerator FollowPath()
    {
        while (true)
        {
            if (movePosStack.Count != 0)
            {
                Vector3 targetPos = movePosStack.Pop();

                //while (movePosStack.Count != 0)
                {
                    //cachedTransform.position = Vector2.MoveTowards(cachedTransform.position, targetPos, moveSpeed * Time.deltaTime);
                    cachedTransform.position = targetPos;
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return null;
        }
    }

    void Awake()
    {
        cachedTransform = base.transform;
    }
}
