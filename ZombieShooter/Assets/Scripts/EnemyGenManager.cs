using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenManager : MonoBehaviour
{
    public GameObject enemyPrefab; //생성 몬스터 프리팹

    public float genTime; //생성 주기

    public Transform[] spawnPoints; //생성 위치

    void Start()
    {
        InvokeRepeating("SpawnTimer", genTime, genTime);
    }

    void SpawnTimer()
	{
        //생성 위치를 랜덤하게 결정
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        //몬스터 생성
        Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}
