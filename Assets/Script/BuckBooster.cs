using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BuckBooster : ItemScript
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            transform.position = new Vector3(PlayerScript.instance.transform.position.x - number,
                                              PlayerScript.instance.transform.position.y,
                                              PlayerScript.instance.transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hit)
        {
            script = collision.GetComponent<PlayerScript>();

            if (script != null)
            {
                PlayerScript.instance.buckBoosters.Add(this);
                number = PlayerScript.instance.buckBoosters.Count;
                hit = true;
            }
        }
        else
        {
            BuckBooster script = collision.GetComponent<BuckBooster>();

            if (script != null&&script.number+1 <=  0)
            {
                PlayerScript.instance.buckBoosters.Add(script);
                number = PlayerScript.instance.buckBoosters.Count;
                script.hit = true;
            }

        }
    }

}
