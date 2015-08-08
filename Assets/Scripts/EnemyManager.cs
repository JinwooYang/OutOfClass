using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public  GameObject  enemyPrefab  =  null;
	public  int  spawnCount  =  10;
	
	void  Start()
	{
		Spawner.Register( enemyPrefab, spawnCount, Spawner.ObjectCache.Type.FixedLimit );
		Spawner.Init();
	}    

	float  deltaTime  =  0.0f;
	public  float  spawnTime  =  2.0f;
	
	int count = 0;
	
	void  Update()
	{
		if (count >= spawnCount)
			return;

		deltaTime += Time.deltaTime;
		if (deltaTime > spawnTime)
		{
			deltaTime = 0.0f;
			GameObject obj = Spawner.Spawn(enemyPrefab);
			if (obj != null)
			{
				obj.SetActive(true);
				float x = Random.Range(-10.0f, 10.0f);
				float z = Random.Range(-10.0f, 10.0f); 
				obj.transform.position = new Vector3(x, 0.0f, z);
//				++count; 
			}
		}
	}

	void DisableObject(GameObject obj)
	{
		--count;
		Spawner.Disable(obj);
	}
}
