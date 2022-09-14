using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGen : MonoBehaviour
{
    public GameObject[] itemsToPickFrom;
    public int gridX;
    public int gridZ;
    public float gridspacingOffset = 1f;
    public Vector3 gridorigin = Vector3.zero;

    // Start is called before the first frame update void Start()
    private void Start()
    {
        SpawnGrid();
    }
    void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for(int z = 0; z < gridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridspacingOffset - (gridX / 2), 0, z * gridspacingOffset - (gridZ / 2)) + gridorigin;
                PickAndSpawn(spawnPosition, Quaternion.identity);
            }
        }
    }

    void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, itemsToPickFrom.Length);
        GameObject clone = Instantiate(itemsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn);
        clone.transform.parent = transform;
    }

}
