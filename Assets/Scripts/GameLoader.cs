using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour {

    public LevelController gameManager;
    public ScoreHandler scoreController;

    // Use this for initialization
    void Awake () {
		if(gameManager == null)
        {
            Instantiate(gameManager);
        }

        if(scoreController)
        {
            Instantiate(scoreController);
        }
	}
}
