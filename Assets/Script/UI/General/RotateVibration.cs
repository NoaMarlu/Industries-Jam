using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateVibration : MonoBehaviour
{
    [SerializeField, Label("óhÇÁÇµãÔçá")] private float swayCondition;

    [SerializeField, Label("óhÇÁÇ∑îºåa")] private float swayRadius;

    private Vector3 setPosition;

    private Vector3 oldPosition;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0) return;

        timer += swayCondition * Time.deltaTime;

        setPosition.x = oldPosition.x + Mathf.Sin(timer) * swayRadius;
        setPosition.y = oldPosition.y + Mathf.Cos(timer) * swayRadius;

        transform.position = setPosition;
    }

    public void SetSwayCondition(float swayCondition) { this.swayCondition = swayCondition; }

    public void SetSwayRadius(float swayRadius) { this.swayRadius = swayRadius; }
}
