using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed; //이동속도

	public Vector3 movement; //방향과 회전 정보를 담은 벡터

	public Animator animator; //애니메이터

	int floorMask; //바닥 충돌 마스크(마우스 위치 체크)
	float camRayLength = 100f; //카레마 시선 체크 레이 길이(바닥 레이 충돌에 사용)

	public Rigidbody playerRigidbody; //플레이어 강체

	private void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
	}

	private void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		//이동 메소드 호출
		Move(h, v);
		Turning();
		Animing(h, v);
	}

	void Animing(float h, float v)
	{
		//어딘가로 이동하는 이동 키가 눌렸다면
		bool isWalking = (h != 0 || v != 0);

		//이동 / 대기 애니메이션 전환
		animator.SetBool("IsWalking", isWalking);

	}
	//이동 처리 메소드
	void Move(float h, float v)
	{
		//이동 방향 벡터 생성
		movement = new Vector3(h, 0f, v);

		//이동 벡터 계산
		movement = movement.normalized * speed * Time.fixedDeltaTime;

		//Rigidbody를 이용한 이동 처리
		playerRigidbody.MovePosition(playerRigidbody.position + movement);
	}

	void Turning()
	{
		//화면 터치(마우스위치) 레이를 구함
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		//레이 충돌 정보 구조체 생성
		RaycastHit floorHit;

		//바닥 충돌 영역과 레이 간의 충돌을 체크함
		//충돌 정보는 -> floorHit로 반환해 줌 (out 명령어 사용)
		if(Physics.Raycast(camRay,out floorHit, camRayLength, floorMask))
		{
			//플레이어와 마우스 포인트 위치간의 방향을 구함
			Vector3 playerToMousePoint = floorHit.point - transform.position;

			//플레이어가 마우스 포인트 위치를 향하는 회전을 구함
			Quaternion newRotation = Quaternion.LookRotation(playerToMousePoint);

			//플레이어 캐릭터 회전
			playerRigidbody.MoveRotation(newRotation);
		}
	}

}
