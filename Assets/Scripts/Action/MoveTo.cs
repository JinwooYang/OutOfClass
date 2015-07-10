using UnityEngine;
using System.Collections;

public class MoveTo : Action 
{
    private float _Duration;

    private Vector3 _StartPos, _TargetPos;

    private Vector3 _DeltaPos;


    public MoveTo(float duration, Vector3 targetPos)
    {
        _Duration = duration;
        _TargetPos = targetPos;
    }

	protected override void OnActionStart (Actor actor) 
    {
        _StartPos = actor.transform.position;
        _DeltaPos = (_TargetPos - _StartPos) / (_Duration * 60.0f);
    }

    protected override void OnActionUpdate(Actor actor) 
    {
        actor.transform.position += _DeltaPos * Time.deltaTime * 60.0f;

        Vector3 curPos = actor.transform.position;

        if (_StartPos.x <= _TargetPos.x && curPos.x >= _TargetPos.x ||
            _StartPos.x > _TargetPos.x && curPos.x < _TargetPos.x)
        {
            curPos.x = _TargetPos.x;
        }
        if (_StartPos.y <= _TargetPos.y && curPos.y >= _TargetPos.y ||
            _StartPos.y > _TargetPos.y && curPos.y < _TargetPos.y)
        {
            curPos.y = _TargetPos.y;
        }
        if (_StartPos.z <= _TargetPos.z && curPos.z >= _TargetPos.z ||
            _StartPos.z > _TargetPos.z && curPos.z < _TargetPos.z)
        {
            curPos.z = _TargetPos.z;
        }

        actor.transform.position = curPos;

        if (curPos == _TargetPos)
        {
            //Destroy(this);
            ActionFinish();
        }
	}
}
