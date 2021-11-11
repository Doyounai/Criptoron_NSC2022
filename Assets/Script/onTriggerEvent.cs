using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class onTriggerEvent : MonoBehaviour
{
    public List<string> targetTag = new List<string>();
    
    [Space]
    public UnityEvent OnTrigger;
    public UnityEvent ExitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!targetTag.Contains(other.tag))
            return;
        OnTrigger.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!targetTag.Contains(other.tag))
            return;
        ExitTrigger.Invoke();
    }
}
