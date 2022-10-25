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

    [Space]

    [Header("Win/lose system")]
    [Range(0.1f, 10.0f)] [Tooltip("Delay for text to appear after winning. Delay to dissapear is delay + 3f")] public float textDelay = 3.0f;
    private GameObject wintext;
    private GameObject losetext;

    private EndpointController epc;
    private WaveController wc;

    [Space]

    [Header("Enemy counter")]
    [InspectorName("Object holding counter TMP")] public GameObject counterObject;
    public int currentMax;
    private TextMeshProUGUI counter;

    [Space]

    [Header("Money debugging/dev-tools")]
    public bool addMoney = false;
    [Range(-100, 100)] public int addMoneyAmount = 100;

    // Start is called before the first frame update
    void Start()
    {
        //-----Enemy counter-----
        counter = counterObject.GetComponent<TextMeshProUGUI>();

        //-----Money-----
        moneyCounter = moneyCounterObj.GetComponent<TextMeshProUGUI>();

        //-----Win/lose-----
        epc = GameObject.FindGameObjectWithTag("endpoint").GetComponent<EndpointController>();
        wc = GetComponent<WaveController>();
        wintext = GameObject.FindGameObjectWithTag("wintext");
        losetext = GameObject.FindGameObjectWithTag("losetext");
        wintext.SetActive(false);
        losetext.SetActive(false);
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

        //-----Win/lose-----
        CheckForWinLose();
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

    void CheckForWinLose()
    {
        if(wc.wavesCompleted && GetCurrentEnemies() == 0)
        {
            //Win
            StartCoroutine(Win());
        }
        else if (epc.lose)
        {
            //Lose
            StartCoroutine(Lose());
        }
    }

    IEnumerator Win()
    {
        //Show win text
        yield return new WaitForSeconds(textDelay);
        wintext.SetActive(true);
        yield return new WaitForSeconds(textDelay + 3f);
        wintext.SetActive(false);


        //Load next level
        GetComponent<LoadLevel>().LoadNextLevel();

        yield return null;
    }
    IEnumerator Lose()
    {
        //Show lose text
        yield return new WaitForSeconds(textDelay);
        losetext.SetActive(true);
        yield return new WaitForSeconds(textDelay + 3f);
        losetext.SetActive(false);

        //Load next level
        GetComponent<LoadLevel>().LoadMainMenu();

        yield return null;
    }
}
