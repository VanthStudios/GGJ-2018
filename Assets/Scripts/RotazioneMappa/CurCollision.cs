using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurCollision : MonoBehaviour
{
    public UnityEvent OnInsertionAllow;
    public UnityEvent OnInsertionDeny;
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Cilindro")
        {
            OnInsertionDeny.Invoke();
        }
    }

    void OnCollisionExit(Collision other)
    {
        OnInsertionAllow.Invoke();
    }
}
