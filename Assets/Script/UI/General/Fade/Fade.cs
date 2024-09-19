using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField, Label("�|�[�Y�Ŏg����")] public bool isPause = false;

    [SerializeField, Label("�t�F�[�h�̎��")] public Type fadeType = Type.Out;

    [SerializeField, Label("�t�F�[�h�����鎞��")] public float wateTime = 1.0f; 

    [SerializeField, Label("�ő�l")] public float maxAlpha = 1;

    [SerializeField, Label("��A�N�e�B�u�̍ێ����Ń��Z�b�g")] private bool isAutoInitialize = true;

    [SerializeField, Label("�����������������A�N�e�B�u�������邩")] private bool autoActiveOut = false;

    [SerializeField, Label("����������������폜���邩")] private bool isDestroy = false;

    [SerializeField, Label("�������珜�O����I�u�W�F�N�g")] public List<ObjType> objTypes;

    public enum ObjType
    {
        Image,
        Text,
        Sprite,
        RawImage,
        GradationController,
        Max
    }

    // �t�F�[�h�̎��
    public enum Type
    {
        Out,
        In
    }

    private bool isOnes = false;

    // �t�F�[�h�����ǂ���
    [SerializeField] private bool isFading = false;

    // �C�[�W���O�^�C�}�[
    [SerializeField] public float et = 0;

    void Start()
    {
        Initialize();
    }

    // ����������
    protected virtual void Initialize()
    {
        // �������珜�O���邩�ǂ������m�F
        bool[] notInitialize = new bool[(int)ObjType.Max];
        if (objTypes != null) 
        {
            foreach (var objTypes in objTypes)
            {
                if (objTypes == ObjType.Image) notInitialize[(int)ObjType.Image] = true;
                if (objTypes == ObjType.Text) notInitialize[(int)ObjType.Text] = true;
                if (objTypes == ObjType.Sprite) notInitialize[(int)ObjType.Sprite] = true;
                if (objTypes == ObjType.RawImage) notInitialize[(int)ObjType.RawImage] = true;
                if (objTypes == ObjType.GradationController) notInitialize[(int)ObjType.GradationController] = true;
            }
        }

        // �w�肵���I�u�W�F�N�g�̎q���̃C���[�W�f�[�^���Q�Ƃ���
        if (!notInitialize[(int)ObjType.Image])
        {
            var childrenImageData = this.GetComponentsInChildren<Image>();
            foreach (var image in childrenImageData)
            {
                Color c = image.color;

                c.a = (fadeType == Type.In) ? 1 : 0;

                image.color = c;
            }
        }
        // �w�肵���I�u�W�F�N�g�̎q���̃e�L�X�g�f�[�^���Q�Ƃ���
        if (!notInitialize[(int)ObjType.Text])
        {
            var childrenTextData = this.GetComponentsInChildren<Text>();
            foreach (var text in childrenTextData)
            {
                Color c = text.color;

                c.a = (fadeType == Type.In) ? 1 : 0;

                text.color = c;
            }
        }
        // �w�肵���I�u�W�F�N�g�̎q���̃X�v���C�g���Q�Ƃ���
        if (!notInitialize[(int)ObjType.RawImage])
        {
            var SpriteRenderer = this.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in SpriteRenderer)
            {
                Color c = sprite.color;

                c.a = (fadeType == Type.In) ? 1 : 0;

                sprite.color = c;
            }
        }
        // �w�肵���I�u�W�F�N�g�̎q���̓���C���[�W���Q�Ƃ���
        if (!notInitialize[(int)ObjType.Sprite])
        {
            var rawImages = this.GetComponentsInChildren<RawImage>();
            foreach (var raw in rawImages)
            {
                Color c = raw.color;

                c.a = (fadeType == Type.In) ? 1 : 0;

                raw.color = c;
            }
        }

        et = 0;
        isFading = isOnes = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isPause) et += 1.0f * Time.unscaledDeltaTime;
        else et += 1.0f * Time.deltaTime;

        float easePos = Easing.ExpoOut(et, wateTime);

        // �������珜�O���邩�ǂ������m�F
        bool[] notInitialize = new bool[(int)ObjType.Max];
        if (objTypes != null)
        {
            foreach (var objTypes in objTypes)
            {
                if (objTypes == ObjType.Image) notInitialize[(int)ObjType.Image] = true;
                if (objTypes == ObjType.Text) notInitialize[(int)ObjType.Text] = true;
                if (objTypes == ObjType.Sprite) notInitialize[(int)ObjType.Sprite] = true;
                if (objTypes == ObjType.GradationController) notInitialize[(int)ObjType.GradationController] = true;
            }
        }

        // �w�肵���I�u�W�F�N�g�̎q���̃C���[�W�f�[�^���Q�Ƃ���
        if (!notInitialize[(int)ObjType.Image])
        {
            var childrenImageData = this.GetComponentsInChildren<Image>();
            foreach (var image in childrenImageData)
            {
                Color c = image.color;
                c.a = (fadeType == Type.In) ? Mathf.Lerp(maxAlpha, 0, easePos) : Mathf.Lerp(0, maxAlpha, easePos);
                image.color = c;
            }
        }

        // �w�肵���I�u�W�F�N�g�̎q���̃e�L�X�g�f�[�^���Q�Ƃ���
        if (!notInitialize[(int)ObjType.Text])
        {
            var childrenTextData = this.GetComponentsInChildren<Text>();
            foreach (var text in childrenTextData)
            {
                Color c = text.color;
                c.a = (fadeType == Type.In) ? Mathf.Lerp(maxAlpha, 0, easePos) : Mathf.Lerp(0, maxAlpha, easePos);
                text.color = c;
            }
        }

        // �w�肵���I�u�W�F�N�g�̎q���̃X�v���C�g�f�[�^���Q�Ƃ���
        if (!notInitialize[(int)ObjType.Sprite])
        {
            var spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in spriteRenderers)
            {
                Color c = sprite.color;
                c.a = (fadeType == Type.In) ? Mathf.Lerp(maxAlpha, 0, easePos) : Mathf.Lerp(0, maxAlpha, easePos);
                sprite.color = c;
            }
        }

        // �w�肵���I�u�W�F�N�g�̎q���̃X�v���C�g�f�[�^���Q�Ƃ���
        if (!notInitialize[(int)ObjType.RawImage])
        {
            var rawImages = this.GetComponentsInChildren<RawImage>();
            foreach (var raw in rawImages)
            {
                Color c = raw.color;
                c.a = (fadeType == Type.In) ? Mathf.Lerp(maxAlpha, 0, easePos) : Mathf.Lerp(0, maxAlpha, easePos);
                raw.color = c;
            }
        }


        if (et < wateTime && !isOnes) return;

        // �t�F�[�h���I��������Ƃ�`����
        isFading = isOnes = true;

        // ���g���폜
        if (isDestroy)
            Destroy(this);

        // �������I�������玩�g���A�N�e�B�u�����č폜����
        if (autoActiveOut)
            this.gameObject.SetActive(false);
    }

    // ��A�N�e�B�u�̍ۂ̍X�V����
    private void OnDisable()
    {
        if (isAutoInitialize)
        {
            Initialize();
        }
    }

    // �^�C�}�[�����Z�b�g
    public void ResetTimer() { et = 0; }

    // �t�F�[�h�����鎞��
    public void SetWaitTime(float wateTime) { this.wateTime = wateTime; }

    // �t�F�[�h�̍ő�l
    public void SetMaxAlpha(float maxAlpha) { this.maxAlpha = maxAlpha; }

    // ��A�N�e�B�u�̍ێ����Ń��Z�b�g���邩�ǂ���
    public void SetIsAutoInitialize(bool isAutoInitialize) { this.isAutoInitialize = isAutoInitialize; }

    // ����������������폜���邩
    public void SetIsDestroy(bool isDestroy) { this.isDestroy = isDestroy; }

    // �����������������A�N�e�B�u�������邩�ǂ���
    public void SetAutoActiveOut(bool autoActiveOut) { this.autoActiveOut = autoActiveOut; }

    // �t�F�[�h�̎�ނ��Z�b�g
    public void SetFadeType(Type fadeType) { this.fadeType = fadeType; }

    // �t�F�[�h���Ă��邩�ǂ���
    public bool GetIsFading() { return isFading ; }

    // �|�[�Y��ʂŎg�p���邩�ǂ���
    public void SetIsPause(bool isPause) { this.isPause = isPause; }

    // �@�\���]
    public void SetIsReverse() { fadeType = (fadeType == Type.In) ? Type.Out : Type.In; Initialize(); }

    // �������珜�O����I�u�W�F�N�g��ݒ�
    public void RegisterExclusionObj(ObjType objType) 
    {
        if (objTypes == null) 
        {
            objTypes = new List<ObjType>();
        }
        objTypes.Add(objType);
    }

    public Type GetFadeType() { return fadeType; }
}
