using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("General")]
    public float currentHealth;
    public bool damage_10 = false;

    [Header("Path/movement (which waypoints to follow) & toggles")]
    public bool moving;
    public int waypointList;
    private List<GameObject> waypoints = new List<GameObject>(); //Actual waypoints to follow

    [Header("Movement info")]
    [SerializeField] private GameObject target;
    [SerializeField] private int targetNumber;

    [Space]

    [Header("Settings")]
    public float maxHealth;
    public float movementSpeed;
    public float damage;
    public float rotateSpeed;

    [Header("Animation")]
    [SerializeField] private EnemyAnimationController animationController;




    // Start is called before the first frame update
    void Start()
    {
        //Set health to maxhealth
        currentHealth = maxHealth;

        //Waypoints initialization
        switch (waypointList) //Select which waypoints from the manager to take at script awakening
        {
            case 1:
                waypoints = WaypointManager.lv1_waypoints;
                break;

            default:
                waypoints = WaypointManager.lv1_waypoints;
                break;
        }

        //AnimationController variable initialization
        animationController = gameObject.GetComponent<EnemyAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        target = waypoints[targetNumber];

        if (moving)
        {
            RotateToPos(target.transform.position); //Rotate to target waypoint
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime); //Move to target waypoint
            animationController.walking = true; //Set walking animation
        }
        else
        {
            animationController.walking = false;
        }

        if (transform.position == new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z))
        {

            if (targetNumber + 1 == waypoints.Count)
            {
                moving = false;
                target = null;

            }
            else
            {
                targetNumber++;
                Debug.Log("New target: " + targetNumber + ", name: " + waypoints[targetNumber]);
            }
        }

        if (damage_10)
        {
            damage_10 = false;
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            //Die
            StartCoroutine(Die());
        }
    }
    IEnumerator Die()
    {
        animationController.die = true;

        yield return new WaitForSeconds(1.35f);

        Destroy(gameObject);

        yield return null;
    }

    void RotateToPos(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        float step = rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, step);

        /*Vector3 objectPos = transform.position;
        Vector3 targ = new Vector3(targetPos.x, objectPos.y, targetPos.z);

        targ.x = targ.x - objectPos.x;
        targ.z = targ.z - objectPos.z;
        float angle = Mathf.Atan2(targ.x, targ.z) * Mathf.Rad2Deg;

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));*/

    }
}
