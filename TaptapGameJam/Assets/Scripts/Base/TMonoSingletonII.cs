using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMonoSingletonII<T> : MonoBehaviour
        where T : Component, IInitializable

{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    // AddComponent will first Awake it
                    obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            (_instance as IInitializable).Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
