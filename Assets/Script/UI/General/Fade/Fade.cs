using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField, Label("ポーズで使うか")] public bool isPause = false;

    [SerializeField, Label("フェードの種類")] public Type fadeType = Type.Out;

    [SerializeField, Label("フェードさせる時間")] public float wateTime = 1.0f; 

    [SerializeField, Label("最大値")] public float maxAlpha = 1;

    [SerializeField, Label("非アクティブの際自動でリセット")] private bool isAutoInitialize = true;

    [SerializeField, Label("処理が完了したら非アクティブ化させるか")] private bool autoActiveOut = false;

    [SerializeField, Label("処理が完了したら削除するか")] private bool isDestroy = false;

    [SerializeField, Label("処理から除外するオブジェクト")] public List<ObjType> objTypes;

    public enum ObjType
    {
        Image,
        Text,
        Sprite,
        RawImage,
        GradationController,
        Max
    }

    // フェードの種類
    public enum Type
    {
        Out,
        In
    }

    private bool isOnes = false;

    // フェード中かどうか
    [SerializeField] private bool isFading = false;

    // イージングタイマー
    [SerializeField] public float et = 0;

    void Start()
    {
        Initialize();
    }

    // 初期化処理
    protected virtual void Initialize()
    {
        // 処理から除外するかどうかを確認
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

        // 指定したオブジェクトの子供のイメージデータを参照する
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
        // 指定したオブジェクトの子供のテキストデータを参照する
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
        // 指定したオブジェクトの子供のスプライトを参照する
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
        // 指定したオブジェクトの子供の特殊イメージを参照する
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

        // 処理から除外するかどうかを確認
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

        // 指定したオブジェクトの子供のイメージデータを参照する
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

        // 指定したオブジェクトの子供のテキストデータを参照する
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

        // 指定したオブジェクトの子供のスプライトデータを参照する
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

        // 指定したオブジェクトの子供のスプライトデータを参照する
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

        // フェードが終わったことを伝える
        isFading = isOnes = true;

        // 自身を削除
        if (isDestroy)
            Destroy(this);

        // 処理が終了したら自身を非アクティブ化して削除する
        if (autoActiveOut)
            this.gameObject.SetActive(false);
    }

    // 非アクティブの際の更新処理
    private void OnDisable()
    {
        if (isAutoInitialize)
        {
            Initialize();
        }
    }

    // タイマーをリセット
    public void ResetTimer() { et = 0; }

    // フェードさせる時間
    public void SetWaitTime(float wateTime) { this.wateTime = wateTime; }

    // フェードの最大値
    public void SetMaxAlpha(float maxAlpha) { this.maxAlpha = maxAlpha; }

    // 非アクティブの際自動でリセットするかどうか
    public void SetIsAutoInitialize(bool isAutoInitialize) { this.isAutoInitialize = isAutoInitialize; }

    // 処理が完了したら削除するか
    public void SetIsDestroy(bool isDestroy) { this.isDestroy = isDestroy; }

    // 処理が完了したら非アクティブ化させるかどうか
    public void SetAutoActiveOut(bool autoActiveOut) { this.autoActiveOut = autoActiveOut; }

    // フェードの種類をセット
    public void SetFadeType(Type fadeType) { this.fadeType = fadeType; }

    // フェードしているかどうか
    public bool GetIsFading() { return isFading ; }

    // ポーズ画面で使用するかどうか
    public void SetIsPause(bool isPause) { this.isPause = isPause; }

    // 機能反転
    public void SetIsReverse() { fadeType = (fadeType == Type.In) ? Type.Out : Type.In; Initialize(); }

    // 処理から除外するオブジェクトを設定
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
