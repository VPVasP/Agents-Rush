using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("UI Elements")]
    public TextMeshProUGUI enemiesRemainingText;
    public GameObject winText;
    public GameObject[] waveTexts;

    private void Awake()
    {
        //singleton pattern
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
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
}