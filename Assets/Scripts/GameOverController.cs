using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI lastCoinsText;

    void Start()
    {
        // קריאת הנתונים מה-PlayerPrefs
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        int lastCoins = PlayerPrefs.GetInt("LastCoins", 0);

        // הצגת הנתונים במסך הסיום
        lastScoreText.text = "Score: " + lastScore.ToString();
        lastCoinsText.text = "Coins Collected: " + lastCoins.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("Shop");
    }
}
