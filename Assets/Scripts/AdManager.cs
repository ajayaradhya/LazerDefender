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
                LevelController.instance.LoadFirstLevelWithoutScoreReset();
                break;
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                break;
        }
    }
}
