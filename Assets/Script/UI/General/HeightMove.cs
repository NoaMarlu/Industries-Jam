using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMove : MonoBehaviour
{
    [SerializeField, Label("�|�[�Y�Ŏg����")] private bool isPause = false;

    [SerializeField, Label("�����̃T�C�Y")] private float startHeight;

    [SerializeField, Label("�I���̃T�C�Y")] private float endHeight;

    [SerializeField, Label("�I���܂ł̎���")] private float wateTime = 1;

    [SerializeField, Label("��A�N�e�B�u�̍ێ����Ń��Z�b�g")] private bool isAutoInitialize = true;

    [SerializeField, Label("���[�v�����邩")] private bool loop = false;
 
    private float timer = 0;

    // ���삪����������
    private bool isDecision = false;

    // ���씽�]
    private bool isReverse = false;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (isDecision) return;// ���삪����������X�V�����Ȃ�

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
