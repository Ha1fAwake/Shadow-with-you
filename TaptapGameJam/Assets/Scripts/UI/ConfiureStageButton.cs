using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomEditor(typeof(ConfiureStage))]
public class ConfiureStageButton : Editor
{
    ConfiureStage cs;

    private void OnEnable()
    {
        cs = target as ConfiureStage;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        int num = 0;

        if(GUILayout.Button("生成关卡按钮"))
        {
            cs.GeneStageButton();
        }
    }
    
    
}
