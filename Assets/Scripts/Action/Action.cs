using UnityEngine;
using System.Collections;


public abstract class Action 
{
    private bool _Finish = false;


    public void ActionStart(Actor actor)
    {
        OnActionStart(actor);
        _Finish = false;
	}


    public void ActionUpdate(Actor actor) 
    {
        OnActionUpdate(actor);
	}


    protected abstract void OnActionStart(Actor actor);


    protected abstract void OnActionUpdate(Actor actor);


    protected void ActionFinish()
    {
        _Finish = true;
    }

    public bool IsFinish()
    {
        return _Finish;
    }
}
