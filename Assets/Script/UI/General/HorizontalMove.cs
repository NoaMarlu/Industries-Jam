using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMove : MonoBehaviour
{
    [SerializeField, Label("ポーズで使うか")] public bool isPause = false;

    [SerializeField, Label("初期位置")] public float startPos;

    [SerializeField, Label("終了位置")] public float endPos;

    [SerializeField, Label("終わるまでの時間")] public float wateTime = 10;

    [SerializeField, Label("非アクティブの際自動でリセット")] private bool isAutoInitialize = true;

    [SerializeField, Label("ループさせるか")] public bool loop = false;

    [HideInInspector] public float timer = 0;

    // 動作が完了したか
    [HideInInspector] public bool isDecision = false;

    // 動作反転
    [HideInInspector] public bool isReverse = false;

    void Start()
    {
        Initialize();
    }

    void OnEnable()
    {
        Initialize();
    }

    protected virtual void Update()
    {
        if (isDecision) return;// 動作が完了したら更新させない

        if (isPause) timer += 1.0f * Time.unscaledDeltaTime;
        else timer += 1.0f * Time.deltaTime;

        float easePos = Easing.ExpoOut(Mathf.Clamp(timer, 0, wateTime), wateTime);

        Vector3 movePos =
            new Vector3((isReverse) ? Mathf.Lerp(endPos, startPos, easePos) : Mathf.Lerp(startPos, endPos, easePos),
            this.gameObject.transform.localPosition.y,
            this.gameObject.transform.localPosition.z);

        this.gameObject.transform.localPosition = movePos;

        if (timer >= wateTime)
        {
            isDecision = true;

            if (loop)
            {
                isReverse = false;
                Initialize();
            }
        }
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

    // 初期化
    protected virtual void Initialize()
    {
        timer = 0;
        this.gameObject.transform.localPosition =
            new Vector3(startPos, this.gameObject.transform.localPosition.y, this.gameObject.transform.localPosition.z);
        isDecision = false ;
    }

    public void SetInitialize() { Initialize(); }

    public void SetStartPos(float startPos) { this.startPos = startPos; }

    public void SetEndPos(float endPos) { this.endPos = endPos; }

    public void SetWaitTime(float wateTime) { this.wateTime = wateTime; }

    public void SetIsAutoInitialize(bool isAutoInitialize) { this.isAutoInitialize = isAutoInitialize; }

    public void SetIsLoop(bool loop) { this.loop = loop; }

    public void SetIsPause(bool isPause) { this.isPause = isPause; }

    public void SetIsReverse(bool isReverse) { this.isReverse = isReverse; }

    public bool GetIsFinish(float adJust = 0.2f) { return timer >= wateTime - adJust; }

}
