using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [SerializeField] TMPro.TextMeshProUGUI finalScoreText;

    private int totalScore = 0;

    public void Start()
    {
        if(finalScoreText != null)
        {
            finalScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        scoreText.text = totalScore.ToString();
        
        PlayerPrefs.SetInt("Score", totalScore);
    }

    

}
