using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                    SceneManager.LoadScene("Mori", LoadSceneMode.Single);
                }
                break;

            case "Mori":
                if (Input.GetKeyDown("space"))
                {
                    SceneManager.LoadScene("ResultScene", LoadSceneMode.Single);
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
