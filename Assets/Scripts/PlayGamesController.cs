using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public class PlayGamesController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
    }

    private void SignIn()
    {
        Social.localUser.Authenticate(success => { });
    }

    public static void AddScoreToLeaderBoard(string leaderBoard, int score)
    {
        Social.ReportScore(score, leaderBoard, success => { });
    }

    public static void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
}
