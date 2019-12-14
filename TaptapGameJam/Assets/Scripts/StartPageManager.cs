using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPageManager : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void HelpBtn()
    {
        Application.OpenURL("https://shimo.im/docs/6YyG6XCWj6KpWpRG");
    }
}
