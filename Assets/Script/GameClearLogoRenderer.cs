using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearLogoRenderer : MonoBehaviour
{
    [SerializeField]
    private bool isClear = true;

    public GameObject gameClearLogo;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameClearLogo.SetActive(GameSystem.Instance.isClear);
    }
}
