using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : Shadow
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

    private bool isGround, isJumping, canInteract = false;
    private float handHeight = 0.5f, headHeight = 1f, bodyWidthOffset = 0.26f;
    private Rigidbody2D rig;
    private Interactable interactable = null;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
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
            }
            else
            {
                transform.Translate(transform.up * 0.1f);
            }
        }
    }

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
            interactable.TriggerItem();
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
        rig.velocity = new Vector2(x * moveSpeed, rig.velocity.y);

        if (x > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (x < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
