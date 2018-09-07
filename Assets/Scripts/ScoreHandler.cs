using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [SerializeField] TMPro.TextMeshProUGUI healthText;
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

    public void UpdateHealth(int currentHealth)
    {
        if(healthText != null)
        {
            int currentHealthInUI;
            int.TryParse(healthText.text, out currentHealthInUI);

            if(currentHealthInUI >= 0)
            {
                healthText.text = currentHealth.ToString();
            }
            
        }
        
    }

}
