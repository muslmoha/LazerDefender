using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;//serialize all waves
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    //Spawn all waves
    private IEnumerator SpawnAllWaves()
    {
        for(int i = 0; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    //Spawn all enemies in wave
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConf)
    {
        for (int i = 0; i < waveConf.GetNumOfEnemies(); i++)
        {
            var newEnemy = Instantiate(waveConf.GetEnemyPrefab(), waveConf.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<Waypoints>().SetWaveConfig(waveConf);//by instantiating as "newEnemy" var we can grab components
            yield return new WaitForSeconds(waveConf.GetSpawnDelay());
        }
    }
}
