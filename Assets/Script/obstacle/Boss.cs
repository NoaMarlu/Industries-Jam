using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Sprite bossSprite; // ボスの画像（スプライト）
    public float initialSpeed = 5.0f; // 最初の左移動のスピード
    public float ellipseSpeed = 2.0f; // 楕円軌道のスピード
    public float ellipseWidth = 2.0f; // 楕円軌道の横幅
    public float ellipseHeight = 4.0f; // 楕円軌道の縦幅
    public float leftMoveDistance = 3.0f; // 左に移動する距離

    private bool isMovingLeft = true; // 左移動中かどうか
    private Vector3 startPosition; // 楕円軌道の開始位置
    private float timeElapsed = 0.0f; // 経過時間
    private SpriteRenderer spriteRenderer; // SpriteRenderer
    private bool isBossActivated; // ボスが有効化されているかどうか
    //スポーンする場所
    public float spawnX = 10.0f;

    void Start()
    {
        // ボスを画面の右外に配置
        //なにかif文追加して条件が出てこない限りbossは出てこないようにする
        transform.position = new Vector3(spawnX, 0.0f, 0.0f);

        // SpriteRendererを追加してスプライトを設定
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = bossSprite;
        // Rigidbody2Dを追加（重力は使用しない）
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;

        // Collider2Dを追加（当たり判定用）
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true; // トリガーとして機能
        isBossActivated = true;
    }
    public void SpawnBoss()
    {
        // ボスを画面内に出現させる
        Instantiate(gameObject);
        transform.position = new Vector3(spawnX, 0.0f, 0.0f);
        isMovingLeft = true; // ボスが左移動を開始
        isBossActivated = true; // ボスが有効化される
    }
    void Update()
    {

        if (!isBossActivated)
        {
            return; // ボスが有効化されるまで待機
        }

        if (isMovingLeft)
        {
            // 左に移動
            transform.Translate(Vector3.left * initialSpeed * Time.deltaTime);

            // 一定の距離まで移動したら楕円軌道へ
            if (transform.position.x <= 10.0f - leftMoveDistance)
            {
                isMovingLeft = false;
                
                startPosition = transform.position; // 楕円軌道の基準点
            }
        }
        else
        {
            // 右回りの縦型楕円形の動きを実現
            timeElapsed += Time.deltaTime * ellipseSpeed;

            float x = Mathf.Cos(timeElapsed) * ellipseWidth;
            float y = Mathf.Sin(timeElapsed) * ellipseHeight;

            // 楕円軌道に沿ってボスを移動
            transform.position = new Vector3(startPosition.x + x, startPosition.y + y, 0);
        }
    }

    // 衝突時の処理
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 例えば、プレイヤーや障害物に衝突したときの処理をここに記述
        if (other.gameObject.CompareTag("Player"))
        {
            // プレイヤーと衝突した場合の処理（例：プレイヤーを破壊）
            Debug.Log("Boss collided with the player!");
            // 追加の処理をここに記述（例：プレイヤーのHP減少やゲームオーバー処理）

        }
    }
}
