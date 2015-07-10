using UnityEngine;
using System.Collections;

public class TargetedAction : Action 
{
    private Actor _Target;
    private Action _Action;


    public TargetedAction(Actor target, Action action)
    {
        _Target = target;
        _Action = action;
    }

    protected override void OnActionStart(Actor actor)
    {
        //TODO
    }

    protected override void OnActionUpdate(Actor actor) 
    {
        _Action.ActionUpdate(_Target);
	}

}
