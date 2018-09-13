using System;
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
        Debug.Log("Level Controller setup done..");
        allScenes = GetAllScenes();
        Debug.Log("Number of scenes found : " + allScenes.Length);
        currentSceneIndex = 0;
        Debug.Log("Start " + currentSceneIndex);
        //start scene, level1, level2,...., success scene, gameover scene
    }

    public void LoadStartMenu()
    {
        currentSceneIndex = 0;
        Debug.Log("LoadStartMenu " + currentSceneIndex);
        ScoreHandler.instance.ResetScores();

        if(allScenes == null && allScenes.Length ==0)
        {
            allScenes = GetAllScenes();
        }
        
        SceneManager.LoadScene(allScenes[0]);
    }

    public void LoadFirstLevel()
    {
        LevelController.instance.LoadFirstLevelByInstance();
    }

    private void LoadFirstLevelByInstance()
    {
        currentSceneIndex = 1;
        Debug.Log("LoadFirstLevel : " + allScenes.Length);
        SceneManager.LoadScene(allScenes[currentSceneIndex]);
    }

    public void LoadNextLevel()
    {
        Debug.Log("LoadNextLevel : " + currentSceneIndex);
        try
        {
            currentSceneIndex++;

            Debug.Log("Loading scene " + allScenes[currentSceneIndex]);
            SceneManager.LoadScene(allScenes[currentSceneIndex]);
        }
        catch (Exception ex)
        {
            currentSceneIndex--;
            throw ex;
        }
        finally
        {
            Debug.Log("LoadNextLevel : " + currentSceneIndex);
        }
    }

    public void LoadGameOverScene()
    {
        currentSceneIndex = allScenes.Length - 1;
        Debug.Log("LoadGameOverScene " + currentSceneIndex);
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
