using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour 
{
    public FloorImageAnimator floorImageAnimator;

    public int floorNum;

    public static Floor playerFloor
    {
        get;
        private set;
    }

    public static Floor monsterFloor
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
    Transform monsterWay;


    public Transform GetRandomMonsterMovePos()
    {
        return monsterWay.GetChild(Random.Range(0, monsterWay.childCount));
    }

    void Awake()
    {
        cachedTransform = base.transform;
        mapManager = cachedTransform.FindChild("MapManager").gameObject.GetComponent<MapManager>();
        monsterWay = cachedTransform.FindChild("MonsterWay");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerFloor = this;
            floorImageAnimator.ShowFloorImage(floorNum);
            print("player floor is " + floorNum);
        }
        else if(other.CompareTag("Monster"))
        {
            monsterFloor = this;
            print("monster floor is " + floorNum);
        }
    }
}
