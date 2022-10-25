using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointController : MonoBehaviour
{
    [Header("Settings")]
    [Range(1f, 100f)] public float health;
    public bool lose = false;

    //Private variables
    private float startHealth;

    // Start is called before the first frame update
    void Start()
    {
        if(health == 0f)
        {
            health = 50f;
        }

        //Set up certain private variables
        startHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0f)
        {
            //Game over
            Debug.Log("Game over! You lost lmao");
            lose = true;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
