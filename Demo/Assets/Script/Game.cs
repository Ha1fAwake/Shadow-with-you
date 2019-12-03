using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    //Time
    const float pausedTimeScale = 0f;
    float playSpeed = 1f;
    //

    

    static Game instance;
    [SerializeField]
    WarFactory warFactory;
    [SerializeField]
    UIManager uiManager;
    [SerializeField]
    EnemyFactory Enemyfactory;
    
    public static Fire SpawnFire()
    {
        Fire f = instance.warFactory.fire;
        return f;
    }

    public static void SpawnEnemy(EnemyFactory factory, EnemyType type)
    {
        
        Enemy enemy = factory.Get(type);
       
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("按下了ESC");
            Time.timeScale =
                Time.timeScale > pausedTimeScale ? pausedTimeScale : playSpeed;
            //弹出UI界面

            //
        }
        else if (Time.timeScale > pausedTimeScale)
        {
            Time.timeScale = playSpeed;
        }
    }

    void EndGame()
    {
        //弹出UI
        Debug.Log("游戏结束");
    }
    private void OnEnable()
    {
        instance = this;
    }


    private void Update()
    {
        PauseGame();
        if(MainActor.IfDie)
        {
            EndGame();
        }
        
        
    }
}
