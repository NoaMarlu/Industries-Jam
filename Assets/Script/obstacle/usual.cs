using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class usual : MonoBehaviour
{

    public GameObject obstaclePrefab; // Prefabをインスペクターで指定
    private PlayerScript playerScript;
    public Sprite usualSprite; // 画像（Sprite）
    public float spawnInterval = 0.5f; // 一定時間ごとのスポーン間隔
    public float minSpawnInterval = 3.0f; // スポーン間隔の最小値
    public float maxSpawnInterval = 5.0f; // スポーン間隔の最大値
    public float spawnDuration = 5.0f; // 障害物を生成する期間
    public float emptyDuration = 3.0f; // 障害物を生成しない空白期間
    public float speed = 2.0f; // 移動速度
    public float speedIncreaseRate = 0.1f; // 時間経過ごとのスピード増加量
    private float currentSpawnInterval; // 現在のスポーン間隔
    private float timeElapsed = 0.0f; // 経過時間
    //スポーンする場所
  public  float spawnX=10.0f;
  public  float spawnZ=0.0f;
    //自然消滅
    [SerializeField]
    private float DestroyX=-20.0f;

    void Start()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        //Invoke("SpawnObject", currentSpawnInterval); // ランダムな間隔でスポーン開始
        StartCoroutine(SpawnCycle());
    }
    void Update()
    {
        // 時間経過に応じてスピードを増加
        timeElapsed += Time.deltaTime;
        speed += speedIncreaseRate * Time.deltaTime * (GameSystem.Instance.BaseSpeed*0.06f);
    }

    // 生成サイクル（生成期間と空白期間の繰り返し）
    IEnumerator SpawnCycle()
    {
        while (true)
        {
            // 生成期間
            yield return StartCoroutine(SpawnObstacles());

            // 空白期間
            yield return new WaitForSeconds(emptyDuration);
        }
    }

    // 生成期間中の障害物生成
    IEnumerator SpawnObstacles()
    {
        float elapsedTime = 0f;

        // 生成期間中は0.5秒ごとに障害物を生成
        while (elapsedTime < spawnDuration)
        {
            SpawnObject();

            // 0.5秒ごとに生成
            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
        }
    }


    void SpawnObject()
    {
        // プレハブをインスタンス化して生成
        GameObject newObject = Instantiate(obstaclePrefab);

        // 画面の右端から生成（Y座標はランダム）
        newObject.transform.position = new Vector3(spawnX, Random.Range(-4.0f, 4.0f), spawnZ);
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0; // 重力を無効化
            rb.bodyType = RigidbodyType2D.Kinematic; // 物理エンジンに影響されない
        }
        // 各オブジェクトごとにランダムな移動方向を設定
        Vector3 direction = GetRandomDirection();

        // ランダムな回転速度を設定
        float rotationSpeed = Random.Range(50.0f, 200.0f); // 回転速度

        // オブジェクトを移動させる
        StartCoroutine(MoveAndRotateObject(newObject, direction, rotationSpeed));

        // 次のスポーンまでの時間をランダムに設定
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // ランダムな方向を生成するメソッド
    Vector3 GetRandomDirection()
    {
        int curveDirection = Random.Range(0, 5); // カーブの方向（0=左下カーブ, 1=右上カーブ, 2=左への直進, 3=ジグザグ移動, 4=波形移動）
        if (curveDirection == 0)
        {
            return new Vector3(-1.0f, -0.5f, 0); // 左下カーブ
        }
        else if (curveDirection == 1)
        {
            return new Vector3(-1.0f, 0.5f, 0); // 右上カーブ
        }
        else if (curveDirection == 2)
        {
            return new Vector3(-1.0f, 0.0f, 0); // まっすぐ左
        }
        else if (curveDirection == 3)
        {
            return Vector3.zero; // ジグザグ移動は特別な処理を後で追加
        }
        else
        {
            return Vector3.zero; // 波形移動も特別な処理を後で追加
        }
    }

    // 移動と回転を同時に行うコルーチン
    IEnumerator MoveAndRotateObject(GameObject obj, Vector3 direction, float rotationSpeed)
    {
        float zigzagFrequency = 2.0f; // ジグザグ移動の周波数
        float waveAmplitude = 1.0f; // 波形移動の振幅
        float waveFrequency = 3.0f; // 波形移動の周波数

        while (obj != null)
        {
            if (direction != Vector3.zero)
            {
                // 通常の方向に移動
                obj.transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
            else
            {
                // ランダムなジグザグ移動
                if (Random.Range(0, 2) == 0)
                {
                    obj.transform.Translate(new Vector3(-1.0f, Mathf.Sin(Time.time * zigzagFrequency), 0) * speed * Time.deltaTime, Space.World);
                }
                // 波形移動
                else
                {
                    obj.transform.Translate(new Vector3(-1.0f, Mathf.Sin(Time.time * waveFrequency) * waveAmplitude, 0) * speed * Time.deltaTime, Space.World);
                }
            }

            // スプライトオブジェクトのみを回転
            // usualSprite.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            obj.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
            // 画面外に出たら削除
            if (obj.transform.position.x < DestroyX)
            {
                Destroy(obj);
            }
            yield return null;
        }
    }

    IEnumerator MoveObject(GameObject obj, GameObject spriteObj, Vector3 direction, float rotationSpeed)
    {
        while (obj != null)
        {
            obj.transform.Translate(direction * speed * Time.deltaTime);
            // スプライトオブジェクトのみを回転
            spriteObj.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            // 画面外に出たら削除
            if (obj.transform.position.x < DestroyX)
            {
                Destroy(obj);
            }
            yield return null;
        }
    }

   


}
