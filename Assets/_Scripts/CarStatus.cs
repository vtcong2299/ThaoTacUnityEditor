using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatus : MonoBehaviour
{
    protected CarMove carMove;
    public float capacity = 100;
    public int checkInCity = 0;
    protected int check;
    public int countDuongDua = 0;    
    public int countDich = 0;
    private int soSanh;
    private void Start()
    {
        this.carMove = GetComponent<CarMove>();
        this.capacity = 1000;
        this.check = this.checkInCity;
        this.soSanh = this.countDich-1;
    }
    private void FixedUpdate()
    {
        this.CheckGas();
        this.DemVongDua();
    }
    private void DemVongDua()
    {
        if (this.soSanh != this.countDich)
        {
            if (this.countDuongDua == this.countDich)
            {
                Debug.Log("Vong Dua Thu: " + (this.countDich + 1));
                this.soSanh = this.countDich;
            }
        }
    }
    private void CheckGas()
    {
        if(this.carMove.isCarMove)
        {
            this.capacity -= 1f* this.carMove.speedCar*Time.fixedDeltaTime;
            if (this.capacity <= 0f)
            {
                this.carMove.speedMax = 0;
            }
        }
    }
    protected void CheckLaps(Collider collider)
    {
        if (collider.gameObject.tag == "DuongDua")
        {
            this.countDuongDua += 1;
        }
        if (collider.gameObject.tag == "Dich")
        {
            this.countDich++;
        }
    }
    protected void CheckInCity(Collider collider)
    {
        if (collider.gameObject.tag == "City")
        {
            this.checkInCity++;
            if (check != checkInCity)
            {
                Debug.Log("Thay Doi Toc Do");
            }
            this.check = this.checkInCity;
        }
    }
    protected void CheckFule(Collider collider)
    {
        if (collider.gameObject.tag == "TramXang")
        {
            this.capacity = 1000;
        }
    }
    public virtual void OnTriggerEnter(Collider collider)
    {
       this.CheckFule(collider);
       this.CheckInCity(collider);
       this.CheckLaps(collider);
    }
    
}
