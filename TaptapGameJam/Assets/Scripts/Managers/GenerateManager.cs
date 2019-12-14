using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateManager : TMonoSingleton<GenerateManager>, IInitializable
{
    float timer;
    float nextGeneInterval;
    List<GameObject> enemyPrefabList;

    public void Init()
    {
        LoadPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GeneEnemy(Vector3 pos, int index)
    {
        if(enemyPrefabList.Count > index)
        {
            Instantiate(enemyPrefabList[index], pos, Quaternion.identity);
        }
    }

    /// <summary>
    /// 加载全部的敌人
    /// </summary>
    void LoadPrefabs()
    {
        GameObject[] gos = Resources.LoadAll<GameObject>("Prefabs/Enemy");
        enemyPrefabList = new List<GameObject>();
        foreach(GameObject go in gos)
        {
            enemyPrefabList.Add(go);
        }
    }
}
