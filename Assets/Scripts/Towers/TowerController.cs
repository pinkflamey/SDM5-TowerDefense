using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Triggers")]
    public bool shoot = false;

    [Header("Information")]
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject turret;
    [SerializeField] private List<GameObject> enemiesInRange;

    [Space]

    [Header("Settings")]
    public GameObject projectile;
    [Range(1.0f, 20.0f)] public float damage = 1f;
    [Range(1.0f, 20.0f)] public float health = 1f;
    [Range(1.0f, 20.0f)] public float range = 5f;
    [Range(1.0f, 20.0f)] public float projSpeed = 1f;
    [Range(1.0f, 15.0f)] public float TargetRadius;
    [Range(20.0f, 75.0f)] public float LaunchAngle;
    [Range(0.0f, 10.0f)] public float TargetHeightOffsetFromGround;
    [Range(0.0f, 5.0f)] public float spawnHeightOffset;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform c in transform)
        {
            if(c.name == "Turret")
            {
                turret = c.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //enemiesInRange = Physics.OverlapSphere(transform.position, range);

        if (shoot)
        {
            shoot = false;
            ShootEnemy(target);
        }
    }

    void ShootEnemy(GameObject t)
    {
        Debug.Log("Fire!");

        Vector3 spawnPos = new Vector3(turret.transform.position.x, turret.transform.position.y + spawnHeightOffset, turret.transform.position.z);
        Projectile proj = Instantiate(projectile, spawnPos, transform.rotation).GetComponent<Projectile>(); //Create new projectile

        proj.TargetObjectTF = t.transform; //Set target

        //Projectile settings
        proj.TargetRadius = TargetRadius;
        proj.LaunchAngle = LaunchAngle;
        proj.TargetHeightOffsetFromGround = TargetHeightOffsetFromGround;

        proj.Launch(); //Launch projectile

        

    }
}
