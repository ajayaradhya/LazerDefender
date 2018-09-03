using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float padding = 1f;
    [SerializeField] float paddingTop = 3f;

    [SerializeField] GameObject playerLazer;
    [SerializeField] float periodOfContinuousFiring = 0.1f;


    float xMin, xMax, yMin, yMax;
    Coroutine fireCoroutine;

    // Use this for initialization
    void Start () {
        SetUpMoveBoundaries();
	}

    // Update is called once per frame
    void Update ()
    {
        MoveHorizontal();
        MoveVertical();
        Fire();
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
            var lazer = Instantiate(playerLazer, transform.position, Quaternion.identity) as GameObject;
            Destroy(lazer, 1);
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
}
