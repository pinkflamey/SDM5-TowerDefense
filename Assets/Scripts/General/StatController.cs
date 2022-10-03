using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatController : MonoBehaviour
{
    [Header("Enemy counter")]
    [InspectorName("Object holding counter TMP")] public GameObject counterObject;
    public int currentMax;
    private TextMeshProUGUI counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = counterObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        counter.text = GetCurrentEnemies() + " / " + GetMaxEnemies();
    }

    int GetMaxEnemies()
    {
        WaveController wc = gameObject.GetComponent<WaveController>();
        Wave currentWave = null;
        try
        {
            currentWave = wc.waves[wc.waveCount];
        }
        catch { }

        int maxEnemies = 0;

        if(currentWave != null)
        {
            foreach (int enemyTypeCount in currentWave.count)
            {
                try
                {
                    maxEnemies += enemyTypeCount;
                }
                catch
                {
                    break;
                }
            }
        }
        

        return maxEnemies;
    }

    int GetCurrentEnemies()
    {
        return GameObject.FindGameObjectsWithTag("enemy").Length;
    }
}
