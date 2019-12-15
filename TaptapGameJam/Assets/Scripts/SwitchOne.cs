using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwitchOne : SwitchBase
{
    public Transform elevator;
    public Transform elevator_Sd;
    public Transform switchOne;
    public Transform switchOne_Sd;

    public override void SwitchFunction()
    {
        elevator.DOLocalMoveY(1.75f, 2.5f);
        elevator_Sd.DOLocalMoveY(-1.25f, 2.5f);
        switchOne.DOScaleX(-1f, 0.01f);
        switchOne_Sd.DOScaleX(-1f, 0.01f);
    }
}
