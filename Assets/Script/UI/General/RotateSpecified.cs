using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpecified : MonoBehaviour
{
    [SerializeField, Label("初期回転値")] private float startRotation;

    [SerializeField, Label("終了回転値")] private float endRotation;

    [SerializeField, Label("終わるまでにかかる時間")] private float wateTime = 1;

    [SerializeField, Label("非アクティブの際自動でリセット")] private bool isAutoInitialize = true;

    [SerializeField, Label("ループさせるか")] public bool loop = false;

    // 動作が完了したか
    [HideInInspector] public bool isDecision = false;

    // 動作反転
    [HideInInspector] public bool isReverse = false;

    [HideInInspector] private float timer = 0;

    private void Start()
    {
        Initialize();
    }
    void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        timer = 0;

        isDecision = false;

        Quaternion quaternion = new Quaternion();

        quaternion.eulerAngles = (isReverse) ? new Vector3(0, 0, endRotation) : new Vector3(0, 0, startRotation);

        transform.localRotation = quaternion;
    }

    private void Update()
    {
        if (isDecision) return;// 動作が完了したら更新させない

        timer += 1.0f * Time.deltaTime;

        float easePos = Easing.ExpoOut(Mathf.Clamp(timer, 0, wateTime), wateTime);

        Quaternion quaternion = new Quaternion();

        quaternion.eulerAngles = (isReverse) ? new Vector3(0, 0, Mathf.LerpAngle(endRotation, startRotation, easePos)) :
                                               new Vector3(0, 0, Mathf.LerpAngle(startRotation, endRotation, easePos)) ;

        transform.localRotation = quaternion;

        if (timer < wateTime) return;

        isDecision = true;

        if (!loop) return;
        isReverse = false;
        Initialize();
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

    public void SetInitialize() { Initialize(); }

    public void SetStartRotation(float startRotation) { this.startRotation = startRotation; }

    public void SetEndRotation(float endRotation) { this.endRotation = endRotation; }

    public void SetWateTime(float wateTime) { this.wateTime = wateTime; }

    public void SetIsAutoInitialize(bool isAutoInitialize) { this.isAutoInitialize = isAutoInitialize; }

    public void SetIsLoop(bool loop) { this.loop = loop; }

    public void SetIsReverse(bool isReverse) { this.isReverse = isReverse; }

    public bool GetIsFinish(float adJust = 0.2f) { return timer >= wateTime - adJust; }
}
