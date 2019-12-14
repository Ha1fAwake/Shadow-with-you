using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GetCloseToFreezer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Flowchart flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
            flowchart.ExecuteBlock("GetCloseToFreezer");
        }
    }
}
