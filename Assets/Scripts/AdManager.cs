using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowNormalAds()
    {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
    }

    public void ShowRewardsAds()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResults });
        }
    }

    void HandleAdResults(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
                Debug.Log("Player has seen ads. Time for rewards.");
                LevelController.instance.LoadFirstLevelWithoutScoreReset();
                break;
            case ShowResult.Failed:
                Debug.Log("Failed to watch ads.");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad watching skipped. No rewards.");
                break;
        }
    }
}
