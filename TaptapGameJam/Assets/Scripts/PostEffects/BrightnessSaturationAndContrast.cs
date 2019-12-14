using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessSaturationAndContrast : PostEffectBase
{
    public Shader briSatConShader;
    public Texture tex1,tex2;
    private Material briSatConMaterial;


    public Material material
    {
        get
        {
            briSatConMaterial = CheckShaderAndCreateMaterial(briSatConShader, briSatConMaterial);
            return briSatConMaterial;
        }
    }

    [Range(0f, 0.1f)]
    public float distortFactorTime = 0.5f;

    [Range(0.0f, 1f)]
    public float distortFactor = 0f;

    [Range(0.0f, 50f)]
    public float sampNum = 0.1f;

    [Range(0.0f, 10f)]
    public float testValue = 0.1f;
    public string testValueName = "";

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(material != null)
        {
            //material.SetFloat("_Brightness", brightness);
            //material.SetFloat("_Saturation", saturation);
            //material.SetFloat("_Contrast", contrast);
            material.SetFloat(testValueName, testValue);
            material.SetFloat("_distortFactorTime", distortFactorTime);
            material.SetFloat("_distortFactor", distortFactor);
            material.SetFloat("_SampNum", sampNum);
            material.SetTexture("_NoiseTex1", tex1);
            material.SetTexture("_NoiseTex2", tex2);

            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

}
