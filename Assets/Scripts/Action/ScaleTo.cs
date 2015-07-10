using UnityEngine;
using System.Collections;

public class ScaleTo : Action 
{
    private float _Duration;

    private Vector3 _StartScale, _TargetScale;

    private Vector3 _DeltaScale;


    public ScaleTo(float duration, Vector3 targetScale)
    {
        _Duration = duration;
        _TargetScale = targetScale;
    }

    protected override void OnActionStart(Actor actor)
    {
        _StartScale = actor.transform.localScale;
        _DeltaScale = (_TargetScale - _StartScale) / (_Duration * 60.0f);
    }


    protected override void OnActionUpdate(Actor actor) 
    {
        actor.transform.localScale += _DeltaScale * Time.deltaTime * 60.0f;

        Vector3 curScale = actor.transform.localScale;

        if(_StartScale.x <= _TargetScale.x && curScale.x >= _TargetScale.x ||
            _StartScale.x > _TargetScale.x && curScale.x < _TargetScale.x)
        {
            curScale.x = _TargetScale.x;
        }
        if (_StartScale.y <= _TargetScale.y && curScale.y >= _TargetScale.y ||
            _StartScale.y > _TargetScale.y && curScale.y < _TargetScale.y)
        {
            curScale.y = _TargetScale.y;
        }
        if (_StartScale.z <= _TargetScale.z && curScale.z >= _TargetScale.z ||
            _StartScale.z > _TargetScale.z && curScale.z < _TargetScale.z)
        {
            curScale.z = _TargetScale.z;
        }

        actor.transform.localScale = curScale;

        if(curScale == _TargetScale)
        {
            //Destroy(this);
            ActionFinish();
        }
	}

}
