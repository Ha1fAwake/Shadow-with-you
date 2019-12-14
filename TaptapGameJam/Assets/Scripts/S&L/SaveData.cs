using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData
{
    public List<int> brickTypeList = new List<int>();
    public List<float> brickXPosList = new List<float>();
    public List<float> brickYPosList = new List<float>();
    public List<float> brickZPosList = new List<float>();
    public List<float> brickXRotationList = new List<float>();
    public List<float> brickYRotationList = new List<float>();
    public List<float> brickZRotationList = new List<float>();
    public List<float> brickXScaleList = new List<float>();
    public List<float> brickYScaleList = new List<float>();
    public List<int> brickIfMoveList = new List<int>();
    public List<int> brickMoveTimeList = new List<int>();
    public List<float> brickMoveSpeedList = new List<float>();
    public List<float> brickXMoveDirList = new List<float>();
    public List<float> brickYMoveDirList = new List<float>();
    //public List<Vector3> brickPosList = new List<Vector3>();
}
