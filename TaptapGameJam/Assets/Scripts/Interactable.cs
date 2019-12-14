using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;
using DG.Tweening;

public enum interactType
{
    trigger,
    drag
}

public enum itemType
{
    freezer,
    table,
    fireplace,
    freezerShadow,
    door,
    switch1,
    switch2,
    switch3,
    switch4,
    steel,
    rock
}

public class Interactable : MonoBehaviour
{
    bool isInteracted = false;
    bool canInteract = true;
    GameObject sign;
    public interactType type;
    public itemType itemType;
    public int informationType;

    private void Start()
    {
        sign = transform.Find("InteractableSign").gameObject;
        sign.SetActive(false);
    }
    
    public void TriggerItem()
    {
        switch(itemType)
        {
            case itemType.freezer:
                //触发提示对话
                Flowchart flowchart1 = GameObject.Find("Flowchart").GetComponent<Flowchart>();
                flowchart1.ExecuteBlock("GetCloseToFreezer");
                break;
            case itemType.table:
                //获得火柴
                GamePlayManager.Instance.ifHaveMatch = true;
                canInteract = false;
                break;
            case itemType.fireplace:
                //失去火柴 火焰升起
                if(GamePlayManager.Instance.ifHaveMatch)
                {
                    GamePlayManager.Instance.ifHaveMatch = false;
                    canInteract = false;
                    GamePlayManager.Instance.fire.SetActive(true);
                    //GamePlayManager.Instance.shadow.SetActive(false);
                    Transform noShadow = GameObject.Find("NoShadow").GetComponent<Transform>();
                    noShadow.DOMoveY(-10, 0.5f).SetEase(Ease.Linear);
                    Camera.main.GetComponent<CameraDark>().x = 1f;
                    Flowchart flowchart2 = GameObject.Find("Flowchart").GetComponent<Flowchart>();
                    flowchart2.ExecuteBlock("Fire");
                }
                break;
            case itemType.freezerShadow:
                //获得钥匙
                GamePlayManager.Instance.ifHaveNormalKey = true;
                canInteract = false;
                break;
            case itemType.door:
                //获得钥匙
                if (GamePlayManager.Instance.ifHaveNormalKey)
                {
                    GamePlayManager.Instance.ifHaveNormalKey = false;
                    canInteract = false;
                    //加载第二关
                    SceneManager.LoadScene(2);
                }
                break;
            case itemType.switch1:
                GetComponent<SwitchBase>().SwitchFunction();
                canInteract = false;
                break;
            case itemType.switch2:
                GetComponent<SwitchBase>().SwitchFunction();
                canInteract = false;
                break;
            case itemType.switch3:
                GamePlayManager.Instance.deviceMoveSpeed = 1 - GamePlayManager.Instance.deviceMoveSpeed;
                if(GamePlayManager.Instance.deviceMoveSpeed == 1)
                {
                    Camera.main.transform.position += new Vector3(6, 0, 0);
                }
                break;
            case itemType.switch4:
                GetComponent<SwitchBase>().SwitchFunction();
                canInteract = false;
                break;
            case itemType.steel:
                GamePlayManager.Instance.steel.SetActive(false);
                GamePlayManager.Instance.steel2.SetActive(false);
                break;
            case itemType.rock:

                break;


        }
    }

    public void SetInteractableSign(bool active)
    {
        if(canInteract)
        {
            sign.SetActive(active);
        }
    }
}
