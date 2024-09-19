using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BackBooster : ItemScript
{
    //ステータス
    [SerializeField] private float speed;
    [SerializeField] private float energy;

    private float offset = 1;

    // Update is called once per frame
    void Update()
    {
        //プレイヤー取得時
        if (hit)
        {
            transform.position = new Vector3(PlayerScript.instance.transform.position.x - number,
                                              PlayerScript.instance.transform.position.y,
                                              PlayerScript.instance.transform.position.z - offset);
        }
        //取得していない場合
        else
        {
            //画面右から流れてくる
            transform.Translate(moveDirection*moveSpeed*Time.deltaTime);
        }

        //画面内生存判定
        SurvivalCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hit)
        {
            //プレイヤーに当たると
            PlayerScript script = collision.GetComponent<PlayerScript>();

            if (script != null)
            {
                //プレイヤーのリストに登録
                PlayerScript.instance.GetBackBoosters().Add(this);
                number = PlayerScript.instance.GetBackBoosters().Count;
                hit = true;
            }
        }
        
    }

}
