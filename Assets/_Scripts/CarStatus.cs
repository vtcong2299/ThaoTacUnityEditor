using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatus : MonoBehaviour
{
    public CarMove carMove;
    public int capacity = 100;
    public int checkInCity = 0;

    private void Start()
    {
        this.capacity = 20000;
        this.carMove = GetComponent<CarMove>();
    }
    protected void CheckGasoline()
    {
        if (this.carMove.isCarMove)
        {
            this.capacity -= 1;
            if (this.capacity == 0)
            {
                carMove.speedMax = 0;
            }
        }
    }
    protected void CheckRefule(Collider collider)
    {
        if(collider.gameObject.tag == "TramXang")
        {
            this.capacity = 20000;
        }
    }
    protected void CheckInCity(Collider collider)
    {
        if(collider.gameObject.tag == "City")
        {
            this.checkInCity++;
        }
    }
    public virtual void OnTriggerEnter(Collider collider)
    {
        this.CheckRefule(collider);
        this.CheckInCity(collider);
    }
    private void Update()
    {
        this.CheckGasoline();
    }
}
