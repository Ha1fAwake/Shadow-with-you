using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PostEffectBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CheckResources();
    }
    

    protected Material CheckShaderAndCreateMaterial(Shader shader,Material material)
    {
        if(shader == null)
        {
            return null;
        }

        if(shader.isSupported && material && material.shader == shader)
        {
            return material;
        }

        if(!shader.isSupported)
        {
            Debug.LogError("Shader Not Support");
            return null;
        }
        else
        {
            material = new Material(shader);
            material.hideFlags = HideFlags.DontSave;
            if (material)
                return material;
            else
                return null;
        }
    }

    protected void CheckResources()
    {
        if(!CheckSupport())
        {
            NotSupport();
        }
    }

    protected bool CheckSupport()
    {
        if(SystemInfo.supportsImageEffects == false || SystemInfo.supportsRenderTextures == false)
        {
            Debug.LogError("This platform do not support image effect");
            return false;
        }
        return true;
    }

    protected void NotSupport()
    {
        enabled = false;
    }
}
