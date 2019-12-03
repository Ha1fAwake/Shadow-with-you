using UnityEngine;

public class TargetPoint : MonoBehaviour
{

    public Actor actor { get; private set; }

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

    void Awake()
    {
        actor = transform.root.GetComponent<Actor>();
        Debug.Assert(actor != null, "Target point without Enemy root!", this);
        //collider
        Debug.Assert(
            GetComponent<BoxCollider>() != null,
            "Target point without sphere collider!", this
        );

    }
}