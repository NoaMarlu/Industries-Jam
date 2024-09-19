using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectMovement : MonoBehaviour
{
    [SerializeField, Label("ポーズで使うか")] private bool isPause = false;
    [SerializeField, Label("描画文字")] public string dispText;

    [SerializeField] private GameObject backCenter;
    [SerializeField] private float backCenterWidth;
    [SerializeField] private GameObject backLeft;
    [SerializeField] private float backLeftWidth;
    [SerializeField] private GameObject backRight;
    [SerializeField] private float backTightWidth;
    [SerializeField] private GameObject backText;
    [SerializeField] private GameObject frontText;
    [SerializeField] private GameObject selectUI;

    [SerializeField] private float wateTime = 1;

    public enum State
    {
        None,  
        Intro,
        Overlap,
        Outro
    }

    [SerializeField] private State state = State.None;

    private float timer = 0;

    private bool isSelect = false;

    // Start is called before the first frame update
    void Start()
    {
        var texts = gameObject.GetComponentsInChildren<Text>();
        foreach (var text in texts)
        {
            text.text = dispText;
        }

        // 初期化
        Initialize();
    }

    private void OnDisable()
    {
        // 初期化
        Initialize();
    }

    private void Initialize()
    {
        {
            Fade fade = backText.GetComponent<Fade>();
            Destroy(fade);
            fade = frontText.GetComponent<Fade>();
            Destroy(fade);
        }
        {
            RectTransform rectTransform = backCenter.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(0, rectTransform.sizeDelta.y);
        }
        {
            RectTransform rectTransform = backLeft.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(0, rectTransform.sizeDelta.y);
        }
        {
            RectTransform rectTransform = backRight.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(0, rectTransform.sizeDelta.y);
        }
        {
            Text text = frontText.GetComponent<Text>();
            text.color = new Vector4(text.color.r, text.color.g, text.color.b, 0);
        }
        {
            Text text = backText.GetComponent<Text>();
            text.color = new Vector4(text.color.r, text.color.g, text.color.b, 1);
        }
        if(selectUI) selectUI.SetActive(false);
        timer = 0;
        isSelect = false;
        state = State.None;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        DebugUpdate();
#endif
        switch (state)
        {
            case State.None:
                // 未選択時
                NoneUpdate();
                break;
            case State.Intro:
                // カーソルが上に重なった時の初期反応
                IntroUpdate();
                break;
            case State.Overlap:
                // カーソルが重なり続けている時の反応
                OverlapUpdate();
                break;
            case State.Outro:
                // カーソルが離れた時の反応
                OutroUpdate();
                break;
        }
    }

    private void NoneUpdate()
    {
        if (selectUI) selectUI.SetActive(false);
        Text text = frontText.GetComponent<Text>();
        text.color = new Vector4(text.color.r, text.color.g, text.color.b, 0);

        if (isSelect)
        {
            state = State.Intro;
            if (selectUI) selectUI.SetActive(true);
        }
    }

    private void IntroUpdate()
    {
        if (isPause)
            timer += 1.0f * Time.unscaledDeltaTime;
        else
            timer += 1.0f * Time.deltaTime;

        bool[] nextState = new bool[3];
        float easePos = Easing.ExpoOut(timer, wateTime);
        nextState[0] = timer >= wateTime;
        // 帯の表示制御
        {
            float width = Mathf.Lerp(0, backCenterWidth, easePos);
            RectTransform rectTransform = backCenter.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
        }
        {
            float width = Mathf.Lerp(0, backLeftWidth, easePos);
            RectTransform rectTransform = backLeft.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
        }
        {
            float width = Mathf.Lerp(0, backTightWidth, easePos);
            RectTransform rectTransform = backRight.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
        }
        // テキストの表示制御
        {
            Fade fade = backText.GetComponent<Fade>();
            if (!fade) fade = backText.AddComponent<Fade>();
            fade.SetFadeType(Fade.Type.In);
            fade.SetIsAutoInitialize(false);
            fade.SetWaitTime(wateTime);
            fade.SetIsPause(isPause);
            nextState[1] = fade.GetIsFading();
        }
        {
            Fade fade = frontText.GetComponent<Fade>();
            if (!fade) fade = frontText.AddComponent<Fade>();
            fade.SetFadeType(Fade.Type.Out);
            fade.SetIsAutoInitialize(false);
            fade.SetWaitTime(wateTime);
            fade.SetIsPause(isPause);
            nextState[2] = fade.GetIsFading();
        }
        if (selectUI)
        {
            Fade fade = selectUI.GetComponent<Fade>();
            if (!fade) fade = selectUI.AddComponent<Fade>();
            fade.SetFadeType(Fade.Type.Out);
            fade.SetIsAutoInitialize(false);
            fade.SetWaitTime(wateTime);
            fade.SetIsPause(isPause);
        }

        // 全ての処理が終わるまでせき止め
        foreach (var NextState in nextState)
        {
            if (!NextState) return;
        }

        // 次のステートへの移行準備
        {
            Fade fade = backText.GetComponent<Fade>();
            Destroy(fade);
            fade = frontText.GetComponent<Fade>();
            Destroy(fade);
            if (selectUI)
            {
                fade = selectUI.GetComponent<Fade>();
                Destroy(fade);
            }
        }
        timer = 0;
        state = State.Overlap;
    }

    private void OverlapUpdate()
    {
        if (!isSelect)
        {
            state = State.Outro;
        }
    }

    private void OutroUpdate()
    {
        if (isPause)
            timer += 1.0f * Time.unscaledDeltaTime;
        else
            timer += 1.0f * Time.deltaTime;

        bool[] nextState = new bool[3];
        float easePos = Easing.ExpoOut(timer, wateTime);
        nextState[0] = timer >= wateTime;
        // 帯の表示制御
        {
            float width = Mathf.Lerp(backCenterWidth, 0, easePos);
            RectTransform rectTransform = backCenter.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
            Fade fade = backCenter.GetComponent<Fade>();
            if (!fade) fade = backCenter.AddComponent<Fade>();
            fade.SetFadeType(Fade.Type.In);
            fade.SetIsAutoInitialize(false);
            fade.SetIsPause(isPause);
            fade.SetWaitTime(wateTime * 2);
        }
        {
            float width = Mathf.Lerp(backLeftWidth, 0, easePos);
            RectTransform rectTransform = backLeft.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
        }
        {
            float width = Mathf.Lerp(backTightWidth, 0, easePos);
            RectTransform rectTransform = backRight.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
        }
        // テキストの表示制御
        {
            Fade fade = backText.GetComponent<Fade>();
            if (!fade) fade = backText.AddComponent<Fade>();
            fade.SetFadeType(Fade.Type.Out);
            fade.SetIsAutoInitialize(false);
            fade.SetWaitTime(wateTime);
            fade.SetIsPause(isPause);
            nextState[1] = fade.GetIsFading();
        }
        {
            Fade fade = frontText.GetComponent<Fade>();
            if (!fade) fade = frontText.AddComponent<Fade>();
            fade.SetFadeType(Fade.Type.In);
            fade.SetIsAutoInitialize(false);
            fade.SetWaitTime(wateTime);
            fade.SetIsPause(isPause);
            nextState[2] = fade.GetIsFading();
        }
        if (selectUI)
        {
            Fade fade = selectUI.GetComponent<Fade>();
            if (!fade) fade = selectUI.AddComponent<Fade>();
            fade.SetFadeType(Fade.Type.In);
            fade.SetIsAutoInitialize(false);
            fade.SetWaitTime(wateTime);
            fade.SetIsPause(isPause);
        }

        // 全ての処理が終わるまでせき止め
        foreach (var NextState in nextState)
        {
            if (!NextState) return;
        }

        // 次のステートへの移行準備
        {
            Fade fade = backText.GetComponent<Fade>();
            Destroy(fade);
            fade = frontText.GetComponent<Fade>();
            Destroy(fade);
            fade = backCenter.GetComponent<Fade>();
            Destroy(fade);
            if (selectUI)
            {
                fade = selectUI.GetComponent<Fade>();
                Destroy(fade);
            }
        }
        timer = 0;
        {
            float width = 0;
            RectTransform rectTransform = backCenter.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
        }
        if (selectUI) selectUI.SetActive(false);
        state = State.None;
    }

    public void DebugUpdate()
    {
        var texts = gameObject.GetComponentsInChildren<Text>();
        foreach (var text in texts)
        {
            text.text = dispText;
        }
    }

    public State GetState() { return state; }

    public bool GetIsSelect() { return state == State.Overlap; }

    public void SetIsSelect(bool isSelect) { this.isSelect = isSelect; }

    public void SetIsPause(bool isPause) { this.isPause = isPause; }

    public void SetText(string text) { this.dispText = text; }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SelectMovement))]
public class SelectMovementTextChangeEditor : Editor
{
    private SelectMovement component => target as SelectMovement;

    private void OnSceneGUI()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode) return;
        {
            var texts = component.gameObject.GetComponentsInChildren<Text>();
            foreach (var text in texts)
            {
                text.text = component.dispText;
            }
        }
    }
}
#endif