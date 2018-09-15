using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupSpawner : MonoBehaviour {

    [SerializeField] float timeBetweenSpawns = 2f;
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] GameObject bossSpawnerPrefab;
    [SerializeField] bool looping = false;

    private bool doneWithTheLevel = false;

    // Use this for initialization
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);

        doneWithTheLevel = true;
    }

    void Update()
    {
        if(doneWithTheLevel)
        {
            LoadNextLevelIfAllEnemiesAreDead();
        }
    }

    private void LoadNextLevelIfAllEnemiesAreDead()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && GameObject.FindGameObjectsWithTag("EnemyBoss").Length <= 0)
        {
            Debug.Log("All Enemies are dead in " + LevelController.instance.GetCurrentLevel() + ". Spawning boss " + bossSpawnerPrefab.ToString());
            var bossSpawner = Instantiate(bossSpawnerPrefab, transform.position, Quaternion.identity);
            bossSpawner.transform.SetParent(gameObject.transform, false);
        }
    }

    IEnumerator SpawnAllWaves()
    {
        for (var waveIndex = 0; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        for (var i = 0; i < currentWave.GetAllWaypoints().Count; i++)
        {
            var enemy = Instantiate(currentWave.GetEnemyPrefab(), currentWave.GetAllWaypoints()[i].transform.position, Quaternion.identity);
            enemy.AddComponent<EnemyGroupPathing>();
            enemy.GetComponent<EnemyGroupPathing>().SetSpeedOfWave(currentWave.GetMovementSpeed());
        }

        yield return new WaitForSeconds(timeBetweenSpawns);
    }
}
