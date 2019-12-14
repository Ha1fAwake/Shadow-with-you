using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Xml;
using Random = UnityEngine.Random;

enum PanelType
{
    Start,
    GamePlay
}

enum curState
{
    OnPad,
    Shooted
}

public class GameManager : TMonoSingleton<GameManager>,IInitializable
{

    //汇总方块信息
    GameObject bricksObject;
    List<BrickBase> brickList = new List<BrickBase>();
    
    //UI
    GameObject canvas;
    GameObject startPanel, endPanel, informationPanel;
    List<GameObject> panelList;

    public int curStage = 1;

    public void Init()
    {
        canvas = GameObject.Find("Canvas");
        DontDestroyOnLoad(canvas);
        panelList = new List<GameObject>();
        FindPanels();
        EventCenter.AddListener<bool>(MyEventType.stageOver, GameOver);
        EventCenter.AddListener<int>(MyEventType.enterStage, GeneStageFromXml);
        EventCenter.AddListener<int>(MyEventType.OpenUI, OpenPanel);
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        AllBricksMoveRepeatly();
    }


    private void OnLevelWasLoaded(int level)
    {
        if(level != 0)
        {
            panelList[0].transform.localPosition = new Vector3(2333, 2333, 0);
            panelList[1].transform.localPosition = Vector3.zero;
        }
    }

    /// <summary>
    /// 控制所有方块的反复横移
    /// </summary>
    private void AllBricksMoveRepeatly()
    {
        foreach(BrickBase b in brickList)
        {
            if(b.ifMove)
            {
                b.transform.Translate(b.moveDir * b.moveSpeed * Mathf.Sign(b.curMoveTime));
                b.curMoveTime++;
                if (b.curMoveTime >= b.moveTime)
                    b.curMoveTime = -b.moveTime;
            }
        }
    }

    /// <summary>
    /// 关卡开始前初始化一些东西
    /// </summary>
    void ResetBeforeStage()
    {

    }
    
    /// <summary>
    /// 根据数字生成关卡
    /// </summary>
    /// <param name="index"></param>
    private void GeneStageFromXml(int index = 0)
    {
        //index = Random.Range(0, 3);
        Debug.Log(index);
        StageData data = LoadByXml(index.ToString());
        GeneStageWithData(data);

        ResetBeforeStage();
    }

    /// <summary>
    /// 根据关卡数据生成关卡
    /// </summary>
    /// <param name="data"></param>
    void GeneStageWithData(StageData data)
    {
        foreach (Transform t in bricksObject.transform)
        {
            Destroy(t.gameObject);
        }

        int num = data.brickTypeList.Count;
        int type;
        Vector3 pos = Vector3.zero;
        Vector3 rotation = Vector3.zero;
        Vector2 scale = Vector3.zero;
        Transform brickTransform;
        for (int i = 0; i < num; i++)
        {
            type = data.brickTypeList[i];
            pos.x = data.brickXPosList[i];
            pos.y = data.brickYPosList[i];
            pos.z = data.brickZPosList[i];
            rotation.x = data.brickXRotationList[i];
            rotation.y = data.brickYRotationList[i];
            rotation.z = data.brickZRotationList[i];
            scale.x = data.brickXScaleList[i];
            scale.y = data.brickYScaleList[i];
            brickTransform = FactoryManager.brickFactory.CreateBrick(type, pos, rotation, scale).transform;
            brickTransform.parent = bricksObject.transform;
            brickList.Add(brickTransform.GetComponent<BrickBase>());
        }
    }

    /// <summary>
    /// 生成一个指定类型的砖块
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="type"></param>
    private void GeneABrick(Vector3 pos, int type)
    {
        GameObject brick = Instantiate(Resources.Load("Prefabs/Bricks/" + type), pos, Quaternion.identity) as GameObject;
        brickList.Add(brick.GetComponent<BrickBase>());
        brick.transform.parent = bricksObject.transform;
    }
    
