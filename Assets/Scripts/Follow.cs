using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour 
{
    public GameObject followObject;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = new Vector3(followObject.transform.position.x,
                                        followObject.transform.position.y,
                                        -10);
	}
}
