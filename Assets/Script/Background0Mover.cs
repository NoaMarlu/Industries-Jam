using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background0Mover : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_offsetSpeed; // xとyの値が大きいほど速くスクロールする

    [SerializeField]
    private float m_accelSpeed; // 加速度

    private Vector3 m_cameraRectMin;

    [SerializeField]
    private bool m_getParts = false; // パーツを入手したフラグ

    [SerializeField]
    private bool m_lostParts = false; // パーツをロストしたフラグ


    void Start()
    {
        //カメラの範囲を取得
        m_cameraRectMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));

        m_offsetSpeed.x = -0.2f;
        m_accelSpeed = -0.1f;
    }

    void Update()
    {
        transform.Translate(Vector3.right * m_offsetSpeed * Time.deltaTime);   //X軸方向にスクロール

        //カメラの左端から完全に出たら、右端に瞬間移動
        if (transform.position.x < (m_cameraRectMin.x - Camera.main.transform.position.x) * 2)
        {
            transform.position = new Vector2((Camera.main.transform.position.x - m_cameraRectMin.x) * 2, transform.position.y);
        }

        m_getParts = GameSystem.Instance.ItemGet;
        // パーツを入手したらスクロール速度を上げる
        if (m_getParts)
        {
            m_offsetSpeed = new Vector2(m_offsetSpeed.x + m_accelSpeed, m_offsetSpeed.y);
            m_offsetSpeed.x = Mathf.Max(m_offsetSpeed.x, -50.0f);
            m_getParts = false;
        }

        // パーツをロストしたらスクロール速度を下げる
        if (m_lostParts)
        {
            m_offsetSpeed = new Vector2(m_offsetSpeed.x - m_accelSpeed, m_offsetSpeed.y);
            m_offsetSpeed.x = Mathf.Min(m_offsetSpeed.x, -1.0f);
            m_lostParts = false;
        }
    }
}