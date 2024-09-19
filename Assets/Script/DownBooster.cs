using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DownBooster : ItemScript
{
    //ステータス
    [SerializeField] private float speed;
    [SerializeField] private float energy;

    private Vector3 offset = new Vector3(-0.3f, -1.3f, 1.0f);

    // Update is called once per frame
    void Update()
    {
        //プレイヤー取得時
        if (hit)
        {
            transform.position = new Vector3(PlayerScript.instance.transform.position.x - number + offset.x,
                                              PlayerScript.instance.transform.position.y + offset.y,
                                              PlayerScript.instance.transform.position.z + offset.z);
        }
        //取得していない場合
        else
        {
            //画面右から流れてくる
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
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
                PlayerScript.instance.GetDownBoosters().Add(this);
                number = PlayerScript.instance.GetDownBoosters().Count;
                GameSystem.Instance.AddSpeed(speed);
                hit = true;
            }
        }
        
    }

}
