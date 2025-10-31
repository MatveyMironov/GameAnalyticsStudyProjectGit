using GameAnalyticsSDK;
using UnityEngine;

public class GameAnalyticsManagerMB : MonoBehaviour
{
    private void Start()
    {
        // Set Custom ID here if you need it for whatever reason

        GameAnalytics.Initialize();
        Debug.Log("GameAnalytics initialized");
    }

    private void OnApplicationQuit()
    {
        GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "Player quit your stupid game");
    }
}
