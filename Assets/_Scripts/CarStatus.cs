using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatus : MonoBehaviour
{
    protected CarMove carMove;
    protected CarDameReceiver dameReceiver;
    public float capacity = 1000;
    public int checkInCity = 0;
    protected int check;
    public int countDuongDua = 0;    
    public int countDich = 0;
    private int soSanh;
    public int coins = 0;
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
            if (this.countDuongDua >= this.countDich)
            {
                Debug.Log("Vong Dua Thu: " + (this.countDich + 1));
                this.soSanh = this.countDich;
                UIManager.instance?.OnLapsChange(countDich);
            }
        }
    }
    private void CheckGas()
    {
        if(this.carMove.isCarMove)
        {
            this.capacity -= 1f* this.carMove.speedCar*Time.fixedDeltaTime;
            UIManager.instance.OnFuleChange(capacity);
            if (this.capacity <= 0f)
            {
                UIManager.instance.EndGame((int)capacity);
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
            Debug.Log("Do xang");
        }
    }
    protected void Coins(Collider collider)
    {
        if (collider.gameObject.tag == "Coins")
        {
            coins++;
            Destroy(collider.gameObject);
            UIManager.instance?.OnCoinsChange(coins);
        }
    }
    public virtual void OnTriggerEnter(Collider collider)
    {
       this.CheckFule(collider);
       this.CheckInCity(collider);
       this.CheckLaps(collider);
       this.Coins(collider);
    }
    
}
