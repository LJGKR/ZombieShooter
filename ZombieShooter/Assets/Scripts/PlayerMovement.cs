using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed; //�̵��ӵ�

	public Vector3 movement; //����� ȸ�� ������ ���� ����

	public Animator animator; //�ִϸ�����

	int floorMask; //�ٴ� �浹 ����ũ(���콺 ��ġ üũ)
	float camRayLength = 100f; //ī���� �ü� üũ ���� ����(�ٴ� ���� �浹�� ���)

	public Rigidbody playerRigidbody; //�÷��̾� ��ü

	private void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
	}

	private void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		//�̵� �޼ҵ� ȣ��
		Move(h, v);
		Turning();
		Animing(h, v);
	}

	void Animing(float h, float v)
	{
		//��򰡷� �̵��ϴ� �̵� Ű�� ���ȴٸ�
		bool isWalking = (h != 0 || v != 0);

		//�̵� / ��� �ִϸ��̼� ��ȯ
		animator.SetBool("IsWalking", isWalking);

	}
	//�̵� ó�� �޼ҵ�
	void Move(float h, float v)
	{
		//�̵� ���� ���� ����
		movement = new Vector3(h, 0f, v);

		//�̵� ���� ���
		movement = movement.normalized * speed * Time.fixedDeltaTime;

		//Rigidbody�� �̿��� �̵� ó��
		playerRigidbody.MovePosition(playerRigidbody.position + movement);
	}

	void Turning()
	{
		//ȭ�� ��ġ(���콺��ġ) ���̸� ����
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		//���� �浹 ���� ����ü ����
		RaycastHit floorHit;

		//�ٴ� �浹 ������ ���� ���� �浹�� üũ��
		//�浹 ������ -> floorHit�� ��ȯ�� �� (out ��ɾ� ���)
		if(Physics.Raycast(camRay,out floorHit, camRayLength, floorMask))
		{
			//�÷��̾�� ���콺 ����Ʈ ��ġ���� ������ ����
			Vector3 playerToMousePoint = floorHit.point - transform.position;

			//�÷��̾ ���콺 ����Ʈ ��ġ�� ���ϴ� ȸ���� ����
			Quaternion newRotation = Quaternion.LookRotation(playerToMousePoint);

			//�÷��̾� ĳ���� ȸ��
			playerRigidbody.MoveRotation(newRotation);
		}
	}

}
