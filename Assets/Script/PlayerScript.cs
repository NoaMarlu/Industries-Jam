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
    private bool FlashCount;//ì_ñ≈ÇµÇƒÇÈä‘ÇÕtrue
    private float FlashSpeed = 0.3f;//ì_ñ≈Ç™êÿÇËë÷Ç¶Ç∑ÇÈéûä‘
    private float FlashTimer;
    private float FlashTimerUpdate;
    public float FlashTimeUpdate = 0.5f;//ì_ñ≈Ç∑ÇÈéûä‘
    public Renderer FlashObject;

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
    }
    void Update()
    {
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

        }//End Update
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
                Destroy(collision.gameObject);
                FlashTimer = 0;
                FlashCount = true;
            }
        }//End if
    }

}
