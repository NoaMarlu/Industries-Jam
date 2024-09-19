using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionUI : MonoBehaviour
{
    [SerializeField] private Transform enemyPos;

    [SerializeField] private float appearanceDistance = 100;// �G���o������܂ł̋���

    private float startPos = 0;

    // ���݂̋���
    public float distance = 0;

    // ���N���X�ł̌Ăяo���p(���܂葽�p���Ȃ���)
    public static PlayerPositionUI instance;
    public void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu��ێ�������
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // ����
        float rate = distance / appearanceDistance;

        // UI�ɔ��f
        transform.position = new Vector2(Mathf.Lerp(startPos, enemyPos.position.x, rate), transform.position.y);
    }

    // ���݂̋�����ݒ�
    void SetDistace(float dist) { distance = dist; }
}
