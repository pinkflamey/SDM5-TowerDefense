using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector_TowerPlacer : MonoBehaviour
{
    [Header("Settings")]
    public List<GameObject> towerPrefabs;
    [Range(0, 2)] public int selectedTower = 0;

    [Header("Info")]
    public GameObject selectedObject;
    Ray ray;
    RaycastHit hitData;
    public Vector3 hitDataPos;
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitData, 1000))
        {
            hitDataPos = hitData.transform.position;

            selectedObject = hitData.transform.gameObject;
            
            transform.position = hitData.transform.position;
        }

        if (Input.GetMouseButtonDown(0) && selectedObject.tag == "gridCube")
        {
            PlaceTower(towerPrefabs[selectedTower]);
        }
    }

    void PlaceTower(GameObject towerType)
    {
        //CubeController cc = selectedObject.gameObject.GetComponent<CubeController>();
        StatController sc = GetComponent<StatController>();
        TowerController selectedTC = towerPrefabs[selectedTower].GetComponent<TowerController>();

        if (selectedObject.transform.childCount == 0 && (sc.money - selectedTC.placementCost) >= 0) //If tile has 0 children AND money - placement cost is more than 0;
        {
            GameObject newTower = Instantiate(towerType, selectedObject.transform); //Place tower of certain type
            TowerController tc = newTower.GetComponent<TowerController>(); //Get tower controller so the tower cost can be retrieved
            sc.AddTakeMoney(-tc.placementCost); //Remove the amount of money that the tower type costs
        }
        else
        {
            Debug.Log("Can't place tower, you don't have enough money, or there is already a tower there!");
        }
    }
}
