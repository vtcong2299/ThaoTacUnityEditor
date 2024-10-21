using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDameReceiver : DameReceiver
{
    protected CarMove carMove;
    private void Start()
    {
        this.hp = 100;
        OnHpChange();
        this.carMove =GetComponent<CarMove>();
    }
    public override void Receiver(int damage)
    {
        base.Receiver(damage);
        if (this.IsDead())
        {
            UIManager.instance.EndGame(hp);
            Debug.Log("Xe Hong Roi");
            this.carMove.speedMax = 0;
        }
    }
}
