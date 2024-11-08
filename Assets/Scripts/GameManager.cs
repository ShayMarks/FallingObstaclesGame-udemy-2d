using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int coinsCollected = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;

    private ObstacleSpawner obstacleSpawner;

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // שמירת GameManager בין סצנות
            SceneManager.sceneLoaded += OnSceneLoaded; // רישום לאירוע טעינת סצנה
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnDestroy()
    {
        // ביטול רישום לאירוע טעינת סצנה
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            // מציאת אובייקטי הטקסט בסצנת המשחק
            GameObject scoreTextObj = GameObject.Find("ScoreText");
            GameObject coinsTextObj = GameObject.Find("CoinsText");

            if (scoreTextObj != null && coinsTextObj != null)
            {
                scoreText = scoreTextObj.GetComponent<TextMeshProUGUI>();
                coinsText = coinsTextObj.GetComponent<TextMeshProUGUI>();

                Debug.Log("ScoreText ו-CoinsText נמצאו והוקצו כראוי.");
            }
            else
            {
                Debug.LogError("לא נמצא ScoreText או CoinsText בסצנת GameScene.");
            }

            // מציאת ObstacleSpawner בסצנה
            obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
            if (obstacleSpawner != null)
            {
                obstacleSpawner.ResetSpawner();
                Debug.Log("ObstacleSpawner reset.");
            }
            else
            {
                Debug.LogError("לא נמצא ObstacleSpawner בסצנת GameScene.");
            }

            // איפוס הניקוד והמטבעות להתחלת משחק חדש
            score = 0;
            coinsCollected = 0;

            UpdateScoreText();
            UpdateCoinsText();
        }
        else
        {
            // ניקוי הפניות כשאינך בסצנת GameScene
            scoreText = null;
            coinsText = null;
            obstacleSpawner = null;
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    void UpdateCoinsText()
    {
        if (coinsText != null)
            coinsText.text = "Coins: " + coinsCollected.ToString();
    }

    public void AddScore()
    {
        score++;
        UpdateScoreText();
    }

    public void AddCoin()
    {
        coinsCollected++;
        //coinsCollected += 1000;
        UpdateCoinsText();
    }

    public void GameOver()
    {
        // שמירת השיא אם נחצה
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        // עדכון המטבעות הכוללים
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += coinsCollected;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);

        // שמירת הניקוד האחרון
        PlayerPrefs.SetInt("LastScore", score);
        PlayerPrefs.SetInt("LastCoins", coinsCollected);

        // שמירת הנתונים ב-PlayerPrefs
        PlayerPrefs.Save();

        // טעינת מסך הסיום
        SceneManager.LoadScene("GameOver");
    }
}
