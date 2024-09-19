using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMove : MonoBehaviour
{
    [SerializeField, Label("‰ñ“]‘¬“x")] private float speed = 1;

    [SerializeField, Label("ŽüŠú")] private float wateTime;

    [SerializeField, Label("‰ñ“]•ûŒü")] private bool isLeft;

    [SerializeField, Label("“¯‚¶•ûŒü‚É‰ñ“]‚µ‘±‚¯‚é‚©")] private bool isDirectionFixed;

    private float timer;

    private float angle;

    // Update is called once per frame
    void Update()
    {

        timer += speed * Time.deltaTime;
        float easePos = Easing.SineOut(timer, wateTime);

        if (!isDirectionFixed)
        {
            // ‰ñ“]UŒ‚ˆ—
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
