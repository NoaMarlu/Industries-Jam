using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMove : MonoBehaviour
{
    [SerializeField, Label("ポーズで使うか")] private bool isPause = false;

    [SerializeField, Label("初期のサイズ")] private float startHeight;

    [SerializeField, Label("終了のサイズ")] private float endHeight;

    [SerializeField, Label("終わるまでの時間")] private float wateTime = 1;

    [SerializeField, Label("非アクティブの際自動でリセット")] private bool isAutoInitialize = true;

    [SerializeField, Label("ループさせるか")] private bool loop = false;
 
    private float timer = 0;

    // 動作が完了したか
    private bool isDecision = false;

    // 動作反転
    private bool isReverse = false;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (isDecision) return;// 動作が完了したら更新させない

        if (isPause)
            timer += 1.0f * Time.unscaledDeltaTime;
        else
            timer += 1.0f * Time.deltaTime;

        float easePos = Easing.ExpoOut(Mathf.Clamp(timer, 0, wateTime), wateTime);

        float moveHeight = (isReverse) ? Mathf.Lerp(endHeight, startHeight, easePos) : Mathf.Lerp(startHeight, endHeight, easePos);
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, moveHeight);
        }

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
    public void Initialize()
    {
        timer = 0;
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, startHeight);
        }
        isDecision = false;
    }

    public void SetStartHeight(float startHeight) { this.startHeight = startHeight; }

    public void SetEndHeight(float endHeight) { this.endHeight = endHeight; }

    public void SetWateTime(float wateTime) { this.wateTime = wateTime; }

    public void SetIsAutoInitialize(bool isAutoInitialize) { this.isAutoInitialize = isAutoInitialize; }

    public void SetIsLoop(bool loop) { this.loop = loop; }

    public void SetIsPause(bool isPause) { this.isPause = isPause; }

    public void SetIsReverse(bool isReverse) { this.isReverse = isReverse; }
}
