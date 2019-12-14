using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RemoteAssetFactory : IAssetFactory
{
    public UnityEngine.Object LoadAudioClip(string name)
    {
        throw new NotImplementedException();
    }

    public GameObject LoadEffect(string name)
    {
        throw new NotImplementedException();
    }
    

    public GameObject LoadItem(string name)
    {
        throw new NotImplementedException();
    }

    public GameObject LoadBrick(int type)
    {
        throw new NotImplementedException();
    }
}
