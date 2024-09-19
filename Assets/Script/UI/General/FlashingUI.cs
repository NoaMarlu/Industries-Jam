using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingUI : MonoBehaviour
{
    [SerializeField, Label("�_�ŊԊu")] private float flashTime;

    [SerializeField, Label("�\������")] private float displayTime = 0.5f;

    private float timer;

    private float displayTimer;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // �w�肵���I�u�W�F�N�g�̎q���̃C���[�W�f�[�^���Q�Ƃ���
        {
            var childrenImageData = this.GetComponentsInChildren<Image>();
            foreach (var image in childrenImageData)
            {
                Color c = image.color;

                c.a = 0;

                image.color = c;
            }
        }
        // �w�肵���I�u�W�F�N�g�̎q���̃e�L�X�g�f�[�^���Q�Ƃ���
        {
            var childrenTextData = this.GetComponentsInChildren<Text>();
            foreach (var text in childrenTextData)
            {
                Color c = text.color;

                c.a = 0;

                text.color = c;
            }
        }
        displayTimer = timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1.0f * Time.deltaTime;

        // �w�肵���I�u�W�F�N�g�̎q���̃C���[�W�f�[�^���Q�Ƃ���
        {
            var childrenImageData = this.GetComponentsInChildren<Image>();
            foreach (var image in childrenImageData)
            {
                Color c = image.color;

                c.a = 0;

                image.color = c;
            }
        }
        // �w�肵���I�u�W�F�N�g�̎q���̃e�L�X�g�f�[�^���Q�Ƃ���
        {
            var childrenTextData = this.GetComponentsInChildren<Text>();
            foreach (var text in childrenTextData)
            {
                Color c = text.color;

                c.a = 0;

                text.color = c;
            }
        }

        if (timer >= flashTime)
        {
            // �w�肵���I�u�W�F�N�g�̎q���̃C���[�W�f�[�^���Q�Ƃ���
            {
                var childrenImageData = this.GetComponentsInChildren<Image>();
                foreach (var image in childrenImageData)
                {
                    Color c = image.color;

                    c.a = 1;

                    image.color = c;
                }
            }
            // �w�肵���I�u�W�F�N�g�̎q���̃e�L�X�g�f�[�^���Q�Ƃ���
            {
                var childrenTextData = this.GetComponentsInChildren<Text>();
                foreach (var text in childrenTextData)
                {
                    Color c = text.color;

                    c.a = 1;

                    text.color = c;
                }
            }
            displayTimer += Time.deltaTime;
            if (displayTimer >= displayTime)
            {
                displayTimer = timer = 0;
            }
        }
    }

    // ��A�N�e�B�u���ɏ�����
    private void OnDisable()
    {
        Initialize();
    }
}
