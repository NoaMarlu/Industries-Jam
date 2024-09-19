using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalGroupHorizontalMove : MonoBehaviour
{
    [Space(10)]
    [Header("使用する際はアンカーポイントを真ん中にしてください")]

    [SerializeField, NamedArray("移動させたいゲームオブジェクト")] private List<GameObject> gameObjects = new List<GameObject>();

    [SerializeField, Label("初期位置")] private float startPos;

    [SerializeField, Label("終了位置")] private float endPos;

    [SerializeField, Label("終わるまでの時間")] private float waitTime = 10;

    [SerializeField, Label("各種の終わる時間")] private float waitTimes = 10;

    [SerializeField, Label("元からあるコンポーネントを使用するか")] private bool useOriginComponent = false;

    [SerializeField, Label("フェードさせるかどうか")] private bool isFade = false;

    [SerializeField, Label("非アクティブの際自動でリセット")] private bool isAutoInitialize = true;

    // イージングタイマー
    private float et = 0;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // 初期化
    private void Initialize()
    {
        et = 0;
        if (gameObjects == null) return;
        foreach (var gameObject in gameObjects)
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<Transform>().localPosition =
            new Vector3(startPos, gameObject.GetComponent<Transform>().localPosition.y, gameObject.GetComponent<Transform>().localPosition.z);
            if (!useOriginComponent)
            {
                var horizontalMove = gameObject.GetComponent<HorizontalMove>();
                Destroy(horizontalMove);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObjects == null) return;

        et += 1.0f * Time.deltaTime;

        // 指定したオブジェクトの子供の位置データを参照する
        bool[] index = new bool[gameObjects.Count];
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (Mathf.Floor(et / (waitTime / (gameObjects.Count + 1))) >= i)
                index[i] = true;

            var fade = gameObjects[i].GetComponent<Fade>();
            if (!useOriginComponent)
            {
                var horizontalMove = gameObjects[i].GetComponent<HorizontalMove>();

                if (index[i] && !horizontalMove)
                {
                    gameObjects[i].SetActive(true);
                    HorizontalMove childrenMove = gameObjects[i].AddComponent<HorizontalMove>();
                    childrenMove.SetStartPos(startPos);
                    childrenMove.SetEndPos(endPos);
                    childrenMove.SetWaitTime(waitTimes);
                }
            }
            else
            {
                if (index[i])
                {
                    gameObjects[i].SetActive(true);
                }
            }
            if (isFade && !fade)
            {
                Fade addFade = gameObjects[i].AddComponent<Fade>();
                addFade.SetWaitTime(waitTimes);
            }
        }

        const float adJust = 0.9f;
        // 表示されなかった用
        if (et >= waitTimes * gameObjects.Count * adJust) 
        {
            if (!useOriginComponent)
            {
                foreach (var gameObj in gameObjects)
                {
                    gameObj.transform.localPosition = new Vector3(endPos, gameObj.transform.localPosition.y, gameObj.transform.localPosition.z);
                }
            }
        }
    }

    // 非アクティブの際の更新処理
    private void OnDisable()
    {
        if (isAutoInitialize) Initialize();
    }

    public bool Finish() { return (et > waitTime); }

    public void SetGameObject(GameObject addObj) { gameObjects.Add(addObj); }

    public void ClearGameObject() { gameObjects.Clear(); }

    public void SetStartPos(float startPos) { this.startPos = startPos; }

    public void SetEndPos(float endPos) { this.endPos = endPos; }

    public void SetWaitTime(float waitTime) { this.waitTime = waitTime; }

    public void SetWaitTimes(float waitTimes) { this.waitTimes = waitTimes; }

    public void SetIsFade(bool isFade) { this.isFade = isFade; }
}
