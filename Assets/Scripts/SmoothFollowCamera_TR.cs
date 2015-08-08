using UnityEngine;
using System.Collections;

public class SmoothFollowCamera_TR : MonoBehaviour {
	
	public Transform target;
	public float dampRotate = 5f;
	public Transform dest;
	
	Transform tr;
	
	void Start () {
		
		tr = GetComponent<Transform>();
	}
	
	void LateUpdate () {
		
		tr.position = dest.position; 		
		tr.LookAt(target);
	}
}

