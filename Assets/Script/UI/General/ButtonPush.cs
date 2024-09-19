using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPush : MonoBehaviour
{
    [SerializeField, Label("ポーズで使うか")] private bool isPause = false;

    [SerializeField, Label("初期のスケール")] private float startScale = 1;

    [SerializeField, Label("終了のスケール")] private float endScale = 0.8f;

    [SerializeField, Label("終わるまでの時間")] private float wateTime = 1;

    [SerializeField, Label("非アクティブの際自動でリセット")] private bool isAutoInitialize = false;

    [SerializeField, Label("ループさせるか")] private bool loop = false;

    private float timer;

    private bool second;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        second = false;
        timer = 0;
        gameObject.transform.localScale = new Vector3(startScale, startScale, startScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause)
            timer += 1.0f * Time.unscaledDeltaTime;
        else
            timer += 1.0f * Time.deltaTime;

        if (!second)
        {
            float easePos = Easing.ExpoIn(Mathf.Clamp(timer, 0, wateTime * 0.5f), wateTime * 0.5f) ;
            float moveScale = Mathf.Lerp(startScale, endScale, easePos);
            gameObject.transform.localScale = new Vector3(moveScale, moveScale, moveScale);
            if (timer >= wateTime * 0.5f)
            {
                second = true;
                timer = 0;
            }
        }
        else
        {
            float easePos = Easing.ExpoOut(Mathf.Clamp(timer, 0, wateTime * 0.5f), wateTime * 0.5f);
            float moveScale = Mathf.Lerp(endScale, startScale, easePos);
            gameObject.transform.localScale = new Vector3(moveScale, moveScale, moveScale);
            if (timer >= wateTime * 0.5f)
            {
                if (loop) Initialize();
            }
        }
    }

    // 非アクティブの際の更新処理
    private void OnDisable()
    {
        if (isAutoInitialize) Initialize();
        else Destroy(this);
    }
    public void SetStartScale(float startScale) { this.startScale = startScale; }

    public void SetEndScale(float endScale) { this.endScale = endScale; }

    public void SetWateTime(float wateTime) { this.wateTime = wateTime; }

    public void SetIsAutoInitialize(bool isAutoInitialize) { this.isAutoInitialize = isAutoInitialize; }

    public void SetIsLoop(bool loop) { this.loop = loop; }

    public bool GetIsFinish(float adJust = 0.2f) { return (second && timer >= (wateTime * 0.5f) - adJust); }

    public void SetIsPause(bool isPause) { this.isPause = isPause; }

}
