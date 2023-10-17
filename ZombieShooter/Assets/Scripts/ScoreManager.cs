using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0; //점수 (스태틱 변수)

    public Text scoreText; //점수 표시 텍스트

    void Update()
    {
        //점수 출력 처리
        scoreText.text = "Score : " + score;
    }
}
