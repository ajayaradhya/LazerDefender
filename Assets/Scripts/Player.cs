﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player Movements")]
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float padding = 1f;
    [SerializeField] float paddingTop = 3f;

    [Header("Lazer Related")]
    [SerializeField] GameObject playerLazer;
    [SerializeField] float periodOfContinuousFiring = 0.1f;

    [Header("Player Health and Death")]
    [SerializeField] int health = 200;
    [SerializeField] AudioClip playerDeathAudio;
    [SerializeField] [Range(0, 1)] float soundVolume = 0.75f;

    [Header("Level")]
    [SerializeField] GameObject levelHandler;


    float xMin, xMax, yMin, yMax;
    Coroutine fireCoroutine;

    // Use this for initialization
    void Start () {
        SetUpMoveBoundaries();
        var scoreHandler = FindObjectOfType<ScoreHandler>();
        if(scoreHandler != null)
        {
            scoreHandler.UpdateHealth(health);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //MoveHorizontal();
        //MoveVertical();
        Move();
        Fire();
    }

    private void Move()
    {
        var currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var newXPos = Mathf.Clamp(currentMousePosition.x, xMin, xMax);
        var newYPos = Mathf.Clamp(currentMousePosition.y, yMin, yMax);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(newXPos, newYPos), moveSpeed * Time.deltaTime);

    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1") )
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            Instantiate(playerLazer, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(periodOfContinuousFiring);
        }
    }

    private void MoveVertical()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(transform.position.x, newYPos);
    }

    private void MoveHorizontal()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        transform.position = new Vector2(newXPos, transform.position.y);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - paddingTop;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var damageDealer = collider.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();

        FindObjectOfType<ScoreHandler>().UpdateHealth(health);

        damageDealer.Hit();
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(playerDeathAudio, Camera.main.transform.position, soundVolume);
            Destroy(gameObject);

            levelHandler.GetComponent<Level>().LoadGameOverScene();
        }
    }
}
