using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; } // シングルトンインスタンス

    public float BaseSpeed=5.0f;
    public float MaxBossDistance = 1000.0f;
    public float GamePosition = 0.0f;
    public float disSpeed=5.0f;//被弾でスピード下がる
    public Boss boss; // ボスの参照
    public bool isBossActive = false; // ボスがアクティブかどうか


    public bool isClear = false;
    //public float 
    void Awake()
    {
        // シングルトンの設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも削除されないようにする
        }
        else
        {
            Destroy(gameObject); // すでにインスタンスがある場合は削除
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public  void AddSpeed(float speed)
    {
        BaseSpeed += speed;
    }
    public void DisSpeed()
    {
        BaseSpeed -= disSpeed;
        if (BaseSpeed <= 0) BaseSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GamePosition += BaseSpeed*Time.deltaTime;

        
        // ボスを登場させる条件を満たしたかどうか
        if (!isBossActive && GamePosition >= MaxBossDistance)
        {
            ActivateBoss(); // ボスを登場させる
        }

    }

    // 他のスクリプトからボスの速度を取得するためのメソッド
    public float GetBossSpeed()
    {
        return BaseSpeed;
    }

    // ボスの速度を変更するメソッド（必要に応じて）
    public void SetBossSpeed(float newSpeed)
    {
        BaseSpeed = newSpeed;
    }

    public float GetMaxBossDistance()
    {
        return MaxBossDistance;
    }


    public void ActivateBoss()
    {
        if (!isBossActive&&boss != null)
        {
            boss.SpawnBoss(); // ボスの登場
            isBossActive = true; // ボスがアクティブ状態になる
        }
    }
}
