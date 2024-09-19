using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMove : MonoBehaviour
{
    [SerializeField, Label("��]���x")] private float speed = 1;

    [SerializeField, Label("����")] private float wateTime;

    [SerializeField, Label("��]����")] private bool isLeft;

    [SerializeField, Label("���������ɉ�]�������邩")] private bool isDirectionFixed;

    private float timer;

    private float angle;

    // Update is called once per frame
    void Update()
    {

        timer += speed * Time.deltaTime;
        float easePos = Easing.SineOut(timer, wateTime);

        if (!isDirectionFixed)
        {
            // ��]�U������
            Quaternion quaternion = new Quaternion();
            quaternion.eulerAngles = (isLeft) ? new Vector3(0, 0, 360 * easePos) : new Vector3(0, 0, -360 * easePos);
            transform.localRotation = quaternion;
        }
        else
        {
            if(isLeft) angle += speed * Time.deltaTime;
            else angle -= speed * Time.deltaTime;
            if (angle >= 360) angle = 0;
            Quaternion quaternion = new Quaternion();
            quaternion.eulerAngles = new Vector3(0, 0, angle);
            transform.localRotation = quaternion;
        }
    }
}
