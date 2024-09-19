using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; } // �V���O���g���C���X�^���X

    public float BaseSpeed=5.0f;
    public float MaxBossDistance = 1000.0f;
    public float GamePosition = 0.0f;
    public Boss boss; // �{�X�̎Q��
    public bool isBossActive = false; // �{�X���A�N�e�B�u���ǂ���
    //public float 
    void Awake()
    {
        // �V���O���g���̐ݒ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ł��폜����Ȃ��悤�ɂ���
        }
        else
        {
            Destroy(gameObject); // ���łɃC���X�^���X������ꍇ�͍폜
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GamePosition += BaseSpeed*Time.deltaTime;

        
        // �{�X��o�ꂳ��������𖞂��������ǂ���
        if (!isBossActive && GamePosition >= MaxBossDistance)
        {
            ActivateBoss(); // �{�X��o�ꂳ����
        }

    }

    // ���̃X�N���v�g����{�X�̑��x���擾���邽�߂̃��\�b�h
    public float GetBossSpeed()
    {
        return BaseSpeed;
    }

    // �{�X�̑��x��ύX���郁�\�b�h�i�K�v�ɉ����āj
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
            boss.SpawnBoss(); // �{�X�̓o��
            isBossActive = true; // �{�X���A�N�e�B�u��ԂɂȂ�
        }
    }
}
