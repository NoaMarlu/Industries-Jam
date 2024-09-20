using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //時間がないためインスタンス化
    static public PlayerScript instance;

    //アイテムのリスト
    private List<BackBooster> backBoosters = new List<BackBooster>();
    private List<UpBooster> upBoosters = new List<UpBooster>();
    private List<DownBooster> downBoosters = new List<DownBooster>();

    //audio
    private AudioSource audioSource;

    //Flash
    private bool FlashCount;//点滅してる間はtrue
    private float FlashSpeed = 0.3f;//点滅が切り替えする時間
    private float FlashTimer;
    private float FlashTimerUpdate;
    public float FlashTimeUpdate = 0.5f;//点滅する時間
    public Renderer FlashObject;

    //Move
    [SerializeField]
    private float SideMove = 10.0f;
    [SerializeField]
    private float LenghtMove = 5.0f;

    //static private int BuckBoosterCount;
    private void Awake()
    {
        //インスタンス生成
        instance = this;
    }

    void Start()
    {
        //Flash
        FlashCount = false;
        FlashTimer = 0;
        FlashTimerUpdate = 0;

        //オーディオ準備
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        FlashTimerUpdate += Time.deltaTime;
        Move();

        if (FlashCount == true)
        {
            Flash();
            if (FlashTimer > FlashTimeUpdate)
            {
                FlashCount = false;
                FlashObject.enabled = true;
            }
        }//End if
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += SideMove * Time.deltaTime * transform.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += LenghtMove * Time.deltaTime * transform.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= SideMove * Time.deltaTime * transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position -= LenghtMove * Time.deltaTime * transform.up;
        }
    }
    void Flash()
    {
        var FlashSpeed = Mathf.Repeat(FlashTimerUpdate, this.FlashSpeed);
        FlashObject.enabled = FlashSpeed >= this.FlashSpeed * 0.5f;
        FlashTimer += Time.deltaTime;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (FlashCount == false)
        {
            if (collision.gameObject.name == "Enemy")
            {
                //オーディオ再生


                Destroy(collision.gameObject);
                FlashTimer = 0;
                FlashCount = true;
            }
        }//End if
    }

    public List<BackBooster> GetBackBoosters()
    {
        return backBoosters;
    }
    public List<UpBooster> GetUpBoosters()
    {
        return upBoosters;
    }
    public List<DownBooster> GetDownBoosters()
    {
        return downBoosters;
    }

    //サウンドを再生する
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}
