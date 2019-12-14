using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : Shadow
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
    private float maxDisToPlayer;
    public float energy = 100;
    private float energyFallSpeed = 0.01f;
    private Transform player;
    private Material material;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Reality/Player").transform;
        material = GetComponent<SpriteRenderer>().material;
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
                transform.Translate(transform.up * 0.1f);
            }
        }
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
        material.SetFloat("_Alpha", 1 - energy * 0.01f);
    }

    /// <summary>
    /// 判断面前是否有可交互物体
    /// </summary>
    private void PhysicCheck()
    {
        Vector3 offset = new Vector3(bodyWidthOffset * transform.right.x, 0, 0);
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
        float y = Input.GetAxis("Vertical");
        rig.velocity = new Vector2(x * moveSpeed * dragRadio, rig.velocity.y);
        
        if(dragRadio == 1)
        {
            if (x > 0)
                transform.eulerAngles = new Vector3(0, 0, 0);
            else if (x < 0)
                transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
