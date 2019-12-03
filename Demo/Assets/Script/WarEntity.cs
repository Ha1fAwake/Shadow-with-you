using UnityEngine;

public abstract class WarEntity : MonoBehaviour
{

    WarFactory originFactory;

    public WarFactory OriginFactory
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

    public  void Recycle()
    {
        originFactory.Reclaim(this);
    }

    public virtual void Update() { }
    
    
        
    
}

