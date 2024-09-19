using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Item
{
    Back,
    Up,
    Down,

    None,
}


public class ItemSpawn : MonoBehaviour
{
    //アイテムのプレハブ
    public GameObject BackBoosterPrefab;
    public GameObject UpBoosterPrefab;
    public GameObject DownBoosterPrefab;

    //スポーンするまでの時間
    [SerializeField] private float spawnInterval;
    [SerializeField] private float moveSpeed;//アイテムが移動する速度

    [SerializeField] private float spawnDistance;//プレイヤーどれだけ離れた位置から出現するか
    [SerializeField] private float spawnRange;   //アイテムの横方向のランダム 

    [SerializeField] private int maxspawn;   //アイテムの最大スポーン数
    [SerializeField] private int minspawn;   //アイテムの最小スポーン数 
    private float spawntime = 0;
    private Quaternion spawnRotation = Quaternion.Euler(0, 0, 90);
    void Update()
    {
        spawntime += Time.deltaTime;

        if(spawntime > spawnInterval)
        {
            //アイテム出現
            SpawnItem();
            spawntime = 0.0f;
        }

    }

    //アイテムのランダム出現用
    void SpawnItem()
    {
        int randomcount = Random.Range(minspawn, maxspawn);
        for (int i = 0; i < randomcount; i++)
        {
            float randomY = Random.Range(-4.5f, 4.5f);
            Vector3 SpawnPosition = new Vector3(
                                                spawnDistance + Random.Range(-spawnRange, spawnRange),
                                                randomY,
                                                PlayerScript.instance.transform.position.z);

            int spawntype = Random.Range((int)Item.Back, (int)Item.Down);


            switch (spawntype)
            {
                case (int)Item.Back:
                    GameObject newBackBooster = Instantiate(BackBoosterPrefab, SpawnPosition, spawnRotation);
                    BackBooster backBoosterScript = newBackBooster.GetComponent<BackBooster>();
                    backBoosterScript.SetMoveSpeed(moveSpeed);
                    break;
                case (int)Item.Up:
                    GameObject newUpBooster = Instantiate(UpBoosterPrefab, SpawnPosition, spawnRotation);
                    UpBooster upBoosterScript = newUpBooster.GetComponent<UpBooster>();
                    upBoosterScript.SetMoveSpeed(moveSpeed);
                    break;
                case (int)Item.Down:
                    GameObject newDownBooster = Instantiate(DownBoosterPrefab, SpawnPosition, spawnRotation);
                    DownBooster downBoosterScript = newDownBooster.GetComponent<DownBooster>();
                    downBoosterScript.SetMoveSpeed(moveSpeed);
                    break;
            }
        }
    }

}
