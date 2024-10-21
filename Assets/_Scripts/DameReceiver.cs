using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameReceiver : MonoBehaviour
{
    public int hp = 1;
    public virtual bool IsDead()
    {
        return this.hp <= 0;
    }
    public virtual void Receiver(int damage)
    {
        this.hp -= damage;
        OnHpChange();
    }
    public void OnHpChange()
    {
        UIManager.instance?.OnHpChange(hp);
    }

}
