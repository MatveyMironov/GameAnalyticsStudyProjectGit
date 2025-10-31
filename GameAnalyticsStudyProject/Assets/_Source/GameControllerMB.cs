using GameAnalyticsSDK;
using System;
using System.Collections;
using UnityEngine;

public class GameControllerMB : MonoBehaviour
{
    [SerializeField] private int requiredPoints;
    [SerializeField] private float availableTime;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject lossPanel;

    public bool IsGameStarted { get; private set; }
    public int Points { get; private set; }

    public event Action<int> OnPointsChanged;
    public event Action<float> OnPassedTimeChaged;

    private Coroutine _countingTime;

    private void Start()
    {
        gamePanel.SetActive(false);
        winPanel.SetActive(false);
        lossPanel.SetActive(false);
    }

    public void StartGame()
    {
        if (IsGameStarted) return;
        IsGameStarted = true;

        gamePanel.SetActive(true);

        _countingTime = StartCoroutine(CountingTime());

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Game");
        Debug.Log("Game started");
    }

    public void AddPoint()
    {
        Points++;
        OnPointsChanged?.Invoke(Points);

        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Points", Points, "", "");
        Debug.Log("Point added");

        if (Points >= requiredPoints)
        {
            Win();
        }
    }

    private void Win()
    {
        winPanel.SetActive(true);

        GameAnalytics.NewDesignEvent("Game won");
        Debug.Log("Game won");

        FinishGame();
    }

    private void Lose()
    {
        lossPanel.SetActive(true);

        GameAnalytics.NewDesignEvent("Game lost");
        Debug.Log("Game lost");

        FinishGame();
    }

    private void FinishGame()
    {
        gamePanel.SetActive(false);

        StopCoroutine(_countingTime);

        Points = 0;
        OnPointsChanged?.Invoke(Points);

        IsGameStarted = false;

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Game");
        Debug.Log("Game finished");
    }

    private IEnumerator CountingTime()
    {
        float passedTime = 0.0f;

        while (passedTime < availableTime)
        {
            yield return null;

            passedTime += Time.deltaTime;
            OnPassedTimeChaged?.Invoke(passedTime / availableTime);
        }

        Lose();
    }
}
