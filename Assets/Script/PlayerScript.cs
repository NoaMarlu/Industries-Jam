using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    static public PlayerScript instance;

    public Vector3 ConnectPosition = Vector3.zero;

    public List<BuckBooster> buckBoosters = new List<BuckBooster>();
    static public int BuckBoosterCount;

    private void Awake()
    {
        instance = this;
    }

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
    private bool ShakeBool;//これがtrueだとカメラのxが1ずれる
    public float ShakeTime = 0.3f;
    public float ShakeSizeX = 1;
    public float ShakeSizeY = 1;
    private float ShakeTimer;
    private bool ShakeCount;
    private float ShakeTimerUpdate;
    public float ShakeTimeUpdate=3.0f;


    //Move
    [SerializeField]
    private float SideMove = 10.0f;
    [SerializeField]
    private float LenghtMove = 5.0f;

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
        ShakeTimer= 0;
        ShakeCount = false;
    }
    void Update()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        BuckBooster script = collision.GetComponent<BuckBooster>();
        if (script != null)
        {
            if (buckBoosters.Count <= 0)
            {
                buckBoosters.Add(script);
            }
            
        }
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
        if (ShakeBool== true)
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

    private void OnCollisionStay2D(Collision2D collision)
        {
            if (FlashCount == false)
            {
                if (collision.gameObject.name == "Enemy")
                {
                    Destroy(collision.gameObject);
                    FlashTimer = 0;
                    FlashCount = true;
                ShakeCount = true;
                }
            }//End if
        }//End Collision

    }