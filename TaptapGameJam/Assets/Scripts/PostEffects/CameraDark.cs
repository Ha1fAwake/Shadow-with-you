using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDark : PostEffectBase
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

    [Range(0f, 1f)]
    public float x = 0.5f;

    [Range(0.0f, 1f)]
    public float y = 0f;

    [Range(0.0f, 1f)]
    public float z = 0.1f;

    [Range(0.0f, 1f)]
    public float a = 0.1f;
    public string testValueName = "";

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(material != null)
        {
            material.SetFloat("_XColor", x);
            material.SetFloat("_YColor", x);
            material.SetFloat("_ZColor", x);
            material.SetFloat("_AColor", a);
            //material.SetFloat("_Saturation", saturation);
            //material.SetFloat("_Contrast", contrast);

            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

}
