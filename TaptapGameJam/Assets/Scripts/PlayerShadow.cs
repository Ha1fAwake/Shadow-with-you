using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;
    public float dragRadio = 1;
    public float checkRadius;
    public int howManyJump;
    public Transform feetPos;
    public LayerMask whatIsGround;

    public float jumpTime;
    private float jumpTimeCounter;

    private bool isJumpingFirstTime;
    private int curJumpNum = 0;

    private bool isGround, isJumping, canInteract = false;
    private float handHeight = 0.5f, headHeight = 1f, bodyWidthOffset = 0.26f;
    private Rigidbody2D rig;
    private FixedJoint2D fixedJoint;
    private Interactable interactable = null;
    private Transform curDragItem;
    private Rigidbody2D curDragRig;

    //能量相关
    private float maxDisToPlayer = 5;
    public float energy = 100;
    private float energyFallSpeed = 0.05f;
    private Transform player;
    private Material material, material2, material3;

    //骨骼动画相关
    SkeletonAnimation animation;
    string curAnimaState = "standby";


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Reality/Player").transform;
        SetOpposedPosToPlayer();
        material = GetComponent<MeshRenderer>().materials[0];
        material2 = GetComponent<MeshRenderer>().materials[1];
        material3 = GetComponent<MeshRenderer>().materials[2];
        animation = GetComponent<SkeletonAnimation>();
        EventCenter.AddListener<int>(MyEventType.playerchangeanime, ChangeAnime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GamePlayManager.Instance.isSpiningStage)
        {
            if (GamePlayManager.Instance.isControllingShadow)
            {
                Move();
                Jump();
                PhysicCheck();
                InteractSomeThing();
                CalcEnergy();
            }
            else
            {
                transform.Translate(transform.up * 0.001f);
            }
        }
    }
    
    private void SetOpposedPosToPlayer()
    {
        float y = -player.position.y;
        transform.position = new Vector3(player.position.x, y, player.position.z);
    }

    /// <summary>
    /// 根据影子与本体之间的距离计算能量
    /// </summary>
    private void CalcEnergy()
    {
        if(Vector3.Distance(transform.position,player.position) > maxDisToPlayer)
        {
            energy -= energyFallSpeed;
        }
        else
        {
            energy = 100;
        }
        material.SetFloat("_Alpha", energy * 0.01f);
        material2.SetFloat("_Alpha", energy * 0.01f);
        material3.SetFloat("_Alpha", energy * 0.01f);
    }

    /// <summary>
    /// 判断面前是否有可交互物体
    /// </summary>
    private void PhysicCheck()
    {
        Vector3 offset = new Vector3(bodyWidthOffset * transform.right.x, 0.6f, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, transform.right, 0.3f, 1 << LayerMask.NameToLayer("Interactable"));
        if(hit)
        {
            interactable = hit.transform.GetComponent<Interactable>();
            interactable.SetInteractableSign(true);
            canInteract = true;
            Debug.DrawRay(transform.position + offset, transform.right, Color.red, 0.15f);
        }
        else
        {
            if(interactable != null)
            {
                interactable.SetInteractableSign(false);
            }
            canInteract = false;
            Debug.DrawRay(transform.position + offset, transform.right, Color.green, 0.15f);
        }
    }

    private void InteractSomeThing()
    {
        if (Input.GetKeyDown(KeyCode.F) && canInteract)
        {
            interactable.SetInteractableSign(false);
            switch (interactable.type)
            {
                case interactType.drag:
                    curDragItem = interactable.transform;
                    curDragItem.parent = transform;
                    curDragRig = curDragItem.GetComponent<Rigidbody2D>();
                    curDragRig.constraints = RigidbodyConstraints2D.FreezeRotation;
                    fixedJoint = curDragItem.GetComponent<FixedJoint2D>();
                    fixedJoint.connectedBody = rig;
                    canInteract = false;
                    dragRadio = 0.3f;
                    break;
                case interactType.trigger:
                    interactable.TriggerItem();
                    break;
                default:
                    break;
            }
        }
        if (Input.GetKeyUp(KeyCode.F) && curDragItem != null)
        {
            curDragItem.transform.parent = GamePlayManager.Instance.shadowWorld;
            curDragRig.constraints = RigidbodyConstraints2D.FreezeAll;
            fixedJoint.connectedBody = null;
            curDragItem = null;
            dragRadio = 1;
        }
    }

    void Jump()
    {
        isGround = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGround && Input.GetKeyDown(KeyCode.Space))//跳一下
        {
            curJumpNum = howManyJump - 1;
            rig.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }
        else if (!isGround && curJumpNum > 0 && Input.GetKeyDown(KeyCode.Space))//跳一下
        {
            curJumpNum--;
            rig.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rig.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float xInput = Mathf.Abs(x);
        float y = Input.GetAxis("Vertical");
        rig.velocity = new Vector2(x * moveSpeed * dragRadio, rig.velocity.y);
        ChangeAnimaState(xInput);

        if (dragRadio == 1)
        {
            if (x > 0)
                transform.eulerAngles = new Vector3(0, 0, 0);
            else if (x < 0)
                transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    /// <summary>
    /// 根据当前关卡和X轴输入改变动画
    /// </summary>
    /// <param name="xInput"></param>
    void ChangeAnimaState(float xInput)
    {
        if (xInput == 0 && curAnimaState != "standby")
        {
            curAnimaState = "standby";
            animation.state.SetAnimation(0, "standby", true);
        }
        switch (GameManager.Instance.curStage)
        {
            case 1:
                if (xInput > 0 && xInput < 0.5f && curAnimaState != "walk")
                {
                    curAnimaState = "walk";
                    animation.state.SetAnimation(0, "walk", true);
                }
                else if (xInput > 0.5f && curAnimaState != "walkfast")
                {
                    curAnimaState = "walkfast";
                    animation.state.SetAnimation(0, "walkfast", true);
                }
                break;
            case 2:
                if (xInput > 0 && xInput < 0.5f && curAnimaState != "walk")
                {
                    curAnimaState = "walk";
                    animation.state.SetAnimation(0, "walkcarefull", true);
                }
                else if (xInput > 0.5f && curAnimaState != "walkfast")
                {
                    curAnimaState = "walkfast";
                    animation.state.SetAnimation(0, "walkfast", true);
                }
                break;
            case 3:
                if (xInput > 0 && xInput < 0.5f && curAnimaState != "walk")
                {
                    curAnimaState = "walk";
                    animation.state.SetAnimation(0, "walkcarefull", true);
                }
                else if (xInput > 0.5f && curAnimaState != "walkfast")
                {
                    curAnimaState = "walkfast";
                    animation.state.SetAnimation(0, "walkfast", true);
                }
                break;
            case 4:
                if (xInput > 0 && curAnimaState != "walk")
                {
                    curAnimaState = "walk";
                    animation.state.SetAnimation(0, "walkold", true);
                }
                break;
            default:
                break;
        }
    }

    void ChangeAnime(int index)
    {
        switch(index)
        {
            case 0:
                animation.state.SetAnimation(0, "standby", true);
                break;
            case 1:
                animation.state.SetAnimation(0, "walk", true);
                break;
            case 2:
                animation.state.SetAnimation(0, "walkfast", true);
                break;
            case 3:
                animation.state.SetAnimation(0, "walkcarefull", true);
                break;
            case 4:
                animation.state.SetAnimation(0, "walkold", true);
                break;
        }
    }
}
