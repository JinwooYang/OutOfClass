using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sequence : Action
{
    private List<Action> _ActionList = new List<Action>();

    private int _CurActionIndex = 0;

    public Sequence(params Action[] actions)
    {
        _ActionList.AddRange(actions);
    }

    protected override void OnActionStart(Actor actor)
    {
        _CurActionIndex = 0;

        _ActionList[_CurActionIndex].ActionStart(actor);
    }

    protected override void OnActionUpdate(Actor actor)
    {
        _ActionList[_CurActionIndex].ActionUpdate(actor);

        if(_ActionList[_CurActionIndex].IsFinish())
        {
            if(++_CurActionIndex < _ActionList.Count)
            {
                _ActionList[_CurActionIndex].ActionStart(actor);
            }
            else
            {
                ActionFinish();
            }
        }
    }

    //void OnDestroy()
    //{
    //    foreach(Action action in _ActionList)
    //    {
    //        Destroy(action);
    //    }
    //}
}
