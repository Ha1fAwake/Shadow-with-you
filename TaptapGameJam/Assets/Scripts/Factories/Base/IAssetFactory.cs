using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IAssetFactory
{
    GameObject LoadBrick(int type);
    GameObject LoadItem(string name);
    GameObject LoadEffect(string name);
    UnityEngine.Object LoadAudioClip(string name);
}
