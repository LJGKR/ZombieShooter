using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public ParticleSystem hitParticle; //피격에 대한 이펙트 파티클

    public int startingHealth; //초기 체력값

    public int currentHealth; //현재 체력값

    private bool isDead = false; //사망 여부

    public Animator animator; //몬스터 애니메이터

    public Collider capsuleCollider; //캡슐 콜라이더

    public ParticleSystem deathParticle; //사망 이펙트 파티클

    bool isSinking = false; //가라앉기 상태

    public float sinKingSpeed; //가라앉기 속도

    public int deathPoint; //점수

    private void Awake()
    {
        //현재 체력을 시작 체력으로 설정
        currentHealth = startingHealth;
    }

    public void Death()
	{
        isDead = true; //사망 여부 설정

        ScoreManager.score += deathPoint; //스코어 점수 반영

        //Collision 캡슐 콜라이더를 Trigger 콜라이더로 변경
        capsuleCollider.isTrigger = true;

        //사망 애니메이션 재생
        animator.SetTrigger("Die");
	}

	private void Update()
	{
		//가라앉기 상태라면
        if(isSinking)
		{
            //바닥으로 이동 처리
            transform.Translate(Vector3.down * sinKingSpeed * Time.deltaTime);
		}
	}

	public void TakeDamage(int amount, Vector3 hitPoint)
	{
        // 체력 감소
        currentHealth -= amount;

        //현재 체력이 0이고 사망상태가 아니라면
        if(currentHealth <= 0 && !isDead)
		{
            //사망 처리
            Death();
            return; //void 타입의 메소드 실행 중단
		}

        //파티클 표시 위치 설정
        hitParticle.transform.position = hitPoint;

        hitParticle.Play(); //이펙트 재생
	}

    //가라앉기 기능 애니메이션 이벤트 메소드
    public void StartSinking()
	{
        //Debug.Log("StartSinking");

        //가라앉기 상태 변경
        isSinking = true;

        //사망 이펙트 재생
        deathParticle.Play();

        //네비게이션 에이전트 컴포넌트 비활성화
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        //물리적 현상을 무시하도록 Rigidbody 설정
        GetComponent<Rigidbody>().isKinematic = false;

        //오브젝트 파괴
        Destroy(gameObject, 2f);
	}
}
