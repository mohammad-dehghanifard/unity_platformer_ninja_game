using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreController : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score;

    private void Start()
    {
        score = PlayerPrefs.GetInt("PlayerScore");
        scoreText.text = "Score : " + score.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Gem"))
        {
            Destroy(collision.gameObject);
            score++;
            PlayerPrefs.SetInt("PlayerScore", score);
            scoreText.text = "Score : " + score.ToString();  
        }
    }
}
