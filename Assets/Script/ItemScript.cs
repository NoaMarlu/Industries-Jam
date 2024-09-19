using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    protected int number = 0;
    protected bool hit = false;
    //�A�C�e���̈ړ����x
    protected float moveSpeed = 0.0f;
    //�ړ�����
    protected Vector3 moveDirection = new Vector3(0,1,0);

    //�A�C�e��������鑬�x�����߂�
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    //��ʓ���������
    public void SurvivalCheck()
    {
        if(transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }

}

