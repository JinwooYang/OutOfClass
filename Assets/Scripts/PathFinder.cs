using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour 
{
    public MapManager mapManager;

    Transform cachedTransform;
    
    Stack<Vector2> movePosStack = new Stack<Vector2>();

    float moveSpeed = 0f;

    float orgDegAngle = 0f;

    bool startFollowPathCoroutine = false;

    public void SetDestination(Vector2 targetPos, float moveSpeed, float orgDegAngle)
    {
        movePosStack.Clear();
        mapManager.GetPath(movePosStack, cachedTransform.position, targetPos);

        this.moveSpeed = moveSpeed;
        this.orgDegAngle = orgDegAngle;

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
            if (movePosStack.Count > 0)
            {
                Vector3 targetPos = movePosStack.Pop();
                Vector3 moveDist = targetPos - cachedTransform.position;
                float radAngle = Mathf.Atan2(moveDist.y, moveDist.x);
                cachedTransform.rotation = Quaternion.Euler(0f, 0f, radAngle * Mathf.Rad2Deg + 90f);


                while (cachedTransform.position != targetPos)
                {
                    cachedTransform.position = Vector3.MoveTowards(cachedTransform.position, targetPos, moveSpeed * Time.deltaTime);
                    yield return null;
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
