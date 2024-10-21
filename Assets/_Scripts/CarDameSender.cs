using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarDameSender : DameSender
{   
    protected CarDameReceiver receiver;
    private void Start()
    {
        damage = 10;
        this.receiver = GetComponent<CarDameReceiver>();       
    }
    public override void ColliderSendDame(Collision collision)
    {
            base.ColliderSendDame(collision);
            this.receiver.Receiver(damage);
            Debug.Log("Bi Dam Roi, Xe Hong Bay Gio");
    }
}
   