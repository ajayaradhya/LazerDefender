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

        Debug.Log("score updated " + scoreText.text);
    }

    public void UpdateHealth(int currentHealth)
    {
        healthText.text = currentHealth.ToString();
        Debug.Log("health updated " + healthText.text);
    }

}
