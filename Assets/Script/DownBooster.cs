using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DownBooster : ItemScript
{
    //�X�e�[�^�X
    [SerializeField] private float speed;
    [SerializeField] private float energy;

    private Vector3 offset = new Vector3(-0.3f, -1.3f, 1.0f);

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
                //�v���C���[�̃��X�g�ɓo�^
                PlayerScript.instance.GetDownBoosters().Add(this);
                number = PlayerScript.instance.GetDownBoosters().Count;
                GameSystem.Instance.AddSpeed(speed);
                hit = true;
            }
        }
        
    }

}
