using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{

    [SerializeField] Text speedText;

    public int speed = 0;

    // 他クラスでの呼び出し用(あまり多用しない事)
    public static SpeedController instance;
    public void Awake()
    {
        if (instance == null) instance = this;
    }

    //// デバッグ用
    //private void Update()
    //{
    //    OnText(speed) ;
    //}

    // テキストに反映
    public void OnText(int s)
    {
        speedText.text = s.ToString() + "km";
    }

    public void SetSpeed(int s) { speed = s; OnText(s); }

    public int GetSpeed() { return speed; }
}
