using UnityEngine;

[CreateAssetMenu]
public class WarFactory : GameObjectFactory
{

    [SerializeField]
    Fire FirePrefeb;

    public Fire fire => Get(FirePrefeb);

    T Get<T>(T prefab) where T : WarEntity
    {
        T instance = CreateGameObjectInstance(prefab);
        instance.OriginFactory = this;
        return instance;
    }

    public void Reclaim(WarEntity entity)
    {
        Debug.Assert(entity.OriginFactory == this, "Wrong factory reclaimed!");
        Destroy(entity.gameObject);
    }
}