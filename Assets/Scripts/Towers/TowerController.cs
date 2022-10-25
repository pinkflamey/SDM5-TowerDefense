using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Triggers")]
    public bool shoot = true;

    [Header("Information")]
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject turret;
    [SerializeField] private List<GameObject> enemiesInRange;

    [Space]

    [Header("Settings")]
    [Tooltip("The amount of points it costs to place the tower")] public float placementCost = 1f;
    [Tooltip("The amount of points player gets back when removing a tower, in percentage of placement cost")] [Range(0.0f, 1f)] public float removeBackPercentage = 0f;
    [Tooltip("0: Mortar | 1: Magic Ray")] [Range(0, 2)] public int projType = 0;
    [Tooltip("Not for RayTrace tower types")] public GameObject projectile;
    [Range(1.0f, 20.0f)] public float damage = 1f;
    [Range(1.0f, 20.0f)] public float health = 1f;
    [Range(1.0f, 20.0f)] public float range = 5f;
    [Range(0.1f, 20.0f)] public float delay = 1.0f;
    [Range(1.0f, 20.0f)] public float projSpeed = 1f;

    [Header("Mortar tower settings")]

    [Range(1.0f, 15.0f)] public float TargetRadius;
    [Range(20.0f, 75.0f)] public float LaunchAngle;
    [Range(0.0f, 10.0f)] public float TargetHeightOffsetFromGround;
    [Range(0.0f, 5.0f)] public float spawnHeightOffset;

    [Header("Gizmos")]

    [Tooltip("0: Sphere mesh | 1: Sphere")] [Range(0, 1)] public int gizmoType = 0;
    [Range(0f, 100f)] public float colorTransparency = 100f;

    // Start is called before the first frame update
    void Start()
    {
        /*foreach(Transform c in transform)
        {
            if(c.name == "Turret")
            {
                turret = c.gameObject;
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemies();
        FindClosestEnemy();

        if (shoot)
        {
            shoot = false;
            StartCoroutine(Shoot());
        }
        
    }

    IEnumerator Shoot()
    {
        if ((target != null)) //If shoot is triggered and there is a target
        {

            switch (projType) //Different shooting types select
            {
                case 0:
                    ShootEnemyMortar(target);
                    break;
                case 1:
                    ShootEnemyArrow(target);
                    break;
                case 2:
                    ShootEnemyLaser(target);
                    break;
                default:
                    ShootEnemyLaser(target);
                    break;
            }
        }
        /*else //If shoot is triggered but the target is null,
        {
            shoot = false; //Set shoot back to false
        }*/

        yield return new WaitForSeconds(delay);

        shoot = true;

        yield return null;
    }

    void FindClosestEnemy()
    {
        GameObject closest = null; //Set up variable for keeping track of closest enemy
        foreach(GameObject o in enemiesInRange) //For each enemy in range
        {
            if(closest != null) //If the closest is not nothing
            {
                /*if (Vector3.Distance(o.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position) && o != null && closest != null) // If the distance between enemy-tower is less than closest-tower
                {
                    closest = o; //The new closest is the enemy
                }*/
                try
                {
                    if (Vector3.Distance(o.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position)) // If the distance between enemy-tower is less than closest-tower
                    {
                        closest = o; //The new closest is the enemy
                    }
                }
                catch
                {

                }
            }
            else
            {
                closest = o;
            }
            
        }
        target = closest; //The target is the closest enemy
    }

    void FindEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy"); //Find all enemies in the game
        for (int i = 0; i < enemies.Length; i++) //For each enemy
        {
            if(enemies[i] != null) //Check if enemy does still exist
            {
                if ((transform.position - enemies[i].transform.position).magnitude < range) //Check if enemy is in range
                {
                    if (!enemiesInRange.Contains(enemies[i])) //if the in-range list doesnt have the enemy,
                    {
                        enemiesInRange.Add(enemies[i]); //add the enemy to the in-range list
                    }
                }
                else
                {
                    enemiesInRange.Remove(enemies[i]); //If enemy is not in range, remove it from the in-range list
                }
            }
        }
    }

    //Currently unused
    void ShootEnemyMortar(GameObject t)
    {
        Debug.Log("Fire mortar!");

        Vector3 spawnPos = new Vector3(turret.transform.position.x, turret.transform.position.y + spawnHeightOffset, turret.transform.position.z);
        MortarProjectile proj = Instantiate(projectile, spawnPos, transform.rotation).GetComponent<MortarProjectile>(); //Create new mortar projectile

        proj.TargetObjectTF = t.transform; //Set target

        //Projectile settings
        proj.TargetRadius = TargetRadius;
        proj.LaunchAngle = LaunchAngle;
        proj.TargetHeightOffsetFromGround = TargetHeightOffsetFromGround;

        proj.Launch(); //Launch projectile

        

    }

    //Currently unused
    void ShootEnemyArrow(GameObject t)
    {
        Debug.Log("Fire arrow!");

        Vector3 spawnPos = new Vector3(turret.transform.position.x, turret.transform.position.y + spawnHeightOffset, turret.transform.position.z);
        ArrowProjectile proj = Instantiate(projectile, spawnPos, transform.rotation).GetComponent<ArrowProjectile>(); //Create new arrow projectile




    }

    void ShootEnemyLaser(GameObject t)
    {
        //Debug.Log("Fire laser");

        Vector3 raypos = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        Vector3 targetpos = new Vector3(t.transform.position.x, t.transform.position.y + 0.25f, t.transform.position.z);
        Vector3 dir = -(raypos - targetpos);

        Ray ray = new Ray(raypos, dir);
        RaycastHit hitData;

        Physics.Raycast(ray, out hitData);
        Debug.DrawRay(raypos, dir * 10, Color.blue, 3f);
        Vector3[] linePositions = {raypos, targetpos};
        StartCoroutine(CreateLaserLines(linePositions));

        GameObject hitObj = null;
        try
        {
            hitObj = hitData.transform.gameObject;
        }
        catch
        {
            
        }
        //Debug.Log("I hit the object " + hitObj.name + "!");

        if(hitObj != null)
        {
            if (hitObj.tag == "enemy")
            {

                try
                {
                    EnemyController enemy = hitObj.GetComponent<EnemyController>();
                    if (enemy.currentHealth > 0)
                    {
                        enemy.TakeDamage(damage);
                    }
                }
                catch
                {
                    return;
                }
            }
        }
        
    }
    IEnumerator CreateLaserLines(Vector3[] positions)
    {
        gameObject.GetComponent<LineRenderer>().SetPositions(positions);

        yield return new WaitForSeconds(delay - (delay / 2));

        Vector3[] _positions = {transform.position, transform.position};
        gameObject.GetComponent<LineRenderer>().SetPositions(_positions);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 1, (colorTransparency / 100));
        Vector3 gizmopos = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);

        switch (gizmoType)
        {
            case 0:
                Gizmos.DrawWireSphere(gizmopos, range);
                break;
            case 1:
                Gizmos.DrawSphere(gizmopos, range);
                break;
            default:
                Gizmos.DrawWireSphere(gizmopos, range);
                break;
        }
    }
}
