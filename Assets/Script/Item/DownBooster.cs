using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DownBooster : ItemScript
{
    //audio
    public AudioClip collectedClip;
    protected AudioSource audioSource;

    //ステータス
    [SerializeField] private float speed;
    [SerializeField] private float energy;

    private Vector3 offset = new Vector3(0.5f, -0.5f, 0.1f);

    private void Start()
    {
        //オーディオ準備
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤー取得時
        if (hit)
        {
            transform.position = new Vector3(PlayerScript.instance.transform.position.x - number * offset.x,
                                              PlayerScript.instance.transform.position.y + offset.y,
                                              PlayerScript.instance.transform.position.z + offset.z);
            GameSystem.Instance.ItemGet = true;
        }
        //取得していない場合
        else
        {
            //画面右から流れてくる
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            GameSystem.Instance.ItemGet = false;
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
                //オーディオ再生
                audioSource.PlayOneShot(collectedClip);

                //プレイヤーのリストに登録
                PlayerScript.instance.GetDownBoosters().Add(this);
                number = PlayerScript.instance.GetDownBoosters().Count;
                GameSystem.Instance.AddSpeed(speed);
                hit = true;
                GameSystem.Instance.ItemGet = true;
            }
        }
        
    }

}
