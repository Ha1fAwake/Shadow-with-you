using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwitchTwo : SwitchBase
{
    public SpriteRenderer infrared;
    public GameObject infrared_Identity;
    public SpriteRenderer switchTwo;
    public SpriteRenderer switchTwo_Sd;
    public Sprite newSwitch2;
    public Sprite newSwitch2_Sd;
    public float infraredTimer = 1f;

    public override void SwitchFunction()
    {
        switchTwo.sprite = newSwitch2;
        switchTwo_Sd.sprite = newSwitch2_Sd;
        infrared.DOColor(new Color(1, 1, 1, 0), infraredTimer);
        Invoke("DestroyInfrared", infraredTimer);
    }

    private void DestroyInfrared()
    {
        Destroy(infrared_Identity);
    }
}
