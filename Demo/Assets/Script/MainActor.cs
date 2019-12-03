using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainActor : Actor {

    static MainActor instance;
    //测试专用
    [SerializeField]
    Enemy enemy;
    //
    [SerializeField]
    SkillTree skillTree;

    Animator animator;
    Collider2D targetPointCollider;

    

    [SerializeField]
    Transform Body;
    [Range(0,100)]
    public int Attack=1;
    [Range(0, 1000)]
    public int Health = 10;
    [Range(0, 1000)]
    public int Defence = 10;

    public static bool IfDie
    {
        get
        {
            if (instance.Health <= 0)
                return true;
            else
                return false;
        }
    }
    [SerializeField]
    float speed = 1f;

     void GetInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("按下了A");
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            Debug.Log(transform.position);
            //动画机控制部分
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("按下了D");
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            Debug.Log(transform.position);

        }
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("按下了W");

        }
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("按下了S");
            

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("按下了空格");

        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("按下了J");
            OnFire();
            //动画机控制部分
        }

    }
    
    public static void GetDamage(int attack)
    {
        int Damage = attack * (1-instance.Defence / (100 + instance.Defence));
        instance.Health -= Damage;
    }

    public void OnFire()
    {
        Vector3 LaunchPoint = transform.position;
        Vector3 TargetPosition = enemy.transform.position;
        //Debug.Log(LaunchPoint);
        Game.SpawnFire().Initialize(LaunchPoint,TargetPosition,5,100);
    }

    private void OnEnable()
    {
        instance = this;
    }

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Update()
    {
        GetInput();
    }
}
