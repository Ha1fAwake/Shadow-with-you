using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationList : MonoBehaviour
{
    GameObject informationObject,content;

    
    // Start is called before the first frame update
    void Start()
    {
        informationObject = Resources.Load("Prefabs/UI/Information") as GameObject;
        content = GameObject.Find(name + "/Viewport/Content");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AddAnInformation("Test");
        }
    }

    void AddAnInformation(string contentStr)
    {
        GameObject go =  Instantiate(informationObject);
        go.transform.parent = content.transform;
        go.transform.localScale = Vector3.one;
        go.GetComponent<InformationItem>().SetText(contentStr);

    }

    void UpdateInformationList()
    {
        ClearAllInformations();
    }

    private void ClearAllInformations()
    {
        foreach(Transform t in content.transform)
        {
            Destroy(t.gameObject);
        }
    }
}
