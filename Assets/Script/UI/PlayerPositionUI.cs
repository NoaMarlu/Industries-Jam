using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionUI : MonoBehaviour
{
    [SerializeField] private Transform enemyPos;

    [SerializeField] private float appearanceDistance = 100;// 敵が出現するまでの距離

    private float startPos = 0;

    // 現在の距離
    public float distance = 0;

    // 他クラスでの呼び出し用(あまり多用しない事)
    public static PlayerPositionUI instance;
    public void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を保持させる
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // 割合
        float rate = distance / appearanceDistance;

        // UIに反映
        transform.position = new Vector2(Mathf.Lerp(startPos, enemyPos.position.x, rate), transform.position.y);
    }

    // 現在の距離を設定
    void SetDistace(float dist) { distance = dist; }
}
