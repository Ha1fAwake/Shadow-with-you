using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTwoDeviceMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(GamePlayManager.Instance.deviceMoveSpeed * 0.002f, 0, 0);
    }
}
