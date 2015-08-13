using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void MoveDoneCallback();


public class PathFinder : MonoBehaviour 
{
    public MapManager mapManager;

    public bool isMoving
    {
        get;
        private set;
    }

    Transform cachedTransform;
    
    Stack<Vector2> movePosStack = new Stack<Vector2>();

    float moveSpeed = 0f;

    float orgDegAngle = 0f;

    bool startFollowPathCoroutine = false;

    MoveDoneCallback moveDoneCallback;

    public void SetMapManager(MapManager mapManager)
    {
        this.mapManager = mapManager;
    }

    public void SetDestination(Vector2 targetPos, float moveSpeed, float orgDegAngle, MoveDoneCallback callback = null)
    {
        isMoving = true;

        movePosStack.Clear();
        mapManager.GetPath(movePosStack, cachedTransform.position, targetPos);

        this.moveSpeed = moveSpeed;
        this.orgDegAngle = orgDegAngle;
        this.moveDoneCallback = callback;

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
            float remainDist = moveSpeed * Time.deltaTime;

            while (movePosStack.Count > 0)
            {
                Vector3 targetPos = movePosStack.Peek();
                Vector3 dist = targetPos - cachedTransform.position;
                float radAngle = Mathf.Atan2(dist.y, dist.x);

                cachedTransform.rotation = Quaternion.Euler(0f, 0f, radAngle * Mathf.Rad2Deg + orgDegAngle);

                if (dist.sqrMagnitude < (remainDist * remainDist))
                {
                    remainDist -= dist.magnitude;
                    cachedTransform.position = targetPos;
                    movePosStack.Pop();

                    if (movePosStack.Count == 0)
                    {
                        isMoving = false;
                        if (moveDoneCallback != null) { moveDoneCallback(); }
                    }
                }
                else
                {
                    cachedTransform.position += (dist.normalized * remainDist);
                    break;
                }
            }

            yield return null;
        }
    }

    void Awake()
    {
        cachedTransform = base.transform;
        isMoving = false;
    }
}
