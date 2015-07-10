using UnityEngine;
using System.Collections;

public class HowToPlayImage : MonoBehaviour 
{
    public float fadeSec;

    Actor actor;
    SpriteRenderer sprRenderer;

    bool closing;

	// Use this for initialization
	void Start () 
    {
        closing = false;

        actor = GetComponent<Actor>();
        sprRenderer = GetComponent<SpriteRenderer>();

        sprRenderer.color = Color.clear;

        actor.RunAction(new TintTo(fadeSec, Color.white));    
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(!closing && Input.GetKeyDown(KeyCode.Escape))
        {
            closing = true;

            actor.StopAllAction();

            actor.RunAction(new TintTo(fadeSec, Color.clear));

            Destroy(this.gameObject, fadeSec);
        }
	}
}
