using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [Header("Waypoint Lists (waypoints in order!)")]

    public List<GameObject> lv1 = new List<GameObject>(); //Inspector list
    //public static List<GameObject> lv1_waypoints = new List<GameObject>(); //


    [Header("Waypoint count variables")]
    public int lv1_waypointCount;

    private void Awake()
    {
        lv1_waypointCount = lv1.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*foreach(GameObject go in lv1_waypoints)
        {
            Debug.Log(go.name);

        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
