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

    private void OnDrawGizmos()
    {
        if((int)transform.position.x % 2 == 0 && (int)transform.position.z % 2 != 0)
        {
            Gizmos.color = new Color(0, 0, 255, 1);
        }
        else if((int)transform.position.x % 2 != 0 && (int)transform.position.z % 2 == 0)
        {
            Gizmos.color = new Color(0, 0, 255, 1);
        }
        else if((int)transform.position.x % 2 == 0 && (int)transform.position.z % 2 == 0)
        {
            Gizmos.color = new Color(255, 255, 0, 1);
        }
        else if((int)transform.position.x % 2 != 0 && (int)transform.position.z % 2 != 0)
        {
            Gizmos.color = new Color(255, 255, 0, 1);
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
