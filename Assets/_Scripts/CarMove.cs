using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    protected Rigidbody rb;
    protected CarStatus status;
    protected DameReceiver dameReceiver;
    public int speedCar = 1;
    public int speed = 1;
    public int speedMax = 15;
    public int speedRotateRight = 50;
    public int speedRotateLeft = -50;
    public float pressHorizontal = 0f;
    public float pressVertical = 0f;
    public bool isCarMove = false;
    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.status = GetComponent<CarStatus>();
        this.dameReceiver = GetComponent<DameReceiver>();
    }
    private void FixedUpdate()
    {
        this.UpdateSpeed();
    }
    private void Update()
    {
        this.pressVertical = Input.GetAxis("Vertical");
        this.pressHorizontal = Input.GetAxis("Horizontal");
        if (!this.dameReceiver.IsDeal() && this.status.capacity > 0)
        {
            if (this.status.checkInCity % 2 == 0)
            {
                this.speedMax = 13;                
            }
            else
            {
                this.speedMax = 7;               
            }
        }
    }
    public void UpdateSpeed()
    {
        this.UpdateSpeedUpDown();
        this.UpdateRotateRight();
        this.UpdateRotateLeft();
        Vector3 movement = transform.TransformDirection(Vector3.forward * pressVertical);
        rb.MovePosition(rb.position + this.speedCar * movement * Time.deltaTime);
    }
    protected void UpdateSpeedUpDown()
    {
        if (pressVertical != 0)
        {
            this.isCarMove = true;
            this.speedCar += speed;
            if (this.speedCar > speedMax)
            {
                this.speedCar = speedMax;
            }
        }
        else
        {
            this.isCarMove = false;
            this.speedCar = 0;
        }
    }
    protected void UpdateRotateRight()
    {
        if (pressHorizontal > 0)
        {
            if (this.pressVertical > 0 && speedCar != 0)
            {
                Quaternion deltaRotation = Quaternion.Euler(Vector3.up * speedRotateRight * Time.fixedDeltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
            else if (this.pressVertical < 0 && speedCar != 0)
            {
                Quaternion deltaRotation = Quaternion.Euler(0f, -speedRotateRight * Time.fixedDeltaTime, 0f);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
        }
    }
    protected void UpdateRotateLeft()
    {
        if (pressHorizontal < 0)
        {
            if (this.pressVertical > 0 && speedCar != 0)
            {
                Quaternion deltaRotation = Quaternion.Euler(Vector3.up * speedRotateLeft * Time.fixedDeltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
            else if (this.pressVertical < 0 && speedCar != 0)
            {
                Quaternion deltaRotation = Quaternion.Euler(Vector3.up * -speedRotateLeft * Time.fixedDeltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
        }
    }
}