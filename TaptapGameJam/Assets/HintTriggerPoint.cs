using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class HintTriggerPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            Flowchart flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
            flowchart.ExecuteBlock("Hint");
        }
    }
}
