using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChangeBound : MonoBehaviour
{
    [SerializeField, Label("�����̃T�C�Y")] private float startScale = 1;

    [SerializeField, Label("�I���̃T�C�Y")] private float endScale = 0;

    [SerializeField, Label("�I���܂ł̎���")] private float wateTime = 1;

    [SerializeField, Label("��A�N�e�B�u�̍ێ����Ń��Z�b�g")] private bool isAutoInitialize = true;

    [SerializeField, Label("���[�v�����邩")] private bool loop = false;

    [SerializeField, Label("�����ŏ����T�C�Y����")] private bool autoMeasure = false;

    [SerializeField, Label("�����ō폜")] private bool autoDestroy = false;

    [SerializeField, Label("�Œ肵�Ă���������")] private Shaft fixeShaft = Shaft.NONE;

    private Vector2 scale;

    private float timer = 0;

    // ���삪����������
    private bool isDecision = false;

    // ���씽�]
    private bool isReverse = false;

    public enum Shaft
    {
        NONE,
        X_SHAFT,
        Y_SHAFT
    }

    void Start()
    {
        if (autoMeasure)
            scale = gameObject.transform.localScale;

        Initialize();
    }

    void Update()
    {
        if (isDecision) return;// ���삪����������X�V�����Ȃ�

        timer += 1.0f * Time.deltaTime;

        float easePos = Easing.BounceOut(Mathf.Clamp(timer, 0, wateTime), wateTime);

        if (autoMeasure)
        {
            Vector2 moveScale = (isReverse) ? new Vector2(Mathf.Lerp(scale.x, endScale, easePos), Mathf.Lerp(scale.y, endScale, easePos)) :
                                              new Vector2(Mathf.Lerp(endScale, scale.x, easePos), Mathf.Lerp(endScale, scale.y, easePos));

            if (fixeShaft == Shaft.X_SHAFT)
            {
                moveScale.x = transform.localScale.x;
            }
            if (fixeShaft == Shaft.Y_SHAFT)
            {
                moveScale.y = transform.localScale.y;
            }

            transform.localScale = new Vector3(moveScale.x, moveScale.y, gameObject.transform.localScale.z);
        }
        else
        {
            float moveScale = (isReverse) ? Mathf.Lerp(endScale, startScale, easePos) : Mathf.Lerp(startScale, endScale, easePos);
            Vector2 setScale = new Vector2(moveScale, moveScale);

            if (fixeShaft == Shaft.X_SHAFT)
            {
                setScale.x = transform.localScale.x;
            }
            if (fixeShaft == Shaft.Y_SHAFT)
            {
                setScale.y = transform.localScale.y;
            }
            transform.localScale = new Vector3(setScale.x, setScale.y, 1);
        }

        if (timer >= wateTime)
        {
            isDecision = true;

            if (autoDestroy)
            {
                Destroy(gameObject);
            }

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
    public void Initialize()
    {
        timer = 0;
        if (!autoMeasure)
        {
            switch (fixeShaft)
            {
                case Shaft.NONE:
                    transform.localScale = new Vector3(startScale, startScale, 1);
                    break;
                case Shaft.X_SHAFT:
                    transform.localScale = new Vector3(transform.localScale.x, startScale, 1);
                    break;
                case Shaft.Y_SHAFT:
                    transform.localScale = new Vector3(startScale, transform.localScale.y, 1);
                    break;
            }
        }
        else
            gameObject.transform.localScale = scale;
        isDecision = false;
    }

    public void SetStartScale(float startScale) { this.startScale = startScale; }

    public void SetEndScale(float endScale) { this.endScale = endScale; }

    public void SetWateTime(float wateTime) { this.wateTime = wateTime; }

    public void SetIsAutoInitialize(bool isAutoInitialize) { this.isAutoInitialize = isAutoInitialize; }

    public void SetIsLoop(bool loop) { this.loop = loop; }

    public void SetIsReverse(bool isReverse) { this.isReverse = isReverse; }

    public void SetAutoMeasure(bool autoMeasure) { this.autoMeasure = autoMeasure; }

    public void SetAutoDestroy(bool autoDestroy) { this.autoDestroy = autoDestroy; }

    public bool GetIsDecision() { return isDecision; }

    public bool GetIsFinish(float adJust = 0.2f) { return timer >= wateTime - adJust; }
}
