﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazer : MonoBehaviour {

    [SerializeField] float lazerSpeed = 2f;
    [SerializeField] AudioClip enemyLazerAudio;
    [SerializeField] [Range(0, 1)] float lazerSoundVolume = 0.75f;

    // Use this for initialization
    void Start ()
    {
        AudioSource.PlayClipAtPoint(enemyLazerAudio, Camera.main.transform.position, lazerSoundVolume);
	}
	
	// Update is called once per frame
	void Update () {
        var newPosition = transform.position.y - lazerSpeed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x, newPosition);
    }
}
