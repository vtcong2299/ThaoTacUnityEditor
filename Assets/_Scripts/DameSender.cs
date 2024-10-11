using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameSender : MonoBehaviour
{
    public DameReceiver dameReceiver;
    public int damage = 1;
    private void Start()
    {
        this.dameReceiver = GetComponent<DameReceiver>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        this.ColliderSendDame(collision);
    }
    public virtual void ColliderSendDame(Collision collision)
    {        
        if (dameReceiver != null)
        {
            dameReceiver.Receiver(damage);
        }
        else
        {
            return;
        }
    }
}
   