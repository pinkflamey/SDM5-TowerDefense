using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemies;
    public int[] count;
    public float enemyDelay;
    public float timeTillNextWave = 5.0f;
}
