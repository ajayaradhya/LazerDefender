
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
        if (PlayerPrefs.GetInt("NewHighScore") == default(int))
        {
            PlayerPrefs.SetInt("NewHighScore", 0);
        }

        var currentHighScoreInMemory = PlayerPrefs.GetInt("NewHighScore");
        var currentGameScore = PlayerPrefs.GetInt("Score");

        if (currentHighScoreInMemory < currentGameScore)
        {
            PlayerPrefs.SetInt("NewHighScore", currentGameScore);
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
                scoreText.text = PlayerPrefs.GetInt("NewHighScore").ToString();
            }
            PlayGamesController.AddScoreToLeaderBoard(GPGSIds.leaderboard_top_defenders, PlayerPrefs.GetInt("NewHighScore"));
        }
    }

    public void UpdateScoreBy(int scoreToAdd)
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
        PlayerPrefs.SetInt("Score", 0);
    }

    

}
