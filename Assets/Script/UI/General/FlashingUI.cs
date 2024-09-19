using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingUI : MonoBehaviour
{
    [SerializeField, Label("点滅間隔")] private float flashTime;

    [SerializeField, Label("表示時間")] private float displayTime = 0.5f;

    private float timer;

    private float displayTimer;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // 指定したオブジェクトの子供のイメージデータを参照する
        {
            var childrenImageData = this.GetComponentsInChildren<Image>();
            foreach (var image in childrenImageData)
            {
                Color c = image.color;

                c.a = 0;

                image.color = c;
            }
        }
        // 指定したオブジェクトの子供のテキストデータを参照する
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

        // 指定したオブジェクトの子供のイメージデータを参照する
        {
            var childrenImageData = this.GetComponentsInChildren<Image>();
            foreach (var image in childrenImageData)
            {
                Color c = image.color;

                c.a = 0;

                image.color = c;
            }
        }
        // 指定したオブジェクトの子供のテキストデータを参照する
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
            // 指定したオブジェクトの子供のイメージデータを参照する
            {
                var childrenImageData = this.GetComponentsInChildren<Image>();
                foreach (var image in childrenImageData)
                {
                    Color c = image.color;

                    c.a = 1;

                    image.color = c;
                }
            }
            // 指定したオブジェクトの子供のテキストデータを参照する
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

    // 非アクティブ時に初期化
    private void OnDisable()
    {
        Initialize();
    }
}
