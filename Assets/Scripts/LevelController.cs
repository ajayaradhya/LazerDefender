﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    [SerializeField] float delayBeforeScreenLoad = 1f;
    public static LevelController instance = null;
    [SerializeField] int currentSceneIndex = 0;

    [SerializeField] string[] allScenes;

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

    void Start()
    {
        allScenes = GetAllScenes();
        currentSceneIndex = 0;
    }

    public void LoadStartMenu()
    {
        LevelController.instance.LoadStartMenuByInstance();
    }

    public void LoadFirstLevel()
    {
        LevelController.instance.LoadFirstLevelByInstance();
    }

    private void LoadStartMenuByInstance()
    {
        currentSceneIndex = 0;
        ScoreHandler.instance.ResetScores();

        if(allScenes == null && allScenes.Length ==0)
        {
            allScenes = GetAllScenes();
        }
        
        SceneManager.LoadScene(allScenes[0]);
    }

    private void LoadFirstLevelByInstance()
    {
        currentSceneIndex = 1;
        ScoreHandler.instance.ResetScores();
        SceneManager.LoadScene(allScenes[currentSceneIndex]);
    }

    public void LoadNextLevel()
    {
        try
        {
            currentSceneIndex++;

            if(currentSceneIndex > allScenes.Length - 2)
            {
                currentSceneIndex = allScenes.Length - 2;
            }
            
            SceneManager.LoadScene(allScenes[currentSceneIndex]);
        }
        catch (Exception ex)
        {
            currentSceneIndex--;
            throw ex;
        }
        finally
        {
        }
    }

    public void LoadGameOverScene()
    {
        currentSceneIndex = allScenes.Length - 1;
        StartCoroutine(DelayBeforeLoadingScene(allScenes[currentSceneIndex]));
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

    IEnumerator LevelCompletionTransition()
    {

    }


    private string[] GetAllScenes()
    {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        string[] scenes = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
        }

        return scenes;
    }

    public string GetCurrentLevel()
    {
        return allScenes[currentSceneIndex];
    }
}
