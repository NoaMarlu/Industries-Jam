using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{

    [SerializeField] Text speedText;

    public int speed = 0;

    // ���N���X�ł̌Ăяo���p(���܂葽�p���Ȃ���)
    public static SpeedController instance;
    public void Awake()
    {
        if (instance == null) instance = this;
    }

    //// �f�o�b�O�p
    //private void Update()
    //{
    //    OnText(speed) ;
    //}

    // �e�L�X�g�ɔ��f
    public void OnText(int s)
    {
        speedText.text = s.ToString() + "km";
    }

    public void SetSpeed(int s) { speed = s; OnText(s); }

    public int GetSpeed() { return speed; }
}
