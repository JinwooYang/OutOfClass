using UnityEngine;
using System.Collections;

public class AutoDestroyParticle : MonoBehaviour {

	public bool destroy = true;
	public ParticleSystem _particleSystem;
	
	void OnEnable()
	{
		_particleSystem = GetComponent<ParticleSystem>();

		StopAllCoroutines();
		StartCoroutine("ParticleProcess");
	}
	
	IEnumerator ParticleProcess()
	{
		yield return null;
		
		while( true )
		{
			if(_particleSystem.IsAlive(true) == false )
			{
				if (destroy)
					Destroy(gameObject);
				else
					gameObject.SetActive(false);
				break;
			}            
			
			yield return new WaitForSeconds(0.5f);
		}
	}

}
