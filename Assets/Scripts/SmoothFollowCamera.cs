using UnityEngine;
using System.Collections;

public class SmoothFollowCamera : MonoBehaviour
{
	Transform _transform;
	public Transform targetTransform;   // 바라볼 대상
	public float distance = 10.0f;
	public float height = 5.0f;
	public float heightDamping = 2.0f;

	Vector3 lastForward = Vector3.forward;
	Vector3 toForward = Vector3.forward;
	public float smoothChange = 1.0f;

	void Awake()
	{
		_transform = transform;
	}
	
	void LateUpdate()
	{
		if (targetTransform == null)
			return;
		
		float wantedHeight = targetTransform.position.y + height;
		float currentHeight = transform.position.y;
		
		currentHeight = 
			Mathf.Lerp( currentHeight, wantedHeight,
			           heightDamping * Time.deltaTime );

		// x-z 평면에 대한 거리를 캐릭터 기준 뒤로 이동
		_transform.position = targetTransform.position;

		if( Input.GetButtonDown("Fire2") )
			toForward = targetTransform.forward;
		
		lastForward = Vector3.Lerp( lastForward, toForward, smoothChange * Time.deltaTime );
		_transform.position -= lastForward * distance; 
		
		// 카메라 높이 설정
		Vector3 cameraPos = _transform.position;
		cameraPos.y = currentHeight;
		_transform.position = cameraPos;
		
		// 카메라가 타겟을 보도록 변경
		transform.LookAt(targetTransform);
	}
}

