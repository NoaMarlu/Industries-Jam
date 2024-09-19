using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpecified : MonoBehaviour
{
    [SerializeField, Label("������]�l")] private float startRotation;

    [SerializeField, Label("�I����]�l")] private float endRotation;

    [SerializeField, Label("�I���܂łɂ����鎞��")] private float wateTime = 1;

    [SerializeField, Label("��A�N�e�B�u�̍ێ����Ń��Z�b�g")] private bool isAutoInitialize = true;

    [SerializeField, Label("���[�v�����邩")] public bool loop = false;

    // ���삪����������
    [HideInInspector] public bool isDecision = false;

    // ���씽�]
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
        if (isDecision) return;// ���삪����������X�V�����Ȃ�

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

    // ��A�N�e�B�u�̍ۂ̍X�V����
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
