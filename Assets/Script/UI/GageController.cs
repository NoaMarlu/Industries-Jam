using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GageController : MonoBehaviour
{
    [SerializeField]private Vector3 startPoint;

    [SerializeField] private GameObject oldGageObj;

    [SerializeField] private GameObject gage;

    [SerializeField] private float decreaseSpeed;

    [SerializeField] private float decreaseInterval;

    [SerializeField] private float swayCondition;

    [SerializeField] private float swayRadius;

    [SerializeField] private float maxWidht;

    [SerializeField] private float maxButtery = 100;

    private bool damageEnable;

    private float timer;

    public float decreaseTimer;

    public float currentGage;

    private float oldGage;


    private Vector3 rotatePosition;

    // 他クラスでの呼び出し用(あまり多用しない事)
    public static GageController instance;
    public void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        {
            RectTransform rectTransform = gage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(maxWidht, rectTransform.sizeDelta.y);
        }
        {
            RectTransform rectTransform = oldGageObj.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(maxWidht, rectTransform.sizeDelta.y);
        }
        startPoint = gameObject.transform.position;
        currentGage = maxButtery;
    }

    // Update is called once per frame
    void Update()
    {
        decreaseTimer -= 1.0f * Time.deltaTime;

        // ダメージを受けた時の処理
        if (damageEnable)
        {
            //DamageUpdate();
        }

        float widht = maxWidht * (currentGage / maxButtery);


        if (widht < 0) widht = 0;
        if (widht >= maxWidht) widht = maxWidht;

        // UIに反映
        {
            RectTransform rectTransform = gage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(widht, rectTransform.sizeDelta.y);
        }

        // 直近でダメージを受けていなければ過去のゲージにも反映させる
        if (decreaseTimer < 0)
        {
            RectTransform rectTransform = oldGageObj.GetComponent<RectTransform>();

            float oldWidht = rectTransform.sizeDelta.x - decreaseSpeed;
            rectTransform.sizeDelta = new Vector2(Mathf.Clamp(oldWidht, widht, maxButtery), rectTransform.sizeDelta.y);
        }
    }

    private void DamageUpdate()
    {
        timer += swayCondition * Time.deltaTime;

        rotatePosition.x = startPoint.x + Mathf.Sin(timer) * swayRadius;
        rotatePosition.y = startPoint.y + Mathf.Cos(timer) * swayRadius;

        float wateTime = 1.0f;
        float easePos = Easing.ExpoOut(timer, wateTime);

        // バッテリーバーを揺らす処理
        if (timer < wateTime)
        {
            gameObject.transform.position = new Vector3(Mathf.Lerp(startPoint.x, rotatePosition.x, easePos),
                Mathf.Lerp(startPoint.y, rotatePosition.y, easePos), startPoint.z);
        }
        else
            gameObject.transform.position = rotatePosition;
    }

    private IEnumerator DoDaamge()
    {
        damageEnable = true;
        decreaseTimer = decreaseInterval;

        yield return new WaitForSeconds(0.8f);

        // ダメージを受けた時の演出終了
        damageEnable = false;
        timer = 0;
    }

    // ダメージを受けた時に1回だけ呼び出す
    public void OnDamage(float damage) { damageEnable = true; StartCoroutine(DoDaamge()); currentGage -= damage; }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GageController))]
public class GageControllerEditor : Editor
{
    private GageController component => target as GageController;

    public override void OnInspectorGUI()
    {
        //元のInspector部分を表示
        base.OnInspectorGUI();

        //ボタンを表示
        if (GUILayout.Button("DamageTest"))
        {
            component.OnDamage(10);
        }
    }
}
#endif