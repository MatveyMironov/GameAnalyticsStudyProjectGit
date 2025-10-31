using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDisplayerMB : MonoBehaviour
{
    [SerializeField] private Slider passedTimeSlider;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private GameControllerMB gameController;

    private void Start()
    {
        gameController.OnPassedTimeChaged += DisplayPassedTime;
        gameController.OnPointsChanged += DisplayPoints;
    }

    private void DisplayPassedTime(float passedTime)
    {
        passedTimeSlider.value = passedTime;
    }

    private void DisplayPoints(int points)
    {
        pointsText.text = "Points: " + points.ToString();
    }
}