using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //플레이어 참조(추적을 위해)
    public Transform playerTransform;

    //몬스터 애니메이터 참조
    public Animator animator;

    //몬스터 네비게이션 에이전트 참조
    public NavMeshAgent navMeshAgent;

    public EnemyHealth health;

    void Start()
    {
        //플레이어 게임 오브젝트를 찾고(태그가 플레이어인) 오브젝트를 이용해 플레이어의 트랜스폼 값을 참조함
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
   
    void Update()
    {
        //몬스터의 체력이 0이면
        if(health.currentHealth <= 0)
		{
            //추적 중지 및 네비게이션 기능 비활성화
            navMeshAgent.enabled = false;
		}
        else
		{
            //플레이어 추적 이동
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }
}
