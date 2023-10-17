using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public ParticleSystem hitParticle; //�ǰݿ� ���� ����Ʈ ��ƼŬ

    public int startingHealth; //�ʱ� ü�°�

    public int currentHealth; //���� ü�°�

    private bool isDead = false; //��� ����

    public Animator animator; //���� �ִϸ�����

    public Collider capsuleCollider; //ĸ�� �ݶ��̴�

    public ParticleSystem deathParticle; //��� ����Ʈ ��ƼŬ

    bool isSinking = false; //����ɱ� ����

    public float sinKingSpeed; //����ɱ� �ӵ�

    public int deathPoint; //����

    private void Awake()
    {
        //���� ü���� ���� ü������ ����
        currentHealth = startingHealth;
    }

    public void Death()
	{
        isDead = true; //��� ���� ����

        ScoreManager.score += deathPoint; //���ھ� ���� �ݿ�

        //Collision ĸ�� �ݶ��̴��� Trigger �ݶ��̴��� ����
        capsuleCollider.isTrigger = true;

        //��� �ִϸ��̼� ���
        animator.SetTrigger("Die");
	}

	private void Update()
	{
		//����ɱ� ���¶��
        if(isSinking)
		{
            //�ٴ����� �̵� ó��
            transform.Translate(Vector3.down * sinKingSpeed * Time.deltaTime);
		}
	}

	public void TakeDamage(int amount, Vector3 hitPoint)
	{
        // ü�� ����
        currentHealth -= amount;

        //���� ü���� 0�̰� ������°� �ƴ϶��
        if(currentHealth <= 0 && !isDead)
		{
            //��� ó��
            Death();
            return; //void Ÿ���� �޼ҵ� ���� �ߴ�
		}

        //��ƼŬ ǥ�� ��ġ ����
        hitParticle.transform.position = hitPoint;

        hitParticle.Play(); //����Ʈ ���
	}

    //����ɱ� ��� �ִϸ��̼� �̺�Ʈ �޼ҵ�
    public void StartSinking()
	{
        //Debug.Log("StartSinking");

        //����ɱ� ���� ����
        isSinking = true;

        //��� ����Ʈ ���
        deathParticle.Play();

        //�׺���̼� ������Ʈ ������Ʈ ��Ȱ��ȭ
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        //������ ������ �����ϵ��� Rigidbody ����
        GetComponent<Rigidbody>().isKinematic = false;

        //������Ʈ �ı�
        Destroy(gameObject, 2f);
	}
}
