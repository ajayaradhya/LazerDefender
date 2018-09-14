using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private int totalScore = 0;

    public static ScoreHandler instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    { 
    }

    void Update()
    {
        UpdateScore();
    }

    private static void UpdateScore()
    {
        if (PlayerPrefs.GetInt("HighScore") == default(int))
        {
            Debug.Log("creating high score");
            PlayerPrefs.SetInt("HighScore", 0);
        }

        var currentHighScoreInMemory = PlayerPrefs.GetInt("HighScore");
        var currentGameScore = PlayerPrefs.GetInt("Score");
        if (currentHighScoreInMemory < currentGameScore)
        {
            PlayerPrefs.SetInt("HighScore", currentGameScore);
        }

        if (GameObject.FindGameObjectsWithTag("CurrentScore").Length > 0)
        {
            var scoreText = GameObject.FindGameObjectsWithTag("CurrentScore")[0].GetComponent<TMPro.TextMeshProUGUI>();
            if (scoreText != null)
            {
                scoreText.text = currentGameScore.ToString();
            }
        }

        if (GameObject.FindGameObjectsWithTag("FinalScore").Length > 0)
        {
            var scoreText = GameObject.FindGameObjectsWithTag("FinalScore")[0].GetComponent<TMPro.TextMeshProUGUI>();
            if (scoreText != null)
            {
                scoreText.text = currentGameScore.ToString();
            }
        }

        if (GameObject.FindGameObjectsWithTag("HighScore").Length > 0)
        {
            var scoreText = GameObject.FindGameObjectsWithTag("HighScore")[0].GetComponent<TMPro.TextMeshProUGUI>();
            if (scoreText != null)
            {
                scoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        PlayerPrefs.SetInt("Score", totalScore);

        if (GameObject.FindGameObjectsWithTag("CurrentScore").Length > 0)
        {
            var scoreText = GameObject.FindGameObjectsWithTag("CurrentScore")[0].GetComponent<TMPro.TextMeshProUGUI>();
            scoreText.text = totalScore.ToString();
        }
    }

    public void ResetScores()
    {
        totalScore = 0;
        PlayerPrefs.SetInt("Score", totalScore);
    }

    

}
