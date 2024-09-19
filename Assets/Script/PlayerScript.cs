using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    static public PlayerScript instance;

    public Vector3 ConnectPosition = Vector3.zero;

    public List<BuckBooster> buckBoosters = new List<BuckBooster>();
    static public int BuckBoosterCount;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        BuckBooster script = collision.GetComponent<BuckBooster>();
        if(script != null)
        {
            if(buckBoosters.Count <= 0)
            { 
                buckBoosters.Add(script);
            }
        }
    }
}
