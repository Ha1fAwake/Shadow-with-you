using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    [SerializeField, Tooltip("最大转弯速度"),Range(0,0.5f)]
    private float rotationSpeed = 0.2f;

    [SerializeField, Tooltip("加速度"), Range(0, 1f)]
    private float acceleratedVeocity = 0.2f;

    [SerializeField, Tooltip("目标")]
    public Transform target = null;        // 目标

    [HideInInspector]
    public float CurrentVelocity = 0.0f;   // 当前速度

    private Rigidbody2D rig;
    private Vector3 targetRotation;
    private AudioSource audioSource = null;   // 音效组件
    private float lifeTime = 0.0f;            // 生命期

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
    }


    private void Update()
    {
        Vector2 selfToTarget = target.position - transform.position;
        Vector2 deltaVelocity = (selfToTarget - rig.velocity * 2f).normalized;
        //rig.AddForce(deltaVelocity * AcceleratedVeocity, ForceMode2D.Force);
        rig.velocity += deltaVelocity * acceleratedVeocity + selfToTarget * acceleratedVeocity;
        //rig.velocity -= Vector3.ProjectOnPlane(rig.velocity, rig.)
        transform.rotation = Quaternion.Euler(0, 0, Global.Vector3ToAngle(selfToTarget));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            EventCenter.Broadcast(MyEventType.PlayerDie);
        }
    }
    
}
