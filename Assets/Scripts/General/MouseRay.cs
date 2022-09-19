using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRay : MonoBehaviour
{
    [Header("Settings")]
    public List<GameObject> towerPrefabs;
    [Range(0, 1)] public int selectedTower = 0;

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
            CubeController cc = selectedObject.gameObject.GetComponent<CubeController>();

            cc.PlaceTower(towerPrefabs[selectedTower]);
        }
    }
}
