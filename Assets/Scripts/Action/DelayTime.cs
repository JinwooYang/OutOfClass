using UnityEngine;
using System.Collections;

public class DelayTime : Action
{
    private float _Duration;

    float _StartTime;

    public DelayTime(float duration)
    {
        _Duration = duration;
    }

    protected override void OnActionStart(Actor actor)
    {
        _StartTime = Time.time;
    }

    protected override void OnActionUpdate(Actor actor)
    {
        if(Time.time - _StartTime >= _Duration)
        {
            ActionFinish();
        }
    }


}
