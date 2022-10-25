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

            PlaceRemoveTower(towerPrefabs[selectedTower]);
        }
    }

    void PlaceRemoveTower(GameObject towerType)
    {
        StatController sc = GetComponent<StatController>();
        TowerController selectedTC = towerPrefabs[selectedTower].GetComponent<TowerController>();

        if (selectedObject.transform.childCount == 0 && (sc.money - selectedTC.placementCost) >= 0) //If tile has 0 children AND money - placement cost is more than 0;
        {
            GameObject newTower = Instantiate(towerType, selectedObject.transform); //Place tower of certain type
            TowerController tc = newTower.GetComponent<TowerController>(); //Get tower controller so the tower cost can be retrieved
            sc.AddTakeMoney(-tc.placementCost); //Remove the amount of money that the tower type costs
        }
        else if (selectedObject.transform.childCount >= 1) //If the tile has children
        {
            foreach (Transform child in selectedObject.transform) //For each child:
            {
                if (child.tag == "tower") //If it is a tower:
                {
                    TowerController tc = child.gameObject.GetComponent<TowerController>();
                    Destroy(child.gameObject); //Destroy the tower
                    sc.AddTakeMoney(tc.placementCost * tc.removeBackPercentage); //Give money back (placement cost * 0.%)
                }
            }
        }
    }
}
