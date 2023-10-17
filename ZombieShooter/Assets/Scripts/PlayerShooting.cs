using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot; //피격 데미지(공격력)
    public float timeBetweenBullets; //발포 주기
    public float range; //발포 길이
    private float timer; //발포 주기 계산용 시간

    private Ray shootRay = new Ray(); //발포 충돌 레이
    private RaycastHit shootHit; //발포 충돌 정보
    private int shootableMask; //발포 피격 대상 충돌 레이어
    public ParticleSystem gunParticleSys; //발포 이펙트(파티클 시스템) 참조

    public LineRenderer gunLine; //발포 탄막 잔상 효과
    public Light faceLight; //발포 잔상 효과 라이트(Spot - 후레쉬)
    public Light gunLight; //발포 효과 라이트(Point - 펑 할때 나는 발포 빛)
    public float effectDisplayTime; //발포 효과 이펙트 표시 시간

    public AudioSource audioSource; //소리 재생 컴포넌트
    public AudioClip shootAudio; //발포 소리

	private void Awake()
	{
        shootableMask = LayerMask.GetMask("Shootable");
        audioSource.clip = shootAudio;
	}

	private void Update()
	{
        // 렌더링 프레임과 프레임 사이의 간격 시간을 timer에 더함
        // * 매 프레임마다의 Time.deltaTime을 더하면 1초가 됨(60 프레임 기준 : 60 * Time.deltaTime = 1초)
        timer += Time.deltaTime;

        //Fire1 -> 마우스 왼쪽 버튼
        // -> Project Settings / Input에 설정된 값

        //마우스 왼쪽 버튼이 눌리고 발포 가능 시간 상태면
        if(Input.GetButton("Fire1") && timer >= timeBetweenBullets)
		{
            //발포
            Shoot();
		}

        //이펙트 효과 재생 시간이 넘으면
        if(timer >= timeBetweenBullets * effectDisplayTime)
		{
            //이펙트 효과 비활성화
            gunLight.enabled = false;
            faceLight.enabled = false;
            gunLine.enabled = false;
		}
	}

    void Shoot()
	{
        audioSource.Play();

        timer = 0; //발포 주기 계산 변수 초기화

        gunLight.enabled = true; //발포 효과 라이트 활성화
        faceLight.enabled = true; //발포 잔상 효과 라이트 활성화

        gunParticleSys.Stop(); //이전 파티클 효과 재생 중지
        gunParticleSys.Play(); //발포 파티클 효과 재생

        gunLine.enabled = true; //발포 탄막 효과(라인 그리기)

        //라인 렌더러의 그리기 시작 위치 설정 (발포 총구 위치)
        gunLine.SetPosition(0, transform.position);

        //충돌 레이의 시작위치 설정(발포 총구 위치)
        shootRay.origin = transform.position;
        //충돌 레이가 만들어지는 방향을 설정
        shootRay.direction = transform.forward;

        //레이캐스트 충돌 체크(레이, out 충돌정보, 레이길이, 충돌대상)
        if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
		{
            //충돌된 대상이 있으면
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            //피격 대상이 몬스터라면
            if(enemyHealth != null)
			{
                //몬스터의 피격 메소드를 호출함
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
			}

            //라인의 길이를 충돌 대상의 위치로 그려라
            gunLine.SetPosition(1, shootHit.point);
		}
        else //레이 충돌이 일어나지 않았으면
		{
            // range 길이만큼 라인을 그려라
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
		}
	}
}
