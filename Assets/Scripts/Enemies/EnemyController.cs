using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //TODO: Split normal enemy controls and animations into seperate scripts

    [Header("Path/movement (which waypoints to follow)")]
    public int waypointList;
    private List<GameObject> waypoints = new List<GameObject>(); //Actual waypoints to follow

    [Space]

    [Header("Settings")]
    public int maxHealth;
    public int movementSpeed;
    public int damage;

    [Space]

    [Header("Information/toggles")]
    [Tooltip("Boolean")] public bool walking = false;
    [Tooltip("Boolean")] public bool running = false;
    [Tooltip("Trigger")] public bool attacking01 = false;
    [Tooltip("Trigger")] public bool attacking02 = false;
    [Tooltip("Trigger")] public bool takingDamage = false;
    [Tooltip("Trigger")] public bool die = false;


    private void Awake()
    {
        switch (waypointList) //Select which waypoints from the manager to take at script awakening
        {
            case 1:
                waypoints = WaypointManager.lv1_waypoints;
                break;

            default:
                waypoints = WaypointManager.lv1_waypoints;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Animator anim = GetComponent<Animator>();

        if (walking)
        {
            running = false;
            attacking01 = false;
            attacking02 = false;
            takingDamage = false;
            die = false;

            anim.SetBool("Walk Forward", true);
        }
        else
        {
            anim.SetBool("Walk Forward", false);
        }

        if (running)
        {
            walking = false;
            attacking01 = false;
            attacking02 = false;
            takingDamage = false;
            die = false;

            anim.SetBool("Run Forward", true);
        }
        else
        {
            anim.SetBool("Run Forward", false);
        }

        if (attacking01)
        {
            walking = false;
            running = false;
            attacking02 = false;
            takingDamage = false;
            die = false;

            anim.SetTrigger("Attack 01");
            attacking01 = false;
        }

        if (attacking02)
        {
            walking = false;
            running = false;
            attacking01 = false;
            takingDamage = false;
            die = false;

            anim.SetTrigger("Attack 02");
            attacking02 = false;
        }

        if (takingDamage)
        {
            walking = false;
            running = false;
            attacking02 = false;
            attacking01 = false;
            die = false;

            anim.SetTrigger("Take Damage");
            takingDamage = false;
        }

        if (die)
        {
            walking = false;
            running = false;
            attacking02 = false;
            attacking01 = false;
            takingDamage = false;

            anim.SetTrigger("Die");
            die = false;
        }
    }

    /*private IEnumerator changeRunning(bool changeTo)
    {
        yield return new WaitForSeconds(0.5f);
        running = changeTo;
        yield return null;
    }
    private IEnumerator changeWalking(bool changeTo)
    {
        yield return new WaitForSeconds(0.5f);
        walking = changeTo;
        yield return null;
    }*/
}
