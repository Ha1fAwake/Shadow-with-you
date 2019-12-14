using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneOneFungus : MonoBehaviour
{
    public Transform cameraMain;
    public Transform parents;

    public void MoveCameraToTop()
    {
        cameraMain.DOMoveY(0, 0.3f);
    }

    public void ParentsLeave()
    {
        parents.DOScaleX(-1, 0.1f);
        parents.DOMoveX(5.5f,1f);
        Invoke("ParentsDisappear", 1.1f);
    }

    public void BanController()
    {
        EventCenter.Broadcast<bool>(MyEventType.changeController, false);
    }

    public void EnableController()
    {
        EventCenter.Broadcast<bool>(MyEventType.changeController, true);
    }

    private void ParentsDisappear()
    {
        Destroy(parents.gameObject);
    }
}
