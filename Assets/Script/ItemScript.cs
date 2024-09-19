using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    protected PlayerScript script;
    protected int number = 0;
    protected bool hit = false;
    protected Vector3 ConnectPosition;
    protected Vector3 NextPosition;


    private void Update()
    {
        if (hit)
        {
          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hit)
        {
            script = collision.GetComponent<PlayerScript>();

            if (script != null)
            {
                hit = true;
               
            }
        }
    }
}
