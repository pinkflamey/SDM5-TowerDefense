using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public Wave[] waves;
    public int waveCount = 0;
    public GameObject enemySpawnObj;

    // Start is called before the first frame update
    void Start()
    {
        StartNextWave(waves[waveCount]);
    }

    IEnumerator Wave(Wave wave)
    {
        Debug.Log("Wave: " + waveCount);
        //Debug.Break();

        for(int i = 0; i < wave.enemies.Length; i++) //For each enemy type;
        {
            Debug.Log("Enemy type: " + wave.enemies[i].name);
            //Debug.Break();

            for(int j = 1; j <= wave.count[i]; j++) //Get the amount of enemies of that type to spawn and spawn them
            {
                Debug.Log("Spawning enemy " + j + " out of " + wave.count[i]);

                Instantiate(wave.enemies[i], enemySpawnObj.transform.position, enemySpawnObj.transform.rotation); //Spawn enemy
                yield return new WaitForSeconds(wave.enemyDelay); //Wait out delay till next enemy
            }
        }

        yield return new WaitForSeconds(wave.timeTillNextWave); //Wait out delay till next wave

        waveCount++; //Set the number for new current wave

        if(waveCount == waves.Length) //Check if the last wave was the last wave of the level
        {
            Debug.Log("Level complete!");
            yield return null;
        }
        else
        {
            StartNextWave(waves[waveCount]); //If not, start the next wave
        }
        
        
        
    }

    void StartNextWave(Wave wave)
    {
        StartCoroutine(Wave(wave));
    }
}
