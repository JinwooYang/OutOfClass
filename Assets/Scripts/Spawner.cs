using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public class ObjectCache
	{
		public enum Type
		{
			Recycle = 0,
			FixedLimit, 
		}
		
		public  GameObject  _prefab = null;
		public  int  _cacheSize = 10;
		public  Type  _type = Type.Recycle;

		GameObject[]  _objects;
		public  void  Generate()
		{
			_objects  =  new GameObject[ _cacheSize ];
			
			for( int i = 0; i < _cacheSize; ++i )
			{
				_objects[i] = GameObject.Instantiate( _prefab ) as GameObject;
				_objects[i].name = _prefab.name + "_" +  i;
				_objects[i].transform.position = Vector3.one * Random.Range(-1000.0f, 1000.0f);
			}
		}

		int   _lastCachedIndex = 0;
		public  GameObject  GetNextObject()
		{
			GameObject  obj = null;
			for (int i = 0; i < _cacheSize; ++i)
			{
				obj = _objects[_lastCachedIndex];
				
				++_lastCachedIndex;
				_lastCachedIndex %= _cacheSize;
				
				if (obj.activeSelf == false)
					break; // == return obj;
			}

			if (obj.activeSelf)
			{
				if (_type == Type.Recycle)
				{
					obj = _objects[_lastCachedIndex];
					
					++_lastCachedIndex;
					_lastCachedIndex %= _cacheSize;
					
					Spawner.Disable(obj);
				}
				else if (_type == Type.FixedLimit)
				{
					return null;
				}
			}
			
			return obj;
		}

		public void Initialize()
		{
			for (int i = 0; i < _cacheSize; ++i)
			{
				_objects[i].SetActive( false );
			}
		}
	}

	static Spawner _instance;
	
	void Awake()
	{
		if( _instance == null )
			_instance = this;
		else
			Destroy( gameObject );
	}

	Dictionary<GameObject, ObjectCache>   _caches = new  Dictionary<GameObject, ObjectCache>();

	public   static  bool  Register(GameObject prefab,  int cacheSize,  ObjectCache.Type type)
	{
		if (_instance == null)
			return false;
		
		if (prefab == null)
			return false;
		
		if( _instance._caches.ContainsKey(prefab) == false )
		{
			ObjectCache cache = new ObjectCache();
			cache._prefab = prefab;
			cache._cacheSize = cacheSize;
			cache._type = type;
			
			_instance._caches.Add(prefab, cache);
			
			return true;
		}
		else
		{
			// ignore            
			return true;
		}
	}

	Hashtable   _activateCachedObjects;
	
	public  static  GameObject  Spawn(GameObject  prefab)
	{
		if (prefab == null)
			return null;
		
		if (_instance == null)
			return null;
		
		if (_instance._caches.ContainsKey(prefab))
		{
			GameObject obj = _instance._caches[prefab].GetNextObject();
			if (obj == null)
				return null;
			
			obj.SetActive(true);
			_instance._activateCachedObjects[obj] = true;
			
			return obj;
		}
		
		// no cached object.
		return Instantiate(prefab) as GameObject;
	}

	public static void Disable(GameObject  objectToDestroy)
	{
		if (objectToDestroy == null)
			return;
		
		if (_instance == null)
			return;
		
		if (_instance._activateCachedObjects.Contains(objectToDestroy) == true)
		{
			if( (bool)_instance._activateCachedObjects[objectToDestroy] == true )
			{
				objectToDestroy.SetActive(false);
				_instance._activateCachedObjects[objectToDestroy] = false;
			}
			
			return;
		}
		
		GameObject.Destroy(objectToDestroy);
	}

	static bool _isInitialized = false;
	public static void Init()
	{
		if (_instance == null)
			return;

		if (_isInitialized == false)
		{
			_isInitialized = true;
//			_instance.StartCoroutine( "RegistCache" );
			_instance.RegisterCache();
		}        
	}

	IEnumerator  RegistCache()
	{
		int amount = 0;
		foreach (ObjectCache cache  in  _instance._caches.Values)
		{
			yield return new WaitForEndOfFrame();
			
			cache.Generate();
			
			yield return new WaitForFixedUpdate();
			
			cache.Initialize();
			
			amount += cache._cacheSize;
		}
		_instance._activateCachedObjects = new Hashtable(amount);
	}

	void RegisterCache()
	{
		int amount = 0;
		foreach (ObjectCache cache  in  _instance._caches.Values)
		{
			cache.Generate();
			cache.Initialize();
			
			amount += cache._cacheSize;
		}
		_instance._activateCachedObjects = new Hashtable(amount);
	}
}
