using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
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

    

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        target = waypoints[targetNumber];

        if (moving)
        {
            //RotateToPos(target.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);

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
    }

    void RotateToPos(Vector3 targetPos)
    {
        Vector3 objectPos = transform.position;
        Vector3 targ = new Vector3(targetPos.x, objectPos.y, targetPos.z);

        targ.x = targ.x - objectPos.x;
        targ.z = targ.z - objectPos.z;
        float angle = Mathf.Atan2(targ.x, targ.z) * Mathf.Rad2Deg;

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));

    }
}
