using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialFilledController : MonoBehaviour
{
    [SerializeField, Label("終わらせるまでの時間")] private float wateTime = 1;

    [SerializeField, Label("初期値"),Range(0,1)] private float startAmount = 0;

    [SerializeField, Label("終了値"), Range(0, 1)] private float endAmount = 1;

    [SerializeField, Label("ループさせるか")] private bool isLoop = false;

    [SerializeField, Label("非アクティブの際自動でリセット")] private bool isAutoInitialize = true;

    // 動作が完了したか
    [HideInInspector] public bool isDecision = false;

    // 動作反転
    [HideInInspector] public bool isReverse = false;

    float timer;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float easePos = (isReverse) ? Easing.ExpoOut(timer, wateTime) : Easing.ExpoIn(timer, wateTime);

        Image image = gameObject.GetComponent<Image>();
        image.fillAmount = 
            (isReverse) ? Mathf.Lerp(endAmount, startAmount, easePos) : Mathf.Lerp(startAmount, endAmount, easePos);

        if (timer < wateTime) return;

        isDecision = true;

        if (!isLoop) return;

        timer = 0;
        isReverse = false;
        Initialize();
    }

    public void Initialize()
    {
        isDecision = false;
        timer = 0;
        Image image = gameObject.GetComponent<Image>();
        image.fillAmount = (isReverse) ? endAmount : startAmount;
    }

    // 非アクティブの際の更新処理
    private void OnDisable()
    {
        if (isAutoInitialize)
        {
            isReverse = false; 
            Initialize();
        }
    }

    public void SetIsReverse(bool isReverse) { this.isReverse = isReverse; }

    public bool GetIsDecision() { return isDecision; }

    public bool GetIsFinish(float adJust = 0.2f) { return timer >= wateTime - adJust; }
}
