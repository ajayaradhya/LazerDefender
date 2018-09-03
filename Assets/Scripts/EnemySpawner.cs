using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;

    private int startingWave = 0;

	// Use this for initialization
	void Start ()
    {
        var currentWave = waveConfigs[startingWave];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
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
