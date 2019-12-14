using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    SkeletonAnimation animation;

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<SkeletonAnimation>();
        SetAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            animation.state.SetAnimation(1, "start walk", false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {

        }
        if (Input.GetKeyDown(KeyCode.D))
        {

        }
        if (Input.GetKeyDown(KeyCode.F))
        {

        }
        if (Input.GetKeyDown(KeyCode.G))
        {

        }
    }

    void SetAnimation()
    {
        animation.state.AddAnimation(0, "standby", false,0);
        animation.state.AddAnimation(0, "start walk", false, 0);
        animation.state.AddAnimation(0, "walk", false, 0);
        animation.state.AddAnimation(0, "speedup", false, 0);
        animation.state.AddAnimation(0, "walkfast", false, 0);
    }
}
