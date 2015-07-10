using UnityEngine;
using System.Collections;

public class TintTo : Action 
{
    private float _Duration;

    private Color _OrgColor, _TargetColor, _DeltaColor;

    private SpriteRenderer _SprRenderer;

	public TintTo(float duration, Color targetColor) 
    {
        _Duration = duration;
        _TargetColor = targetColor;
	}

    protected override void OnActionStart(Actor actor)
    {
        _SprRenderer = actor.GetComponent<SpriteRenderer>();

        _OrgColor = _SprRenderer.color;

        _DeltaColor = (_TargetColor - _OrgColor) / (_Duration * 60.0f);
    }

    protected override void OnActionUpdate(Actor actor) 
    {
        _SprRenderer.color += _DeltaColor * Time.deltaTime * 60.0f;

        Color curColor = _SprRenderer.color;

        if (_OrgColor.r <= _TargetColor.r && curColor.r >= _TargetColor.r ||
            _OrgColor.r > _TargetColor.r && curColor.r < _TargetColor.r)
        {
            curColor.r = _TargetColor.r;
        }
        if (_OrgColor.g <= _TargetColor.g && curColor.g >= _TargetColor.g ||
            _OrgColor.g > _TargetColor.g && curColor.g < _TargetColor.g)
        {
            curColor.g = _TargetColor.g;
        }
        if (_OrgColor.b <= _TargetColor.b && curColor.b >= _TargetColor.b ||
            _OrgColor.b > _TargetColor.b && curColor.b < _TargetColor.b)
        {
            curColor.b = _TargetColor.b;
        }
        if (_OrgColor.a <= _TargetColor.a && curColor.a >= _TargetColor.a ||
            _OrgColor.a > _TargetColor.a && curColor.a < _TargetColor.a)
        {
            curColor.a = _TargetColor.a;
        }


        _SprRenderer.color = curColor;

        if (curColor == _TargetColor)
        {
            //Destroy(this);
            ActionFinish();
        }
    }
}
