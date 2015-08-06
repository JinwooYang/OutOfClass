using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour 
{
    public static GameObject playerFloor
    {
        get;
        private set;
    }

    public static GameObject monsterFloor
    {
        get;
        private set;
    }


    public MapManager mapManager
    {
        get;
        private set;
    }

    Transform cachedTransform;

    void Awake()
    {
        cachedTransform = base.transform;
        mapManager = cachedTransform.FindChild("MapManager").gameObject.GetComponent<MapManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerFloor = this.gameObject;
            print("player floor is " + this.gameObject.name);
        }
        else if(other.CompareTag("Monster"))
        {
            monsterFloor = this.gameObject;
            print("monster floor is " + this.gameObject.name);
        }
    }
}
