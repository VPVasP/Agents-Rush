using System.Collections;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("UI Elements")]
    public TextMeshProUGUI enemiesRemainingText;
    public GameObject winText;
    public GameObject[] waveTexts;

    public TextMeshProUGUI enemyText, playerHealthText, playerRageText;


    private void Awake()
    {
        //singleton pattern
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    //disable all notifications ui at the start
    private void Start()
    {
        DisableNotificationsAtStart();
    }
    //updates the UI text for enemies remaining
    public void UpdateEnemiesRemaining(int remaining)
    {
        enemiesRemainingText.text = "Enemies remaining: " + remaining.ToString();
    }
    ///displays the win text on the screen.
    public void ShowWinText()
    {
        winText.SetActive(true);
    }

    //shows a wave text message for a specified duration
    public void ShowWaveText(int index, string message, float duration)
    {
        if (index >= 0 && index < waveTexts.Length)
        {
            waveTexts[index].SetActive(true);
            waveTexts[index].GetComponent<TextMeshProUGUI>().text = message;
            Invoke(nameof(HideWaveText), duration);
        }
    }

    //hides all wave text messages
    public void HideWaveText()
    {
        foreach (var text in waveTexts)
        {
            text.SetActive(false);
        }
    }
    //notification creator
    public void Notification(TextMeshProUGUI notificationText, string nameOfMessage, Color color, string messageInfo)
    {
        notificationText.color = color;
        notificationText.text = nameOfMessage + messageInfo;

        StartCoroutine(ShowNotification(notificationText, nameOfMessage, color, messageInfo));
    }
    //disable all the notifications
    private void DisableNotificationsAtStart()
    {
        enemyText.gameObject.SetActive(false);
        playerHealthText.gameObject.SetActive(false);
        playerRageText.gameObject.SetActive(false);
    }
    //show notification and disable notification
    private IEnumerator ShowNotification(TextMeshProUGUI notificationText, string nameOfMessage, Color color, string messageInfo)
    {
        //set the color and text and then enable the notificationText
        notificationText.color = color;
        notificationText.text = nameOfMessage + messageInfo;
        notificationText.gameObject.SetActive(true);

        //wait for 2 seconds before disabling the notification again
        yield return new WaitForSeconds(2f);

        //disable the notificationText
        notificationText.gameObject.SetActive(false);
    }
}