using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMove : MonoBehaviour
{
    [SerializeField, Label("�|�[�Y�Ŏg����")] public bool isPause = false;

    [SerializeField, Label("�����ʒu")] public float startPos;

    [SerializeField, Label("�I���ʒu")] public float endPos;

    [SerializeField, Label("�I���܂ł̎���")] public float wateTime = 10;

    [SerializeField, Label("��A�N�e�B�u�̍ێ����Ń��Z�b�g")] private bool isAutoInitialize = true;

    [SerializeField, Label("���[�v�����邩")] public bool loop = false;

    [HideInInspector] public float timer = 0;

    // ���삪����������
    [HideInInspector] public bool isDecision = false;

    // ���씽�]
    [HideInInspector] public bool isReverse = false;

    void Start()
    {
        Initialize();
    }

    void OnEnable()
    {
        Initialize();
    }

    private void Update()
    {
        if (isDecision) return;// ���삪����������X�V�����Ȃ�

        if (isPause) timer += 1.0f * Time.unscaledDeltaTime;
        else timer += 1.0f * Time.deltaTime;

        float easePos = Easing.ExpoOut(Mathf.Clamp(timer, 0, wateTime), wateTime);

        Vector3 movePos =
            new Vector3(this.gameObject.transform.localPosition.x,
            (isReverse) ? Mathf.Lerp(endPos, startPos, easePos) : Mathf.Lerp(startPos, endPos, easePos),
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

    // ��A�N�e�B�u�̍ۂ̍X�V����
    private void OnDisable()
    {
        if (isAutoInitialize)
        {
            isReverse = false;
            Initialize();
        }
    }

    // ������
    private void Initialize()
    {
        timer = 0;
        this.gameObject.transform.localPosition =
            new Vector3(this.gameObject.transform.localPosition.x, startPos, this.gameObject.transform.localPosition.z);
        isDecision = false;
    }

    public void SetInitialize() { Initialize(); }

    public void SetStartPos(float startPos) { this.startPos = startPos; }

    public void SetEndPos(float endPos) { this.endPos = endPos; }

    public void SetWateTime(float wateTime) { this.wateTime = wateTime; }

    public void SetIsAutoInitialize(bool isAutoInitialize) { this.isAutoInitialize = isAutoInitialize; }

    public void SetIsLoop(bool loop) { this.loop = loop; }

    public void SetIsPause(bool isPause) { this.isPause = isPause; }

    public void SetIsReverse(bool isReverse) { this.isReverse = isReverse; }

    public bool GetIsFinish(float adJust = 0.2f) { return timer >= wateTime - adJust; }

}

//public class VerticalMove : HorizontalMove
//{
//    // Update is called once per frame
//    protected override void Update()
//    {
//        if (isDecision) return;// ���삪����������X�V�����Ȃ�

//        if (isPause) timer += 1.0f * Time.unscaledDeltaTime;
//        else timer += 1.0f * Time.deltaTime;

//        float easePos = Easing.ExpoOut(Mathf.Clamp(timer, 0, waitTime), waitTime);

//        Vector3 movePos =
//            new Vector3(this.gameObject.transform.localPosition.x,
//            (isReverse) ? Mathf.Lerp(endPos, startPos, easePos) : Mathf.Lerp(startPos, endPos, easePos),
//            this.gameObject.transform.localPosition.z);

//        this.gameObject.transform.localPosition = movePos;

//        if (timer >= waitTime)
//        {
//            isDecision = true;

//            if (loop)
//            {
//                isReverse = false;
//                Initialize();
//            }
//        }
//    }

//    protected override void Initialize()
//    {
//        timer = 0;
//        this.gameObject.transform.localPosition =
//            new Vector3(this.gameObject.transform.localPosition.x, startPos, this.gameObject.transform.localPosition.z);
//        isDecision = false;
//    }
//}
