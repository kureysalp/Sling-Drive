using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void OnGameStarted();
    public static event OnGameStarted GameStarted;

    public delegate void OnGameFailed();
    public static event OnGameStarted GameFailed;

    public delegate void OnGameRestarted();
    public static event OnGameRestarted GameRestarted;

    public delegate void OnLevelPassed();
    public static event OnLevelPassed LevelPassed;

    public delegate void OnLevelExit();
    public static event OnLevelExit LevelExited;

    public static void StartGame()
    {
        GameStarted?.Invoke();
    }

    public static void FailGame()
    {
        GameFailed?.Invoke();
    }

    public static void RestartGame()
    {
        GameRestarted?.Invoke();
    }

    public static void PassLevel()
    {
        LevelPassed?.Invoke();
    }

    public static void ExitLevel()
    {
        LevelExited?.Invoke();
    }
}
