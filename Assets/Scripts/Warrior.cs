using UnityEngine;
using System.Collections;

public class Warrior : MonoBehaviour {

	Animator _animator;
	Transform _transform;
	
	public float moveSpeed = 2.5f;
	public float rotationSpeed = 5.0f;
	Vector3 moveToPosition = Vector3.zero;
	
	public   GameObject   arrow;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_transform = GetComponent<Transform>();
		
		moveToPosition = _transform.position;
	}

	void Update () 
	{	
		if (Input.GetKeyDown(KeyCode.A))
			_animator.SetTrigger("LSlash1");
		
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			
			int groundMask = 1 << LayerMask.NameToLayer("Ground");
			RaycastHit hitInfo;
			bool result = Physics.Raycast(ray, out hitInfo, Mathf.Infinity, groundMask);
			if( result ) 
			{
				moveToPosition = hitInfo.point;
				
				if (arrow)
				{
					arrow.transform.position = moveToPosition;
					arrow.SetActive(true);
				}
			}
		}

		MoveProcess();
	}

	float deltaAttackTime = 0.0f;
	public float attackMaxTime = 3.0f;

	void MoveProcess()
	{
		if (Vector3.Distance(_transform.position, moveToPosition) > 0.05f)
		{
			_animator.SetBool("run", true);

            // 이동 
            Vector3 dir = moveToPosition - _transform.position;
            dir.y = 0f;
            dir.Normalize();

            _transform.position += dir * moveSpeed * Time.deltaTime;

            // 회전
            Quaternion from = _transform.rotation;
            Quaternion to = Quaternion.LookRotation(dir);
            _transform.rotation = Quaternion.Lerp(from, to, rotationSpeed * Time.deltaTime);
        }
        else
        {
            if( arrow )
                  arrow.SetActive(false);

            _animator.SetBool("run", false);

			AttackToTarget();
		}
	}

	void AttackToTarget()
	{
		if (targetEnemy == null)
			return;
		
		if (targetEnemy.gameObject.activeSelf == false)            
			return;
		
		Vector3 dir = targetEnemy.position - transform.position;
		dir.y = 0f;
		dir.Normalize();
		
		Quaternion from = transform.rotation;
		Quaternion to = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Lerp(from, to, rotationSpeed * Time.deltaTime);
		
		deltaAttackTime += Time.deltaTime;
		if (deltaAttackTime > attackMaxTime)
		{
			deltaAttackTime = 0.0f;
			_animator.SetTrigger("attack");
		}
	}

	void OnAttack()
	{
		if (targetEnemy == null)
			return;

		targetEnemy.SendMessage( "OnDamage", SendMessageOptions.RequireReceiver);
	}

	Transform targetEnemy = null;
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Monsters")
			return;
		
		targetEnemy = other.transform;
	}
	
	
	void OnTriggerExit(Collider other)
	{
		if (other.tag != "Monsters")
			return;
		
		targetEnemy = null;
	}
}
