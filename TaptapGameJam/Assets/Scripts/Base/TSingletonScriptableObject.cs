using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TSingletonScriptableObject<T> : ScriptableObject
        where T : ScriptableObject
{
    protected static T _instance = null;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                T[] assets = Resources.LoadAll<T>("");
                if(assets.Length > 1)
                {
                    Debug.LogWarningFormat("[Base]: Find more than 1 ScriptableObject:'{0}', use the first:'{1}'", typeof(T), assets.FirstOrDefault());
                }
                _instance = assets.FirstOrDefault();
            }
            if(_instance == null)
            {
                throw new MissingComponentException(
                    string.Format("[Base]: Can not find ScriptableObject:'{0}' singleton, please create one first", typeof(T))
                );
            }
            return _instance;
        }
    }
}
