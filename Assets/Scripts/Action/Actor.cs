using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour 
{
    public List<Action> _ActionList = new List<Action>();

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
	    for(int i = 0; i < _ActionList.Count; ++i)
        {
            _ActionList[i].ActionUpdate(this);

            if (_ActionList[i].IsFinish())
            {
                _ActionList.RemoveAt(i);
                --i;
            }
        }
	}


    public void RunAction(Action action)
    {
        action.ActionStart(this);
        _ActionList.Add(action);
    }


    public int GetNumberOfRunningAction()
    {
        return _ActionList.Count;
    }


    public void StopAllAction()
    {
        for (int i = _ActionList.Count - 1; i >= 0; --i)
        {
            _ActionList.RemoveAt(i);
        }
    }
}
