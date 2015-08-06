using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour 
{
    public static GameObject curFloor
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
            curFloor = this.gameObject;
        }
    }
}
