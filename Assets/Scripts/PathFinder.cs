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

    //public void SetDestination(Vector2 pos, float speed)
    //{
    //    movePosStack.Clear();
    //    mapManager.GetPath(movePosStack, cachedTransform.position, pos);
    //    moveSpeed = speed;
    //    if (!startCoroutine)
    //    {
    //        StartCoroutine(FollowPath());
    //        startCoroutine = true;
    //    }
    //}

    //IEnumerator FollowPath()
    //{
    //    while (movePosStack.Count != 0)
    //    {
    //        Vector3 targetPos = movePosStack.Pop();

    //        while (targetPos != cachedTransform.position)
    //        {
    //            cachedTransform.position = Vector2.MoveTowards(cachedTransform.position, targetPos, moveSpeed * Time.deltaTime);
    //            return null;
    //        }
    //    }
    //    return null;
    //}

    //void Awake () 
    //{
    //    cachedTransform = base.transform;
    //    mapManager.SetTileState(cachedTransform.position, TileState.CLOSED);
    //}
}
