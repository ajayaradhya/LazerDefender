using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoss : MonoBehaviour {

    [SerializeField] List<GameObject> lazerPrefabs;
    [SerializeField] GameObject blastEffectPrefab;
    [SerializeField] GameObject enemyHealthBarPrefab;
    [SerializeField] AudioClip blastAudio;
    [SerializeField] [Range(0, 1)] float blastSoundVolume = 0.75f;

    [SerializeField] float health;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimebetweenShots = 3f;
    [SerializeField] float healthBarOffsetY = 1f;

    [SerializeField] int scoreGainedByPlayerAfterEnemyDeath = 500;

    private GameObject healthBar;
    private float initialHealth;


    // Use this for initialization
    void Start () {
        initialHealth = health;
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimebetweenShots);
        InstantiateHealthBar();
    }
	
	// Update is called once per frame
	void Update () {
        CountDownAndShoot();
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
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
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimebetweenShots);
        }
    }

    private void Fire()
    {
        var lazerSelected = lazerPrefabs[Random.Range(0, lazerPrefabs.Count)];
        Instantiate(lazerSelected, transform.position, Quaternion.identity);
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
        ProcessDamage(damageDealer);
    }

    private void ProcessDamage(DamageDealer damageDealer)
    {
        if (damageDealer == null) { return; }
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(CreateBlast(transform.position));
        AudioSource.PlayClipAtPoint(blastAudio, Camera.main.transform.position, blastSoundVolume);
        Destroy(gameObject);
        LevelController.instance.LoadNextLevel();
    }

    void OnDestroy()
    {
        if (healthBar != null)
        {
            ScoreHandler.instance.UpdateScore(scoreGainedByPlayerAfterEnemyDeath);
            Destroy(healthBar);
        }
    }

    IEnumerator CreateBlast(Vector3 position)
    {
        var blast = Instantiate(blastEffectPrefab, position, Quaternion.identity);
        Destroy(blast, 0.5f);
        yield return new WaitForSeconds(1f);
    }
}
