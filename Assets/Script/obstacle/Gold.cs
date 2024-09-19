using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public GameObject obstaclePrefab; // Prefabをインスペクターで指定
    public Sprite usualSprite; // 画像（Sprite）
    public float spawnInterval = 3.0f; // 一定時間ごとのスポーン間隔
    public float minSpawnInterval = 3.0f; // スポーン間隔の最小値
    public float maxSpawnInterval = 5.0f; // スポーン間隔の最大値
    public float speed = 2.0f; // 移動速度
    public float speedIncreaseRate = 0.1f; // 時間経過ごとのスピード増加量
    private float currentSpawnInterval; // 現在のスポーン間隔
    private float timeElapsed = 0.0f; // 経過時間
     //スポーンする場所
    public float spawnX = 10.0f;
    //自然消滅
    [SerializeField]
    private float DestroyX = -20.0f;
    void Start()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnObject", currentSpawnInterval); // ランダムな間隔でスポーン開始
    }
    void Update()
    {
        // 時間経過に応じてスピードを増加
        timeElapsed += Time.deltaTime;
        speed += speedIncreaseRate * Time.deltaTime;
    }
    void SpawnObject()
    {
        // obstaclePrefabから新しいオブジェクトを生成
        GameObject newObject = Instantiate(obstaclePrefab);

        // 生成位置を設定（画面の右端から生成、Y座標はランダム）
        newObject.transform.position = new Vector3(spawnX, Random.Range(-4.0f, 4.0f), 0);

        // Rigidbody2D と Collider2D が Prefab に含まれていることを確認する
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = newObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }

        BoxCollider2D collider = newObject.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = newObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
        }

        // ランダムな移動方向を設定
        Vector3 direction = GetRandomDirection();

        // ランダムな回転速度を設定
        float rotationSpeed = Random.Range(50.0f, 200.0f); // 回転速度

        // オブジェクトを移動させるコルーチンを開始
        StartCoroutine(MoveObject(newObject, direction, rotationSpeed));

        // 次のスポーンまでの時間をランダムに設定
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnObject", currentSpawnInterval); // 次のオブジェクトをランダムな間隔でスポーン
    }

    // ランダムな方向を生成するメソッド
    Vector3 GetRandomDirection()
    {
        int curveDirection = Random.Range(0, 3); // カーブの方向（0=左下カーブ, 1=右上カーブ, 2=左への直進）
        if (curveDirection == 0)
        {
            return new Vector3(-1.0f, -0.5f, 0); // 左下カーブ
        }
        else if (curveDirection == 1)
        {
            return new Vector3(-1.0f, 0.5f, 0); // 右上カーブ
        }
        else
        {
            return new Vector3(-1.0f, 0.0f, 0); // まっすぐ左
        }
    }

    IEnumerator MoveObject(GameObject obj, Vector3 direction, float rotationSpeed)
    {
        while (obj != null)
        {
            obj.transform.Translate(direction * speed * Time.deltaTime, Space.World);
            // スプライトオブジェクトのみを回転
            obj.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);

            // 画面外に出たら削除
            if (obj.transform.position.x < DestroyX)
            {
                Destroy(obj);
            }
            yield return null;
        }
    }

   
}
