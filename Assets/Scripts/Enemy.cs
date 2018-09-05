﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject lazerPrefab;
    [SerializeField] GameObject blastEffectPrefab;

    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimebetweenShots = 3f;

	// Use this for initialization
	void Start () {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimebetweenShots);
	}
	
	// Update is called once per frame
	void Update ()
    {
        CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimebetweenShots);
        }
    }

    private void Fire()
    {
        Instantiate(lazerPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
        ProcessDamage(damageDealer);
        
    }

    private void ProcessDamage(DamageDealer damageDealer)
    {
        if(damageDealer == null) { return; }
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Destroy(gameObject);
            var blast = Instantiate(blastEffectPrefab, transform.position, Quaternion.identity);
            Destroy(blast, 1);
        }
    }
}
