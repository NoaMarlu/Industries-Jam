using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverLogoRenderer : MonoBehaviour
{
    [SerializeField]
    private bool isClear = false;

    public GameObject gameOverLogo;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameOverLogo.SetActive(!GameSystem.Instance.isClear);
    }
}
