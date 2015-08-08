using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skeleton : MonoBehaviour
{
	public enum ENEMYSTATE
	{
		NONE = -1,
		IDLE = 0,
		MOVE,
		ATTACK,
		DAMAGE,
		DEAD
	}
	
	public ENEMYSTATE  enemyState  =  ENEMYSTATE.IDLE;

	Animator  anim = null;
	void  Awake()
	{
		anim = GetComponent<Animator>();
		
		enemyState = ENEMYSTATE.IDLE;
	}

	public  GameObject  damageParticle;
	void  OnEnable()
	{
		damageParticle.SetActive(false);
	}

	delegate  void  FsmFunc();
	Dictionary<ENEMYSTATE, FsmFunc>  dicState = new Dictionary<ENEMYSTATE, FsmFunc>();
	void Start()
	{
		dicState[ENEMYSTATE.NONE] = None;
		dicState[ENEMYSTATE.IDLE] = Idle;
		dicState[ENEMYSTATE.MOVE] = Move;
		dicState[ENEMYSTATE.ATTACK] = Attack;
		dicState[ENEMYSTATE.DAMAGE] = Damage;
		dicState[ENEMYSTATE.DEAD] = Dead;

		FindPlayer();
	}
	
	Transform target = null;
	void  FindPlayer()
	{
		GameObject findObject = GameObject.FindWithTag("Player");
		if (findObject != null)
			target = findObject.transform;
	}
	
	void Update()
	{
		dicState[enemyState]();
	}

	void None()
	{
	}
	
	float stateTime = 0.0f;
	public float idleStateMaxTime = 2.0f;
	
	void Idle()
	{
		stateTime += Time.deltaTime;
		if (stateTime > idleStateMaxTime)
		{
			stateTime = 0.0f;
			enemyState = ENEMYSTATE.MOVE;
			anim.SetTrigger("move");
		}

		float distance = (target.position - transform.position).magnitude;
		if (distance <= attackRange)
		{
			stateTime = attackStateMaxTime;
			enemyState = ENEMYSTATE.ATTACK;
			anim.SetTrigger("attack");
		}
	}

	public  float  moveSpeed = 3.0f;
	public  float  rotationSpeed = 10.0f;
	public  float  attackRange = 1.0f;
	public  float  attackStateMaxTime = 2.0f;
	
	void Move()
	{
		float distance = (target.position - transform.position).magnitude;
		if (distance > attackRange)
		{
			Vector3 dir = target.position - transform.position;
			dir.y = 0.0f;
			dir.Normalize();
			transform.position += dir * moveSpeed * Time.deltaTime;
			
			transform.rotation = Quaternion.Lerp(   transform.rotation,
			                                     Quaternion.LookRotation(dir),
			                                     rotationSpeed * Time.deltaTime );
		}
		else
		{
			stateTime = attackStateMaxTime;
			enemyState = ENEMYSTATE.ATTACK;
			anim.SetTrigger("attack");
		}
	}

	public   AnimationClip   attackClip;
	void Attack()
	{
		stateTime += Time.deltaTime;
		if (stateTime > attackStateMaxTime)
		{            
			stateTime = -attackClip.length;    // attack motion time
			anim.SetTrigger("attack");
		}
		
		float distance = (target.position - transform.position).magnitude;
		if (distance > attackRange)
		{
			stateTime = 0.0f;
			enemyState = ENEMYSTATE.IDLE;            
		}
	}

	
	void Damage()
	{
		enemyState = ENEMYSTATE.IDLE;                            
	}
	
	void Dead()
	{
		anim.SetBool("dead", true);
		enemyState = ENEMYSTATE.NONE;

		StartCoroutine( DeadProcess() );
	}

	public float fadeOutSpeed = 0.5f;
	public float fadeWaitTime = 2.0f;
	IEnumerator DeadProcess()
	{
		Renderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
		
		Shader transparentShader = Shader.Find("Transparent/Diffuse");
		renderers[0].material.shader = transparentShader;
		renderers[1].material.shader = transparentShader;
		
		yield return new WaitForSeconds(2.0f);
		
		Color fadeOutColor = Color.white;
		while (fadeOutColor.a > 0.0f)
		{
			yield return new WaitForEndOfFrame();
			
			fadeOutColor.a -= fadeOutSpeed * Time.deltaTime;
			
			renderers[0].material.color = fadeOutColor;
			renderers[1].material.color = fadeOutColor;
		}
		
		fadeOutColor.a = 0.0f;
		renderers[0].material.color = fadeOutColor;
		renderers[1].material.color = fadeOutColor;
		
		enemyState = ENEMYSTATE.NONE;
		gameObject.SetActive(false);
		
		yield return null;
	}

	
	public float healthPoint = 10;
	public void OnDamage()
	{
		--healthPoint;
		
		if (healthPoint > 0)
		{
			enemyState = ENEMYSTATE.DAMAGE;
			anim.SetFloat("damage", Random.Range(1f, 2f));
		}
		else
		{
			enemyState = ENEMYSTATE.DEAD;
		}

		damageParticle.SetActive(true);
	}

	public void ResetDamage()
	{
		anim.SetFloat("damage", 0.0f);
	}
}

