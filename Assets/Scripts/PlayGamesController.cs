using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public class PlayGamesController : MonoBehaviour
{
    [SerializeField] GameObject leaderBoardUI;

    private static bool isAuthenticated = false;

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
            SignIn();
        }
        catch (Exception ex)
        {
            Debug.Log("Unable to setup google play account " + ex.InnerException);
        }
    }

    private void SignIn()
    {
        Debug.Log("Sign in called..");
        Social.localUser.Authenticate((bool success) => {
            isAuthenticated = success;

            if (isAuthenticated)
            {
                GameObject.FindGameObjectWithTag("GooglePlayUserName").GetComponent<TMPro.TextMeshProUGUI>().text = Social.localUser.userName;
            }
        });
    }

    public static void AddScoreToLeaderBoard(string leaderBoard, int score)
    {
        Debug.Log("adding to leaderboard " + score);
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderBoard, success => { });
        }
    }

    public void ShowLeaderBoard()
    {
        Debug.Log("showing leaderboard..");
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }
}