using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTwoSucceed : MonoBehaviour
{
    public Transform cameraMain;
    public Text something;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            cameraMain.DOMoveY(20, 0.5f);
            something.DOText("我们应该可以在这个废弃工厂躲避一段时间吧......", 2.5f);
            Invoke("LoadSceneThree", 1.5f);
        }
    }

    private void LoadSceneThree()
    {
        SceneManager.LoadScene(3);
    }
}
