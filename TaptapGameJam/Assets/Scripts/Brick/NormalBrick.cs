using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBrick : BrickBase
{
    public override void GetCollided()
    {
;       KillManager.Instance.GetKill((int)brickType);
        AudioSource.PlayClipAtPoint(AudioManager.Instance.bubble, transform.position);
        Destroy(gameObject);
    }
    
}
