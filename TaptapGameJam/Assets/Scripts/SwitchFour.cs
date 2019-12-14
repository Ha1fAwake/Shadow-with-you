using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFour : SwitchBase
{
    public GameObject ladder;
    public GameObject ladder_Sd;

    public override void SwitchFunction()
    {
        ladder.SetActive(true);
        ladder_Sd.SetActive(true);
    }
}
