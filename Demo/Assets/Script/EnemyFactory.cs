using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyFactory : GameObjectFactory
{
    [System.Serializable]
    class EnemyConfig
    {
        public Enemy prefab;
        [Range(100,500)]
        public int Health = 100;
        [Range(1,1000)]
        public int Attack = 10;
        [Range(1,1000)]
        public int Defence = 10;

    }
    [SerializeField]
    EnemyConfig weak = null, strong = null;

    public void Reclaim(Enemy enemy)//销毁函数
    {
        //Debug.Assert(enemy.OriginFactory == this, "Wrong factory reclaimed!");
        Destroy(enemy.gameObject);
    }
    EnemyConfig GetConfig(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Weak: return weak;
            case EnemyType.Strong: return strong;
            
        }
        Debug.Assert(false, "Unsupported enemy type!");
        return null;
    }

    public Enemy Get(EnemyType type = EnemyType.Weak)
    {
        EnemyConfig config = GetConfig(type);
        Enemy instance = CreateGameObjectInstance(config.prefab);
        instance.Initialize(
            config.Health,config.Attack,config.Defence
        
        );
        return instance;
    }

}
