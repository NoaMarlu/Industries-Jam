using UnityEngine;

public class BackBooster : ItemScript
{
    //audio
    public AudioClip collectedClip;
    protected AudioSource audioSource;

    //ステータス
    [SerializeField] private float speed;
    [SerializeField] private float energy;

    private float offset = 0.5f;

    private void Start()
    {
        //オーディオ準備
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤー取得時
        if (hit)
        {
            transform.position = new Vector3(PlayerScript.instance.transform.position.x - number * offset,
                                              PlayerScript.instance.transform.position.y,
                                              PlayerScript.instance.transform.position.z - offset);
        }
        //取得していない場合
        else
        {
            //画面右から流れてくる
            transform.Translate(moveDirection*moveSpeed*Time.deltaTime);
        }

        //画面内生存判定
        SurvivalCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hit)
        {
            //プレイヤーに当たると
            PlayerScript script = collision.GetComponent<PlayerScript>();

            if (script != null)
            {
                //オーディオ再生
                audioSource.PlayOneShot(collectedClip);

                //プレイヤーのリストに登録
                PlayerScript.instance.GetBackBoosters().Add(this);
                AfterBurner.Instance.ResetBuner();
                number = PlayerScript.instance.GetBackBoosters().Count;
                GameSystem.Instance.AddSpeed(speed);
                hit = true;
                GameSystem.Instance.ItemGet = true;
            }
        }
        
    }

}
