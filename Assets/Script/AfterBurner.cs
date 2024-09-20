using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBurner : MonoBehaviour
{
    static public AfterBurner Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void ResetBuner()
    {
        transform.position -= new Vector3(1, 0, 0);
    }
}
