using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatController : MonoBehaviour
{
    [Header("Money system")]
    //Money shit
    public float money = 0f;
    [InspectorName("Object holding counter TMP")] public GameObject moneyCounterObj;
    private TextMeshProUGUI moneyCounter;

    [Header("Money debugging/dev-tools")]
    public bool addMoney = false;
    [Range(-100, 100)] public int addMoneyAmount = 100;

    [Space]

    [Header("Enemy counter")]
    [InspectorName("Object holding counter TMP")] public GameObject counterObject;
    public int currentMax;
    private TextMeshProUGUI counter;

    // Start is called before the first frame update
    void Start()
    {
        //-----Enemy counter-----
        counter = counterObject.GetComponent<TextMeshProUGUI>();

        //-----Money-----
        moneyCounter = moneyCounterObj.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //-----Enemy counter-----
        counter.text = GetCurrentEnemies() + " / " + GetMaxEnemies(); //Set counter text

        //-----Money-----
        if (addMoney) //Add money (for debugging purposes)
        {
            addMoney = false;
            AddTakeMoney(addMoneyAmount);
        }

        moneyCounter.text = "$" + money;
    }

    public void AddTakeMoney(float amount) //Use -float to remove money
    {
        money += amount;
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
