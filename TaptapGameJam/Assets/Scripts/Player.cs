﻿using System;
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
    
    private bool isGround, isJumping;
    private Rigidbody2D rig;

    public float minTorchHeight, maxTorchHeight;
    Vector3 curPos, lastPos;
    Transform shadow,torch;
    Rigidbody2D shadowRig;

    // Start is called before the first frame update
    void Start()
    {
        torch = GameObject.Find(name + "/Torch").transform;
        shadow = GameObject.Find("PlayerShadow").transform;
        rig = GetComponent<Rigidbody2D>();
        shadowRig = shadow.GetComponent<Rigidbody2D>();
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
            ControllTorch();
        }
    }
    
    private void ControllTorch()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
        float y = Input.GetAxis("Vertical") * torch.up.y + curPos.y;
        y = Mathf.Clamp(y, minTorchHeight, maxTorchHeight);
        torch.transform.position = new Vector3(curPos.x, y, curPos.z);
    }


    private void UpdateShadowPos()
    {
        curPos = transform.position;
        Vector3 delta = curPos - lastPos;
        Vector3 shadowMoveDelta = new Vector3(delta.x, -delta.y, delta.z);
        shadow.transform.position += shadowMoveDelta;
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
        float y = Input.GetAxis("Vertical");
        rig.velocity = new Vector2(x * moveSpeed, rig.velocity.y);
        if (x > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (x < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
}