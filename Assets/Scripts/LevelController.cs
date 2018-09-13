using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    [SerializeField] float delayBeforeScreenLoad = 1f;

    public static LevelController instance = null;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private int currentLevel = 1;

    void Start()
    {
        Debug.Log("Level Controller setup done..");
    }

    public void LoadStartMenu()
    {
        currentLevel = 1;
        ScoreHandler.instance.ResetScores();
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadGameOverScene()
    {
        currentLevel = 1;
        StartCoroutine(DelayBeforeLoadingScene("GameOver"));
    }

    public void LoadNextLevel()
    {
        try
        {
            currentLevel++;
            Debug.Log("loading scene " + currentLevel);
            SceneManager.LoadScene("Level " + currentLevel);
        }
        catch(Exception ex)
        {
            currentLevel--;
            throw ex;
        }
        
    }

    public void QuitGame()
    {
        ScoreHandler.instance.ResetScores();
        Application.Quit();
    }

    IEnumerator DelayBeforeLoadingScene(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeScreenLoad);
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }
}
