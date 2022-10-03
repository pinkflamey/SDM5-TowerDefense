using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [Header("Settings")]
    public float selectHeight = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTower(GameObject towerType)
    {
        Instantiate(towerType, transform); //Places tower of certain type
    }

    private void OnMouseEnter()
    {
        //Debug.Log("A mouse has entered me!");

        if (tag == "gridCube")
        {

            Vector3 pos = transform.position;

            transform.position = new Vector3(pos.x, pos.y + selectHeight, pos.z);
        }


        /*Animator animator = GetComponent<Animator>();
        animator.SetBool("selected", true);*/
    }

    private void OnMouseExit()
    {
        if(tag == "gridCube")
        {

            Vector3 pos = transform.position;

            transform.position = new Vector3(pos.x, pos.y - selectHeight, pos.z);
        }

        /*Animator animator = GetComponent<Animator>();
        animator.SetBool("selected", false);*/
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "mousePointer")
        {
            Debug.Log("Colliding with " + collision.gameObject.name);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "mousePointer")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
    }*/
}
