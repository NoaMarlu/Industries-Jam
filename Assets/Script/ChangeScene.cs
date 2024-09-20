using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "TitleScene":
                if (Input.GetKeyDown("space"))
                {
                    SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
                }
                break;

            case "GameScene":
                if (PlayerScript.instance.Energy < 0/*||Boss.instance.BossDie==true*/) 
                {
                    GameSystem.Instance.isClear = false;
                    SceneManager.LoadScene("ResultScene", LoadSceneMode.Single);
                }
                if(Boss.instance.isActive())
                {
                    if (Boss.instance.BossDie)
                    {
                        GameSystem.Instance.isClear = true;
                        SceneManager.LoadScene("ResultScene", LoadSceneMode.Single);
                    }
                }
                    break;

            case "ResultScene":
                if (Input.GetKeyDown("space"))
                {
                    SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
                }
                break;
        }
    }
}
