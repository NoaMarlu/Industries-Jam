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
    public AudioClip hitclip;

    public float Energy = 100;
    public float DisEnergyPalam = 20;
    //Flash
    private bool FlashCount;//点滅してる間はtrue
    private float FlashSpeed = 0.3f;//点滅が切り替えする時間
    private float FlashTimer;
    private float FlashTimerUpdate;
    public float FlashTimeUpdate = 0.5f;//点滅する時間
    public Renderer FlashObject;
    //Stop
    private Vector2 Wall1;
    private Vector2 Wall2;
    public float Radius = 1.0f;
    //Shake
    private GameObject CameraObject;
    private Vector3 CameraPos;
    private bool ShakeBool;//これがtrueだとカメラのxが1ずれる
    public float ShakeTime = 0.3f;
    public float ShakeSizeX = 1;
    public float ShakeSizeY = 1;
    private float ShakeTimer;
    private bool ShakeCount;
    private float ShakeTimerUpdate;
    public float ShakeTimeUpdate = 3.0f;


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
        //Shake
        CameraObject = GameObject.Find("Main Camera");
        ShakeTimerUpdate = 0;
        ShakeBool = false;
        ShakeTimer = 0;
        ShakeCount = false;

        //オーディオ準備
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        SpeedController.instance.SetSpeed((int)GameSystem.Instance.BaseSpeed);
        FlashTimerUpdate += Time.deltaTime;
        Move();
        Stop();
        //Shake
        if (ShakeCount == true)
        {
            ShakeTimerUpdate += Time.deltaTime;
            Shake();
            if (ShakeTimerUpdate > ShakeTimeUpdate)
            {
                ShakeTimerUpdate = 0;
                ShakeCount = false;
                CameraObject.transform.position = CameraPos;
            }
        }

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
    void Stop()
    {

        Wall1 = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Wall2 = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));

        if (transform.position.x + Radius > Wall1.x)
            transform.position = new Vector2(Wall1.x - Radius, transform.position.y);
        if (transform.position.y + Radius > Wall1.y)
            transform.position = new Vector2(transform.position.x, Wall1.y - Radius);
        if (transform.position.x - Radius < Wall2.x)
            transform.position = new Vector2(Wall2.x + Radius, transform.position.y);
        if (transform.position.y - Radius < Wall2.y)
            transform.position = new Vector2(transform.position.x, Wall2.y + Radius);



    }
    void Shake()
    {
        if (ShakeBool == false)
        {
            CameraObject.transform.position = new Vector3(ShakeSizeX, ShakeSizeY, -10);
            ShakeTimer += Time.deltaTime;
            if (ShakeTimer > ShakeTime)
            {
                ShakeTimer = 0;
                ShakeBool = true;
            }
        }
        if (ShakeBool == true)
        {
            CameraObject.transform.position = new Vector3(0, 0, -10);
            ShakeTimer += Time.deltaTime;
            if (ShakeTimer > ShakeTime)
            {
                ShakeTimer = 0;
                ShakeBool = false;
            }
        }
    }//End void
    public void OnDamage()
    {
        //オーディオ再生
        PlaySound(hitclip);
        FlashTimer = 0;
        FlashCount = true;
        ShakeCount = true;
        CameraPos= CameraObject.transform.position;
        Energy -= DisEnergyPalam;
        GageController.instance.OnDamage(DisEnergyPalam);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (FlashCount == false)
        {
            if (collision.gameObject.name == "Enemy")
            {
                //オーディオ再生
                PlaySound(hitclip);

                Destroy(collision.gameObject);
                FlashTimer = 0;
                FlashCount = true;
            }
        }//End if
        if (collision.gameObject.name == "item")
        {
            //Energy += DisEnergyPalam;
        }
    }//End Collision

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
