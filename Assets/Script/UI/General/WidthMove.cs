using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidthMove : MonoBehaviour
{
    [SerializeField, Label("�|�[�Y�Ŏg����")] private bool isPause = false;

    [SerializeField, Label("�����̃T�C�Y")] private float startWidth;

    [SerializeField, Label("�I���̃T�C�Y")] private float endWidth;

    [SerializeField, Label("�I���܂ł̎���")] private float wateTime = 10;

    [SerializeField, Label("��A�N�e�B�u�̍ێ����Ń��Z�b�g")] private bool isAutoInitialize = true;

    [SerializeField, Label("���[�v�����邩")] private bool loop = false;

    private float timer = 0;

    // ���삪����������
    private bool isDecision = false;

    // ���씽�]
    private bool isReverse = false;

    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDecision) return;// ���삪����������X�V�����Ȃ�

        if (isPause) timer += 1.0f * Time.unscaledDeltaTime;
        else timer += 1.0f * Time.deltaTime;

        float easePos = Easing.ExpoOut(Mathf.Clamp(timer, 0, wateTime), wateTime);
        float moveWidth = (isReverse) ? Mathf.Lerp(endWidth, startWidth, easePos) : Mathf.Lerp(startWidth, endWidth, easePos);
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(moveWidth, rectTransform.sizeDelta.y);
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
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(startWidth, rectTransform.sizeDelta.y);
        }
        isDecision = false;
    }

    public void SetStartWidth(float startWidth) { this.startWidth = startWidth; }

    public void SetEndWidth(float endWidth) { this.endWidth = endWidth; }

    public void SetWateTime(float wateTime) { this.wateTime = wateTime; }

    public void SetIsAutoInitialize(bool isAutoInitialize) { this.isAutoInitialize = isAutoInitialize; }

    public void SetIsLoop(bool loop) { this.loop = loop; }

    public void SetIsPause(bool isPause) { this.isPause = isPause; }

    public void SetIsReverse(bool isReverse) { this.isReverse = isReverse; }
}
