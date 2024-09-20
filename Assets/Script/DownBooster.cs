using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DownBooster : ItemScript
{
    //audio
    public AudioClip collectedClip;
    protected AudioSource audioSource;

    //�X�e�[�^�X
    [SerializeField] private float speed;
    [SerializeField] private float energy;

    private Vector3 offset = new Vector3(-0.3f, -1.3f, 1.0f);

    private void Start()
    {
        //�I�[�f�B�I����
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�擾��
        if (hit)
        {
            transform.position = new Vector3(PlayerScript.instance.transform.position.x - number + offset.x,
                                              PlayerScript.instance.transform.position.y + offset.y,
                                              PlayerScript.instance.transform.position.z + offset.z);
        }
        //�擾���Ă��Ȃ��ꍇ
        else
        {
            //��ʉE���痬��Ă���
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        //��ʓ���������
        SurvivalCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hit)
        {
            //�v���C���[�ɓ������
            PlayerScript script = collision.GetComponent<PlayerScript>();

            if (script != null)
            {
                //�I�[�f�B�I�Đ�
                audioSource.PlayOneShot(collectedClip);

                //�v���C���[�̃��X�g�ɓo�^
                PlayerScript.instance.GetDownBoosters().Add(this);
                number = PlayerScript.instance.GetDownBoosters().Count;
                GameSystem.Instance.AddSpeed(speed);
                hit = true;
            }
        }
        
    }

}
