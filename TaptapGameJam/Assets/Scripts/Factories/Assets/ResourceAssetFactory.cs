using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ResourceAssetFactory : IAssetFactory
{
    private const string brickPath = "Prefabs/Bricks/";
    private const string itemPath = "Prefabs/Item/";
    private const string effectPath = "Prefabs/Effect/";
    private const string audioPath = "Audios/";

    public GameObject LoadBrick(int type)
    {
        return InstantiateGameObject(brickPath + type);
    }

    public UnityEngine.Object LoadAudioClip(string name)
    {
        return LoadAsset(audioPath + name);
    }

    public GameObject LoadEffect(string name)
    {
        return InstantiateGameObject(effectPath + name);
    }


    public GameObject LoadItem(string name)
    {
        return InstantiateGameObject(itemPath + name);
    }

    /// <summary>
    /// 加载需要实例化的资源
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private GameObject InstantiateGameObject(string path)
    {
        UnityEngine.Object o = Resources.Load(path);
        if(o == null)
        {
            Debug.LogError("无法加载资源，路径:" + path);
        }
        return UnityEngine.GameObject.Instantiate(o) as GameObject;
    }

    /// <summary>
    /// 加载不需要实例化的资源
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private UnityEngine.Object LoadAsset(string path)
    {
        UnityEngine.Object o = Resources.Load(path);
        if (o == null)
        {
            Debug.LogError("无法加载资源，路径:" + path);
        }
        return o;
    }
}
