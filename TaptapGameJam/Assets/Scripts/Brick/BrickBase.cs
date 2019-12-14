using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BrickBase : MonoBehaviour
{
    public BrickType brickType = 0;

    protected int life;

    [SerializeField]
    public float reboundValue;

    //方块反复移动的相关变量
    [SerializeField]
    public bool ifMove = false;
    [SerializeField]
    public int moveTime;
    [SerializeField]
    public int curMoveTime;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public Vector2 moveDir;



    //关卡信息
    public int stageLevel = 0;

    public abstract void GetCollided();
    

}