    /// <summary>
    /// 开始时加载所有的UI面板
    /// </summary>
    void FindPanels()
    {
        panelList = new List<GameObject>();
        Vector3 farAway = new Vector3(2333, 2333, 0);
        foreach (Transform t in canvas.transform)
        {
            panelList.Add(t.gameObject);
            t.localPosition = farAway;
        }
        panelList[0].transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 根据场景编号加载场景
    /// </summary>
    /// <param name="sceneNum"></param>
    void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    /// <summary>
    /// 根据index加载特定UI面板
    /// </summary>
    /// <param name="index"></param>
    public void OpenPanel(int index)
    {
        panelList[index].transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 玩家结束游玩时调用
    /// </summary>
    /// <param name="isWin"></param>
    void GameOver(bool isWin)
    {
        panelList[1].SetActive(true);
        GameObject go = GameObject.Find(panelList[1].name + "/Title");
        if(isWin)
        {
            go.GetComponent<Text>().text = "Congratulations!!!";
        }
        else
        {
            go.GetComponent<Text>().text = "Maybe you need more INFORMATION :(";
        }
    }

    /// <summary>
    /// 隐藏所有UI面板
    /// </summary>
    void SetAllPanelFalse()
    {
        Vector3 far = new Vector3(2333, 2333, 0);
        foreach(GameObject go in panelList)
        {
            go.transform.localPosition = far;
        }
    }

    /// <summary>
    /// 创建存储信息的实体,返回GameObject
    /// </summary>
    StageData CreateStageGO()
    {
        StageData data = new StageData();
        BrickBase curBrick;
        Vector3 rotation;
        foreach (Transform t in bricksObject.transform)
        {
            curBrick = t.GetComponent<BrickBase>();
            rotation = t.rotation.eulerAngles;
            data.brickTypeList.Add((int)curBrick.brickType);
            data.brickXPosList.Add(curBrick.transform.position.x);
            data.brickYPosList.Add(curBrick.transform.position.y);
            data.brickZPosList.Add(curBrick.transform.position.z);
            data.brickXRotationList.Add(rotation.x);
            data.brickYRotationList.Add(rotation.y);
            data.brickZRotationList.Add(rotation.z);
            data.brickXScaleList.Add(curBrick.transform.localScale.x);
            data.brickYScaleList.Add(curBrick.transform.localScale.y);
            //data.brickPosList.Add(curBrick.transform.position);
        }

        return data;
    }
    


    void SaveByXml(string fileName)
    {
        StageData data = CreateStageGO();
        string path = Application.dataPath + "/StreamingAssets/" + fileName + ".xml";

        XmlDocument xmlDoc = new XmlDocument();
        //创建根节点
        XmlElement root = xmlDoc.CreateElement("stagedata");
        root.SetAttribute("name", "stage1");

        XmlElement brick;
        XmlElement brickXPos;
        XmlElement brickYPos;
        XmlElement brickZPos;
        XmlElement brickXRotation;
        XmlElement brickYRotation;
        XmlElement brickZRotation;
        XmlElement brickXScale;
        XmlElement brickYScale;
        XmlElement brickType;
        for (int i=0;i<data.brickTypeList.Count;i++)
        {
            brick = xmlDoc.CreateElement("brick");
            brickXPos = xmlDoc.CreateElement("brickxpos");
            brickYPos = xmlDoc.CreateElement("brickypos");
            brickZPos = xmlDoc.CreateElement("brickzpos");
            brickXPos.InnerText = data.brickXPosList[i].ToString();
            brickYPos.InnerText = data.brickYPosList[i].ToString();
            brickZPos.InnerText = data.brickZPosList[i].ToString();
            brickXRotation = xmlDoc.CreateElement("brickxrotation");
            brickYRotation = xmlDoc.CreateElement("brickyrotation");
            brickZRotation = xmlDoc.CreateElement("brickzrotation");
            brickXRotation.InnerText = data.brickXRotationList[i].ToString();
            brickYRotation.InnerText = data.brickYRotationList[i].ToString();
            brickZRotation.InnerText = data.brickZRotationList[i].ToString();
            brickXScale = xmlDoc.CreateElement("brickxscale");
            brickYScale = xmlDoc.CreateElement("brickyscale");
            brickXScale.InnerText = data.brickXScaleList[i].ToString();
            brickYScale.InnerText = data.brickYScaleList[i].ToString();
            brickType = xmlDoc.CreateElement("bricktype");
            brickType.InnerText = data.brickTypeList[i].ToString();
            
            brick.AppendChild(brickXPos);
            brick.AppendChild(brickYPos);
            brick.AppendChild(brickZPos);
            brick.AppendChild(brickXRotation);
            brick.AppendChild(brickYRotation);
            brick.AppendChild(brickZRotation);
            brick.AppendChild(brickXScale);
            brick.AppendChild(brickYScale);
            brick.AppendChild(brickType);
            root.AppendChild(brick);
        }

        xmlDoc.AppendChild(root);
        xmlDoc.Save(path);
    }

    StageData LoadByXml(string fileName)
    {
        string path = Application.dataPath + "/StreamingAssets/" + fileName + ".xml";

        if (File.Exists(path))
        {
            StageData data = new StageData();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            
            XmlNodeList targets = xmlDoc.GetElementsByTagName("brick");

            if(targets.Count > 0)
            {
                foreach(XmlNode xn in targets)
                {
                    XmlNode brickXPos = xn.ChildNodes[0];
                    XmlNode brickYPos = xn.ChildNodes[1];
                    XmlNode brickZPos = xn.ChildNodes[2];
                    data.brickXPosList.Add(float.Parse(brickXPos.InnerText));
                    data.brickYPosList.Add(float.Parse(brickYPos.InnerText));
                    data.brickZPosList.Add(float.Parse(brickZPos.InnerText));
                    XmlNode brickXRotation = xn.ChildNodes[3];
                    XmlNode brickYRotation = xn.ChildNodes[4];
                    XmlNode brickZRotation = xn.ChildNodes[5];
                    data.brickXRotationList.Add(float.Parse(brickXRotation.InnerText));
                    data.brickYRotationList.Add(float.Parse(brickYRotation.InnerText));
                    data.brickZRotationList.Add(float.Parse(brickZRotation.InnerText));
                    XmlNode brickXScale = xn.ChildNodes[6];
                    XmlNode brickYScale = xn.ChildNodes[7];
                    data.brickXScaleList.Add(float.Parse(brickXScale.InnerText));
                    data.brickYScaleList.Add(float.Parse(brickYScale.InnerText));
                    XmlNode brickType = xn.ChildNodes[8];
                    data.brickTypeList.Add(int.Parse(brickType.InnerText));
                }
            }

            return data;
        }
        else
        {
            Debug.LogError("Xml文件不存在");
            return null;
        }

    }
}
