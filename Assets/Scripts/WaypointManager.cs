using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [Header("Waypoint Lists (waypoints in order!)")]

    public List<GameObject> lv1 = new List<GameObject>(); //Inspector list
    public static List<GameObject> lv1_waypoints = new List<GameObject>(); //


    //[Header("Waypoint count variables")]
    public static int lv1_waypointCount = lv1_waypoints.Count;

    private void Awake()
    {
        lv1_waypoints = lv1; //make the inspector variable into the static variable so it is accessible by all other scripts
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in lv1_waypoints)
        {
            Debug.Log(go.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
