using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenManager : MonoBehaviour
{
    public GameObject enemyPrefab; //���� ���� ������

    public float genTime; //���� �ֱ�

    public Transform[] spawnPoints; //���� ��ġ

    void Start()
    {
        InvokeRepeating("SpawnTimer", genTime, genTime);
    }

    void SpawnTimer()
	{
        //���� ��ġ�� �����ϰ� ����
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        //���� ����
        Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}
