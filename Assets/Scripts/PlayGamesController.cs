using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public class PlayGamesController : MonoBehaviour
{
    private bool isAuthenticated = false;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);

        try
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            GameObject.FindGameObjectWithTag("LoginStatus").GetComponent<TMPro.TextMeshProUGUI>().text = "not connected";
            SignIn();
        }
        catch(Exception ex)
        {
            Debug.Log("ajay : unable to setup google play acc " + ex.InnerException);
        }
    }

    private void SignIn()
    {
        Debug.Log("Sign in called..");
        Social.localUser.Authenticate((bool success) => {
            isAuthenticated = success;
            GameObject.FindGameObjectWithTag("LoginStatus").GetComponent<TMPro.TextMeshProUGUI>().text = "success";
        });
    }

    public void AddScoreToLeaderBoard(string leaderBoard, int score)
    {
        if(isAuthenticated)
        {
            Social.ReportScore(score, leaderBoard, success => { });
        }
        else
        {
            Debug.Log("ajay : to connect to services");
        }
        
    }

    public void ShowLeaderBoard()
    {
        if (isAuthenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("ajay : to connect to services");
        }
        
    }
}
