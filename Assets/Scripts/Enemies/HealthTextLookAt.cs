using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthTextLookAt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshPro>().outlineWidth = 0.2f;
        GetComponent<TextMeshPro>().outlineColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(-Camera.main.transform.position);
    }
}
