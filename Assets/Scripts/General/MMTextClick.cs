using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMTextClick : MonoBehaviour
{
    public bool play;
    public string nextLevelName;
    public bool exit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (play)
        {
            //Load level01
            Debug.Log("Loading level...");
            SceneManager.LoadScene(nextLevelName);
        }
        if (exit)
        {
            //Exit game
            Debug.Log("Quitting game...");
            Debug.Break();
            Application.Quit();
        }
    }
}
