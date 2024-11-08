using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI totalCoinsText;

    void Start()
    {
        // טוען את השיא מה-PlayerPrefs
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();

        // טוען את כמות המטבעות הכוללת
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoinsText.text = "Total Coins: " + totalCoins.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("Shop");
    }
}
