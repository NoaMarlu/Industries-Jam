using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialFilledController : MonoBehaviour
{
    [SerializeField, Label("�I��点��܂ł̎���")] private float wateTime = 1;

    [SerializeField, Label("�����l"),Range(0,1)] private float startAmount = 0;

    [SerializeField, Label("�I���l"), Range(0, 1)] private float endAmount = 1;

    [SerializeField, Label("���[�v�����邩")] private bool isLoop = false;

    [SerializeField, Label("��A�N�e�B�u�̍ێ����Ń��Z�b�g")] private bool isAutoInitialize = true;

    // ���삪����������
    [HideInInspector] public bool isDecision = false;

    // ���씽�]
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

    // ��A�N�e�B�u�̍ۂ̍X�V����
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
