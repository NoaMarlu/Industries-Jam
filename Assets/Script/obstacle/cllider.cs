using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class cllider : MonoBehaviour
{

  public  GameObject player;

    public GameObject obstale;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerScript.instance != null)
        {
            player = PlayerScript.instance.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerScript.instance.OnDamage();
                
            
        }

    }
}
