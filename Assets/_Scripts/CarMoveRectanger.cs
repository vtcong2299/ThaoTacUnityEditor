using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CarMoveRectanger : MonoBehaviour
{
    public Transform[] Point;
    public int speed = 40;
    public int speedRotate = 8;
    public int index = 0;
    public void DiChuyen()
    {
        Vector3 targetPoint = Point[index].position;
        base.transform.position = Vector3.MoveTowards(base.transform.position, targetPoint, speed * Time.fixedDeltaTime);
        float distance = Vector3.Distance(transform.position, targetPoint);
        if (distance < 0.01f)
        {
            index++;
            if (index >= Point.Length)
            {
                index = 0;
            }
        }
    }
    public void Xoay()
    {
        Vector3 direction = Point[index].position - transform.position;
        //direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotate * Time.deltaTime);
    }
    void FixedUpdate()
    {
        DiChuyen();
        Xoay();
    }
}
