using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;
    public float checkRadius;
    public int howManyJump;
    public Transform feetPos;
    public LayerMask whatIsGround;

    public float jumpTime;
    private float jumpTimeCounter;

    private bool isJumpingFirstTime;
    private int curJumpNum = 0;
    
    private bool isGround, isJumping, isUsingTorch = false, canInteract = false;
    private Rigidbody2D rig;
    private Interactable interactable = null;

    public float minTorchHeight, maxTorchHeight, bodyWidthOffset = 0.26f;
    Vector3 curPos, lastPos;
    Transform shadow,torch;
    Rigidbody2D shadowRig;

    string curAnimaState = "standby";
    SkeletonAnimation animation;


    // Start is called before the first frame update
    void Start()
    {
        torch = GameObject.Find(name + "/Torch").transform;
        torch.gameObject.SetActive(false);
        shadowRig = GamePlayManager.Instance.shadowRig;
        shadow = shadowRig.transform;
        rig = GetComponent<Rigidbody2D>();
        animation = GetComponent<SkeletonAnimation>();
        curPos = transform.position;
        lastPos = transform.position;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GamePlayManager.Instance.isControllingShadow && !GamePlayManager.Instance.isSpiningStage)
        {
            UpdateShadowPos();
            Move();
            Jump();
            PhysicCheck();
            InteractSomeThing();
            ControllTorch();
        }
    }

    /// <summary>
    /// 判断面前是否有可交互物体
    /// </summary>
    private void PhysicCheck()
    {
        Vector3 offset = new Vector3(bodyWidthOffset * transform.right.x, 0.6f, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, transform.right, 0.3f, 1 << LayerMask.NameToLayer("Interactable"));
        if (hit)
        {
            interactable = hit.transform.GetComponent<Interactable>();
            interactable.SetInteractableSign(true);
            canInteract = true;
            Debug.DrawRay(transform.position + offset, transform.right, Color.red, 0.15f);
        }
        else
        {
            if (interactable != null)
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
                case interactType.trigger:
                    interactable.TriggerItem();
                    break;
                default:
                    break;
            }
        }
    }

    private void ControllTorch()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GamePlayManager.Instance.isUsingTorch = !GamePlayManager.Instance.isUsingTorch;
            torch.gameObject.SetActive(!torch.gameObject.active);
        }
        if(GamePlayManager.Instance.isUsingTorch)
        {
            float inputY = 0.03f;
            if (Input.GetKey(KeyCode.W) && torch.localPosition.y <= maxTorchHeight)
            {
                torch.Translate(transform.up * inputY);
            }
            if (Input.GetKey(KeyCode.S) && torch.localPosition.y >= minTorchHeight)
            {
                inputY *= -1;
                torch.Translate(transform.up * inputY);
            }
        }
    }

    private void UpdateShadowPos()
    {
        curPos = transform.position;
        Vector3 delta = curPos - lastPos;
        Vector3 shadowMoveDelta = new Vector3(delta.x, -delta.y, delta.z);
        shadow.position += shadowMoveDelta;
        lastPos = curPos;
    }

    void Jump()
    {
        isGround = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            curJumpNum = howManyJump - 1;
            rig.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }
        else if (!isGround && curJumpNum > 0 && Input.GetKeyDown(KeyCode.Space))
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
        rig.velocity = new Vector2(x * moveSpeed, rig.velocity.y);
        ChangeAnimaState(xInput);
        if (x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            shadow.transform.eulerAngles = new Vector3(0, 180, 180);
        }
        else if (x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            shadow.transform.eulerAngles = new Vector3(0, 0, 180);
        }
    }

    /// <summary>
    /// 根据当前关卡和X轴输入改变动画
    /// </summary>
    /// <param name="xInput"></param>
    void ChangeAnimaState(float xInput)
    {
        if(xInput == 0 && curAnimaState != "standby")
        {
            curAnimaState = "standby";
            animation.state.SetAnimation(0, "standby", true);
            EventCenter.Broadcast<int>(MyEventType.playerchangeanime, 0);
        }
        switch(GameManager.Instance.curStage)
        {
            case 1:
                if (xInput > 0 && xInput < 0.5f && curAnimaState != "walk")
                {
                    curAnimaState = "walk";
                    animation.state.SetAnimation(0, "walk", true);
                    EventCenter.Broadcast<int>(MyEventType.playerchangeanime, 1);
                }
                else if (xInput > 0.5f && curAnimaState != "walkfast")
                {
                    curAnimaState = "walkfast";
                    animation.state.SetAnimation(0, "walkfast", true);
                    EventCenter.Broadcast<int>(MyEventType.playerchangeanime, 2);
                }
                break;
            case 2:
                if (xInput > 0 && xInput < 0.5f && curAnimaState != "walk")
                {
                    curAnimaState = "walk";
                    animation.state.SetAnimation(0, "walkcarefull", true);
                    EventCenter.Broadcast<int>(MyEventType.playerchangeanime, 1);
                }
                else if (xInput > 0.5f && curAnimaState != "walkfast")
                {
                    curAnimaState = "walkfast";
                    animation.state.SetAnimation(0, "walkfast", true);
                    EventCenter.Broadcast<int>(MyEventType.playerchangeanime, 2);
                }
                break;
            case 3:
                if (xInput > 0 && xInput < 0.5f && curAnimaState != "walk")
                {
                    curAnimaState = "walk";
                    animation.state.SetAnimation(0, "walkcarefull", true);
                    EventCenter.Broadcast<int>(MyEventType.playerchangeanime, 1);
                }
                else if (xInput > 0.5f && curAnimaState != "walkfast")
                {
                    curAnimaState = "walkfast";
                    animation.state.SetAnimation(0, "walkfast", true);
                    EventCenter.Broadcast<int>(MyEventType.playerchangeanime, 2);
                }
                break;
            case 4:
                if (xInput > 0 && curAnimaState != "walk")
                {
                    curAnimaState = "walk";
                    animation.state.SetAnimation(0, "walkold", true);
                    EventCenter.Broadcast<int>(MyEventType.playerchangeanime, 1);
                }
                break;
            default:
                break;
        }
    }
}
