using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Image spaceTitle;
    public Image spaceResult;

    bool loadOnceTitle = false;
    bool loadOnceGame = false;
    bool loadOnceResult = false;

    // Start is called before the first frame update
    void Start()
    {
        loadOnceTitle = false;
        loadOnceResult = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "TitleScene":
                ButtonPush bpTitle = spaceTitle.GetComponent<ButtonPush>();
                if (Input.GetKeyDown("space"))
                {
                    bpTitle.enabled = true;
                }
                if (bpTitle.GetIsFinish()&& !loadOnceTitle)
                {
                    loadOnceTitle = true;
                    FadeManager.Instance.LoadScene("GameScene", 0.3f);

                    //SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
                    GameSystem.Instance.isClear = false;
                    GameSystem.Instance.isBossActive = false;
                    GameSystem.Instance.GamePosition = 0;
                    GameSystem.Instance.BaseSpeed = 5;
                }
                    break;

            case "GameScene":
                if (PlayerScript.instance.Energy < 0 && !loadOnceGame)
                {
                    FadeManager.Instance.LoadScene("ResultScene", 0.3f);
                    loadOnceGame = true;
                }
                if(GameSystem.Instance.isBossActive)
                {
                    if (GameSystem.Instance.isClear && !loadOnceGame)
                    {
                        FadeManager.Instance.LoadScene("ResultScene", 0.3f);
                        loadOnceGame = true;
                    }
                }
                    break;
               

            case "ResultScene":
                ButtonPush bpResult = spaceResult.GetComponent<ButtonPush>();
                if (Input.GetKeyDown("space"))
                {
                    bpResult.enabled = true;
                }
                if (bpResult.GetIsFinish() && !loadOnceResult)
                {
                    loadOnceResult = true;
                    FadeManager.Instance.LoadScene("TitleScene", 0.3f);
                }
                break;
        }
    }
}
