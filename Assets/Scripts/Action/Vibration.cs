using UnityEngine;
using System.Collections;

public class Vibration : Action 
{
    private float _Duration;
    private Vector3 _Range;

    private Vector3 _OrgPos;

    private float _OldTime;

    public Vibration(float duration, Vector3 range)
    {
        _Duration = duration;
        _Range = range;
    }

    protected override void OnActionStart(Actor actor)
    {
        _OrgPos = actor.transform.position;
        _OldTime = Time.time;
    }

    protected override void OnActionUpdate(Actor actor)
    {
        if(Time.time - _OldTime < _Duration)
        {
            Vector3 pos = new Vector3(Random.Range(_OrgPos.x - _Range.x, _OrgPos.x + _Range.x),
                                    Random.Range(_OrgPos.y - _Range.y, _OrgPos.y + _Range.y),
                                    Random.Range(_OrgPos.z - _Range.z, _OrgPos.z + _Range.z));

            actor.transform.position = pos;
        }
        else
        {
            actor.transform.position = _OrgPos;
            ActionFinish();
        }
    }
}
