using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : WarEntity {

    Vector3 StartPoint; Vector3 TargetPoint;float speed; int Damage;
    float progress;
    //测试
    Vector3 Distance;

    public void Initialize(Vector3 StartPoint,Vector3 TargetPoint,float speed,int Damage)
    {
        this.StartPoint = StartPoint;
        this.TargetPoint = TargetPoint;
        this.speed = speed;
        this.Damage = Damage;
    }
    private void Start()
    {
        transform.position = StartPoint;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer==9)
        {
            Recycle();
            other.gameObject.GetComponent<Enemy>().GetDamage(Damage);
            Debug.Log("击中目标");

        }
        
    }
    


    public override void Update()
    {
        Distance = TargetPoint - StartPoint;
        transform.position += Distance * Time.deltaTime;
        progress += Time.deltaTime;
        
        if(progress>=2)
        {
            Recycle();
            //Debug.Log("击中目标");
            progress -= 2;
        }
            
        
    }
    

    
}
