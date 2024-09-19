using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopFade : Fade
{
    [SerializeField, Label("�ŏ��l")] public float minAlpha = 0;

    // Update is called once per frame
    protected override void Update()
    {
        if (isPause) et += 1.0f * Time.unscaledDeltaTime;
        else et += 1.0f * Time.deltaTime;

        float easePos = Easing.SineIn(et, wateTime);

        // �������珜�O���邩�ǂ������m�F
        bool[] notInitialize = new bool[(int)ObjType.Max];
        if (objTypes != null)
        {
            foreach (var objTypes in objTypes)
            {
                if (objTypes == ObjType.Image) notInitialize[(int)ObjType.Image] = true;
                if (objTypes == ObjType.Text) notInitialize[(int)ObjType.Text] = true;
                if (objTypes == ObjType.Sprite) notInitialize[(int)ObjType.Sprite] = true;
            }
        }

        // �w�肵���I�u�W�F�N�g�̎q���̃C���[�W�f�[�^���Q�Ƃ���
        if (!notInitialize[(int)ObjType.Image])
        {
            var childrenImageData = this.GetComponentsInChildren<Image>();
            foreach (var image in childrenImageData)
            {
                Color c = image.color;
                c.a = (fadeType == Type.In) ? Mathf.Lerp(maxAlpha, minAlpha, easePos) : Mathf.Lerp(minAlpha, maxAlpha, easePos);
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
                c.a = (fadeType == Type.In) ? Mathf.Lerp(maxAlpha, minAlpha, easePos) : Mathf.Lerp(minAlpha, maxAlpha, easePos);
                text.color = c;
            }
        }
        
        if (!notInitialize[(int)ObjType.Sprite])
        {
            var sprites = this.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in sprites)
            {
                Color c = sprite.color;
                c.a = (fadeType == Type.In) ? Mathf.Lerp(maxAlpha, minAlpha, easePos) : Mathf.Lerp(minAlpha, maxAlpha, easePos);
                sprite.color = c;
            }
        }
    }

    protected override void Initialize()
    {
        // �������珜�O���邩�ǂ������m�F
        bool[] notInitialize = new bool[(int)ObjType.Max];
        if (objTypes != null)
        {
            foreach (var objTypes in objTypes)
            {
                if (objTypes == ObjType.Image) notInitialize[(int)ObjType.Image] = true;
                if (objTypes == ObjType.Text) notInitialize[(int)ObjType.Text] = true;
            }
        }

        // �w�肵���I�u�W�F�N�g�̎q���̃C���[�W�f�[�^���Q�Ƃ���
        if (!notInitialize[(int)ObjType.Image])
        {
            var childrenImageData = this.GetComponentsInChildren<Image>();
            foreach (var image in childrenImageData)
            {
                Color c = image.color;

                c.a = (fadeType == Type.In) ? maxAlpha : minAlpha;

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

                c.a = (fadeType == Type.In) ? maxAlpha : minAlpha;

                text.color = c;
            }
        }
        if (!notInitialize[(int)ObjType.Sprite])
        {
            var spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in spriteRenderers)
            {
                Color c = sprite.color;

                c.a = (fadeType == Type.In) ? maxAlpha : minAlpha;

                sprite.color = c;
            }
        }
        et = 0;
    }
}
