using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    [SerializeField] private float speed = 1.0f;

    [SerializeField] private State state;

    //private Fade fade;

    private enum State
    {
        None,
        FadeIn,
        FadeOut
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.FadeIn: UpdateFadeIn(); break;
            case State.FadeOut: UpdateFadeOut(); break;
        }
    }

    public void FadeIn()
    {
        // �t�F�[�h�C��
        state = State.FadeIn;
        fadeImage.gameObject.SetActive(true);
    }

    public void FadeOut()
    {
        // �t�F�[�h�A�E�g�J�n
        state = State.FadeOut;
        fadeImage.gameObject.SetActive(true);
    }

    public bool IsFading()
    {
        // �t�F�[�h����
        return state != State.None;
    }

    private void UpdateFadeIn()
    {
        // �t�F�[�h�C���X�V����
        //AddFadeComponent();
        //fade.SetFadeType(Fade.Type.In);

        //if (fade.GetIsFading())
        //{
        //    state = State.None;
        //    fade.SetFadeType(Fade.Type.Out);
        //    fadeImage.gameObject.SetActive(false);
        //}

        Color color = fadeImage.color;
        color.a -= speed * Time.unscaledDeltaTime;
        if (color.a <= 0.0f)
        {
            color.a = 0.0f;
            state = State.None;
            fadeImage.gameObject.SetActive(false);
        }
        fadeImage.color = color;
    }

    private void UpdateFadeOut()
    {
        //// �t�F�[�h�A�E�g�X�V����
        //AddFadeComponent();
        //fade.SetFadeType(Fade.Type.Out);
        //if (fade.GetIsFading())
        //{
        //    state = State.None;
        //    Destroy(fade);
        //}

        Color color = fadeImage.color;
        color.a += speed * Time.unscaledDeltaTime;
        if (color.a >= 1.0f)
        {
            color.a = 1.0f;
            state = State.None;
        }
        fadeImage.color = color;
    }

    //private void AddFadeComponent()
    //{
    //    fade = fadeImage.gameObject.GetComponent<Fade>();
    //    if (!fade) fade = fadeImage.gameObject.AddComponent<Fade>();
    //    fade.SetWaitTime(speed);
    //}
}
