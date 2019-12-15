using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneTwoFungus : MonoBehaviour
{
    public Transform dark;

    public void MoveCameraToTop()
    {
        dark.DOMoveY(20, 0.3f);
    }

    public void BanController()
    {
        EventCenter.Broadcast<bool>(MyEventType.changeController, false);
    }

    public void EnableController()
    {
        EventCenter.Broadcast<bool>(MyEventType.changeController, true);
    }
}
