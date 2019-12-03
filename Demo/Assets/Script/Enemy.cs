using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    Weak,Strong
}
public class Enemy : Actor {

    int Health=100, Attack=10, Defence=10;

    EnemyFactory originFactory;
    public EnemyFactory OriginFactory
    {
        get
        {
            return originFactory;
        }
        set
        {
            Debug.Assert(originFactory == null, "Redefined origin factory!");
            originFactory = value;
        }
    }

    public void Initialize(int Health, int Attack,int Defence)
    {
        this.Health = Health;
        this.Attack = Attack;
        this.Defence = Defence;
    }
    public void GetDamage(float attack)//伤害结算机制很不对
    {
        float Damage = attack * (1-Defence / (100 + Defence));
        print(Damage);

        Health = Health - (int)Damage;
    }
    public void ExamDie()
    {
        if(Health <= 0)
        {
            //OriginFactory.Reclaim(this);
            Destroy(gameObject);
            //播放相应动画
        }

    }

    private void Awake()
    {
        
    }

    public override void Update()//Enemy的更新
    {
        ExamDie();
    }
}
