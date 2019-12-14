using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSignMove : MonoBehaviour
{
    int sign = 1;
    float radio = 0;
    float moveSpeed = 0.1f;
    public Vector3 startPos = new Vector3(0, -0.75f, 0);
    public Vector3 offset = new Vector3(0, -0.15f, 0);
    // Update is called once per frame
    void Update()
    {
        radio += moveSpeed;
        if(radio >= 1.01f)
        {
            sign = -sign;
            radio = 0;
        }
        transform.localPosition = Vector3.Lerp(startPos + sign * offset, startPos - sign * offset, radio);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
