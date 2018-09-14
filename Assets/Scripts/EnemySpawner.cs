using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;
    [SerializeField] GameObject bossSpawnerPrefab;

    private int startingWave = 0;
    private bool doneWithTheLevel = false;

    // Use this for initialization
    IEnumerator Start ()
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
        if (doneWithTheLevel)
        {
            LoadNextLevelIfAllEnemiesAreDead();
        }
    }

    private void LoadNextLevelIfAllEnemiesAreDead()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            Debug.Log("All Enemies are dead in " + LevelController.instance.GetCurrentLevel() + ". Spawning boss " + bossSpawnerPrefab.ToString());
            Instantiate(bossSpawnerPrefab, transform.position, Quaternion.identity);
        }
    }

    IEnumerator SpawnAllWaves()
    {
        for(var waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        for(var enemyCount = 0; enemyCount < currentWave.GetNumberOfEnemies(); enemyCount++)
        {
            var enemy = Instantiate(currentWave.GetEnemyPrefab(), currentWave.GetAllWaypoints()[0].transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyPathing>().SetWaveConfig(currentWave);
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawns());
        }
        
    }
}
