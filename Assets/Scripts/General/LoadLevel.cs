using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        if (nextLevel != null)
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}