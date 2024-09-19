using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BackBooster : ItemScript
{
    //�X�e�[�^�X
    [SerializeField] private float speed;
    [SerializeField] private float energy;

    private float offset = 1;

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�擾��
        if (hit)
        {
            transform.position = new Vector3(PlayerScript.instance.transform.position.x - number,
                                              PlayerScript.instance.transform.position.y,
                                              PlayerScript.instance.transform.position.z - offset);
        }
        //�擾���Ă��Ȃ��ꍇ
        else
        {
            //��ʉE���痬��Ă���
            transform.Translate(moveDirection*moveSpeed*Time.deltaTime);
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
                PlayerScript.instance.GetBackBoosters().Add(this);
                number = PlayerScript.instance.GetBackBoosters().Count;
                hit = true;
            }
        }
        
    }

}
