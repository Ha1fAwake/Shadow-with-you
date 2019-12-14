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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetAnimation()
    {
        animation.state.AddAnimation(0, "standby", true, 0);
        animation.state.AddAnimation(1, "start walk", false, 0);
    }
}
