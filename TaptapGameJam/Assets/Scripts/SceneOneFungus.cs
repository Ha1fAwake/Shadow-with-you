using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneOneFungus : MonoBehaviour
{
    public Transform cameraMain;

    public void MoveCameraToTop()
    {
        cameraMain.DOMoveY(0, 0.3f);
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
