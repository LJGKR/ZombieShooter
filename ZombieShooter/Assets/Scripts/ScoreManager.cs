using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0; //���� (����ƽ ����)

    public Text scoreText; //���� ǥ�� �ؽ�Ʈ

    void Update()
    {
        //���� ��� ó��
        scoreText.text = "Score : " + score;
    }
}
