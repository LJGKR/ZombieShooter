using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot; //�ǰ� ������(���ݷ�)
    public float timeBetweenBullets; //���� �ֱ�
    public float range; //���� ����
    private float timer; //���� �ֱ� ���� �ð�

    private Ray shootRay = new Ray(); //���� �浹 ����
    private RaycastHit shootHit; //���� �浹 ����
    private int shootableMask; //���� �ǰ� ��� �浹 ���̾�
    public ParticleSystem gunParticleSys; //���� ����Ʈ(��ƼŬ �ý���) ����

    public LineRenderer gunLine; //���� ź�� �ܻ� ȿ��
    public Light faceLight; //���� �ܻ� ȿ�� ����Ʈ(Spot - �ķ���)
    public Light gunLight; //���� ȿ�� ����Ʈ(Point - �� �Ҷ� ���� ���� ��)
    public float effectDisplayTime; //���� ȿ�� ����Ʈ ǥ�� �ð�

    public AudioSource audioSource; //�Ҹ� ��� ������Ʈ
    public AudioClip shootAudio; //���� �Ҹ�

	private void Awake()
	{
        shootableMask = LayerMask.GetMask("Shootable");
        audioSource.clip = shootAudio;
	}

	private void Update()
	{
        // ������ �����Ӱ� ������ ������ ���� �ð��� timer�� ����
        // * �� �����Ӹ����� Time.deltaTime�� ���ϸ� 1�ʰ� ��(60 ������ ���� : 60 * Time.deltaTime = 1��)
        timer += Time.deltaTime;

        //Fire1 -> ���콺 ���� ��ư
        // -> Project Settings / Input�� ������ ��

        //���콺 ���� ��ư�� ������ ���� ���� �ð� ���¸�
        if(Input.GetButton("Fire1") && timer >= timeBetweenBullets)
		{
            //����
            Shoot();
		}

        //����Ʈ ȿ�� ��� �ð��� ������
        if(timer >= timeBetweenBullets * effectDisplayTime)
		{
            //����Ʈ ȿ�� ��Ȱ��ȭ
            gunLight.enabled = false;
            faceLight.enabled = false;
            gunLine.enabled = false;
		}
	}

    void Shoot()
	{
        audioSource.Play();

        timer = 0; //���� �ֱ� ��� ���� �ʱ�ȭ

        gunLight.enabled = true; //���� ȿ�� ����Ʈ Ȱ��ȭ
        faceLight.enabled = true; //���� �ܻ� ȿ�� ����Ʈ Ȱ��ȭ

        gunParticleSys.Stop(); //���� ��ƼŬ ȿ�� ��� ����
        gunParticleSys.Play(); //���� ��ƼŬ ȿ�� ���

        gunLine.enabled = true; //���� ź�� ȿ��(���� �׸���)

        //���� �������� �׸��� ���� ��ġ ���� (���� �ѱ� ��ġ)
        gunLine.SetPosition(0, transform.position);

        //�浹 ������ ������ġ ����(���� �ѱ� ��ġ)
        shootRay.origin = transform.position;
        //�浹 ���̰� ��������� ������ ����
        shootRay.direction = transform.forward;

        //����ĳ��Ʈ �浹 üũ(����, out �浹����, ���̱���, �浹���)
        if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
		{
            //�浹�� ����� ������
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            //�ǰ� ����� ���Ͷ��
            if(enemyHealth != null)
			{
                //������ �ǰ� �޼ҵ带 ȣ����
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
			}

            //������ ���̸� �浹 ����� ��ġ�� �׷���
            gunLine.SetPosition(1, shootHit.point);
		}
        else //���� �浹�� �Ͼ�� �ʾ�����
		{
            // range ���̸�ŭ ������ �׷���
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
		}
	}
}
