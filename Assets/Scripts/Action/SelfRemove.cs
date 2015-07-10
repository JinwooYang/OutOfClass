using UnityEngine;
using System.Collections;

public class SelfRemove : Action 
{
    protected override void OnActionStart(Actor actor)
    {
        
    }

    protected override void OnActionUpdate(Actor actor)
    {
        Object.Destroy(actor.gameObject);
        ActionFinish();
    }
}
