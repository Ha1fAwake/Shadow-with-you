using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
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
    
    private bool isGround, isJumping;
    private float handHeight = 0.5f, headHeight = 1f, bodyWidthOffset = 0.26f;
    private Rigidbody2D rig;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GamePlayManager.Instance.isControllingShadow)
        {
            Move();
            Jump();
            PhysicCheck();
            InteractSomeThing();
        }
    }

    private void PhysicCheck()
    {
        Vector3 offset = new Vector3(bodyWidthOffset * transform.right.x, 0, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, transform.right, 0.2f);
        if(hit)
        {
            Debug.Log(hit.collider.name);
            Debug.DrawRay(transform.position + offset, transform.right, Color.red, 0.2f);
        }
        else
        {
            Debug.DrawRay(transform.position + offset, transform.right, Color.green, 0.2f);
        }
    }

    private void InteractSomeThing()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

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
