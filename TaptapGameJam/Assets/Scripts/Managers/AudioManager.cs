using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : TMonoSingleton<AudioManager>, IInitializable
{
    public AudioClip bubble;

    public void Init()
    {
        bubble = Resources.Load<AudioClip>("Audios/Bubble");
    }
    
}
