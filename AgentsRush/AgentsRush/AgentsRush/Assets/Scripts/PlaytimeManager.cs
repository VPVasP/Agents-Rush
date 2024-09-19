using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlaytimeManager : MonoBehaviour
{
    [SerializeField] private float currentplayTime;
    [SerializeField] TextMeshProUGUI playtimeText;
    [SerializeField] private Slider timerSlider;
    int minutes = 0;
    int seconds = 0;
    private void Update()
    {
        CountDownTimer();
        UpdateCountDownTimerUI();
        EndCountDownTimer();
    }
    //update the countdownTimer minutes and seconds
    private void CountDownTimer()
    {
        currentplayTime -= Time.deltaTime;
        minutes = Mathf.FloorToInt(currentplayTime / 60);
        seconds = Mathf.FloorToInt(currentplayTime % 60);
    }
    private void EndCountDownTimer()
    {
        if (currentplayTime == 0) {

            //disable the UI Elements
            gameObject.SetActive(false);
            GameManager.instance.PlayEnd();
        }
    }
    //update countdown timer UI for both slider and the text
    private void UpdateCountDownTimerUI()
    {
        timerSlider.value = currentplayTime;
        string currentTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        playtimeText.text = currentTime;
    }
}
