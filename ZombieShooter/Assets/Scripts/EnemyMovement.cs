using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //�÷��̾� ����(������ ����)
    public Transform playerTransform;

    //���� �ִϸ����� ����
    public Animator animator;

    //���� �׺���̼� ������Ʈ ����
    public NavMeshAgent navMeshAgent;

    public EnemyHealth health;

    void Start()
    {
        //�÷��̾� ���� ������Ʈ�� ã��(�±װ� �÷��̾���) ������Ʈ�� �̿��� �÷��̾��� Ʈ������ ���� ������
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
   
    void Update()
    {
        //������ ü���� 0�̸�
        if(health.currentHealth <= 0)
		{
            //���� ���� �� �׺���̼� ��� ��Ȱ��ȭ
            navMeshAgent.enabled = false;
		}
        else
		{
            //�÷��̾� ���� �̵�
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }
}
