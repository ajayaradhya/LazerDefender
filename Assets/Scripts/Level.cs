using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    [SerializeField] float delayBeforeScreenLoad = 2f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(DelayBeforeLoadingScene("GameOver"));
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator DelayBeforeLoadingScene(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeScreenLoad);
        SceneManager.LoadScene(sceneName);
    }

}
