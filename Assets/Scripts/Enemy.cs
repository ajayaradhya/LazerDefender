using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject lazerPrefab;
    [SerializeField] GameObject blastEffectPrefab;
    [SerializeField] GameObject enemyHealthBarPrefab;
    [SerializeField] AudioClip blastAudio;
    [SerializeField] [Range(0, 1)] float blastSoundVolume = 0.75f;

    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimebetweenShots = 3f;
    [SerializeField] float healthBarOffsetY = 2f;

    [SerializeField] int scoreGainedByPlayerAfterEnemyDeath = 100;

    private GameObject healthBar;
    private float initialHealth;

    // Use this for initialization
    void Start ()
    {
        initialHealth = health;
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimebetweenShots);
        InstantiateHealthBar();

    }

    private void InstantiateHealthBar()
    {
        var positionOfHealthBar = new Vector2(transform.position.x, transform.position.y + healthBarOffsetY);
        var healthBarPos = Camera.main.WorldToScreenPoint(positionOfHealthBar);
        if (enemyHealthBarPrefab != null)
        {
            healthBar = Instantiate(enemyHealthBarPrefab, healthBarPos, Quaternion.identity);
            healthBar.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        CountDownAndShoot();

        if(healthBar != null)
        {
            var positionOfHealthBar = new Vector2(transform.position.x, transform.position.y + healthBarOffsetY);
            var healthBarPos = Camera.main.WorldToScreenPoint(positionOfHealthBar);
            healthBar.transform.position = healthBarPos;

            healthBar.GetComponent<Slider>().value = health / initialHealth;
        }
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
            Die();
        }
    }

    private void Die()
    {
        ScoreHandler.instance.UpdateScore(scoreGainedByPlayerAfterEnemyDeath);
        AudioSource.PlayClipAtPoint(blastAudio, Camera.main.transform.position, blastSoundVolume);
        Destroy(gameObject);
        var blast = Instantiate(blastEffectPrefab, transform.position, Quaternion.identity);
        Destroy(blast, 1);
    }

    void OnDestroy()
    {
        if(healthBar!=null)
        {
            Destroy(healthBar);
        }
    }
}
