using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    protected int number = 0;
    protected bool hit = false;
    //アイテムの移動速度
    protected float moveSpeed = 0.0f;
    //移動方向
    protected Vector3 moveDirection = new Vector3(0,1,0);

    //アイテムが流れる速度を決める
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    //画面内生存判定
    public void SurvivalCheck()
    {
        if(transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }

}

