using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public float curScale = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent = GamePlayManager.Instance.itemsInLight.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent = GamePlayManager.Instance.shadowWorld;
    }
}
