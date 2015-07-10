using UnityEngine;
using System.Collections;

public class RepeatForever : Action 
{
    public Action _Action;

    private RepeatForever() {}

    public RepeatForever(Action action)
    {
        _Action = action;
    }

	protected override void OnActionStart(Actor actor) 
    {
        _Action.ActionStart(actor);
	}
	

    protected override void OnActionUpdate(Actor actor) 
    {
        _Action.ActionUpdate(actor);

        if(_Action.IsFinish())
        {
            _Action.ActionStart(actor);
        }
	}
}
