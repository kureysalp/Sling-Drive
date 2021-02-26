using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int score;

    [Header("UI")]
    public Text scoreCounter;
    public GameObject startScreen;
    public GameObject loseScreen;
    public GameObject levelPassText;


    private void Start()
    {
        PlayerActions.ExitedCorner += CornerPassed;
        EventManager.GameStarted += StartGame;
        EventManager.GameFailed += LoseGame;
        EventManager.LevelPassed += PassedLevel;
        EventManager.LevelExited += ExitedLevel;
    }

    private void StartGame()
    {
        startScreen.SetActive(false);
    }

    private void LoseGame()
    {
        loseScreen.SetActive(true);
    }

    public void RestartGame()
    {
        EventManager.RestartGame();

        score = 0;
        scoreCounter.text = $"{score}";        
    }

    private void CornerPassed()
    {
        score++;
        scoreCounter.text = $"{score}";
    }

    private void PassedLevel()
    {
        levelPassText.SetActive(true);
    }

    private void ExitedLevel()
    {
        levelPassText.SetActive(false);
    }
}
