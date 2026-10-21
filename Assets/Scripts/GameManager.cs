using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static readonly string FormatScore = "SCORE: {0}";
    public TMP_Text scoreText;
    public GameObject gameOverUI;
    public bool IsGameOver {get; private set;}
    private int score = 0;

    private void Start()
    {
        SetScore(score);
        gameOverUI.SetActive(false);
    }

    private void Update()
    {
        if(IsGameOver && Input.GetMouseButtonDown(0)) //Input.GetKeyDown(KeyCode.Space)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = string.Format(FormatScore, score);
    }

    public void AddScore(int add)
    {
        if (IsGameOver)
        {
            return;
        }
        SetScore(score + add);
    }

    public void OnPlayerDead()
    {
        IsGameOver = true;
        gameOverUI.SetActive(true);
    } 
}
