using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    bool isInteracted = false;
    bool canInteract = false;
    GameObject sign;
    public int informationType;
    

    private void Start()
    {
        sign = transform.Find("InteractableSign").gameObject;
        sign.SetActive(false);
    }
    
    public void TriggerItem()
    {
        Debug.Log("Trigger:" + name);
    }

    public void SetInteractableSign(bool active)
    {
        sign.SetActive(active);
    }
}
