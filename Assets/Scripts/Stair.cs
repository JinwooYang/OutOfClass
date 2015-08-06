using UnityEngine;
using System.Collections;

public class Stair : MonoBehaviour 
{
    public GameObject curFloor;
    public GameObject toGoFloor;

	void Awake () 
    {
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {

        }
    }
}
