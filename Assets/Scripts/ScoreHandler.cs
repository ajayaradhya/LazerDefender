using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [SerializeField] TMPro.TextMeshProUGUI healthText;

    private int totalScore = 0;

    public void UpdateScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        scoreText.text = totalScore.ToString();
    }

    public void UpdateHealth(int currentHealth)
    {
        if(healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
        
    }

}
