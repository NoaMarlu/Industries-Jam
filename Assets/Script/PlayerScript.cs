using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //���Ԃ��Ȃ����߃C���X�^���X��
    static public PlayerScript instance;

    //�A�C�e���̃��X�g
    private List<BackBooster> backBoosters = new List<BackBooster>();
    private List<UpBooster> upBoosters = new List<UpBooster>();
    private List<DownBooster> downBoosters = new List<DownBooster>();

    //audio
    private AudioSource audioSource;

    //Flash
    private bool FlashCount;//�_�ł��Ă�Ԃ�true
    private float FlashSpeed = 0.3f;//�_�ł��؂�ւ����鎞��
    private float FlashTimer;
    private float FlashTimerUpdate;
    public float FlashTimeUpdate = 0.5f;//�_�ł��鎞��
    public Renderer FlashObject;

    //Move
    [SerializeField]
    private float SideMove = 10.0f;
    [SerializeField]
    private float LenghtMove = 5.0f;

    //static private int BuckBoosterCount;
    private void Awake()
    {
        //�C���X�^���X����
        instance = this;
    }

    void Start()
    {
        //Flash
        FlashCount = false;
        FlashTimer = 0;
        FlashTimerUpdate = 0;

        //�I�[�f�B�I����
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
                //�I�[�f�B�I�Đ�


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

    //�T�E���h���Đ�����
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}
