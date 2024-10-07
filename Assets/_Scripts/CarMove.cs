using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CarMove : MonoBehaviour
{
    public Transform[] Point;
    public int speed = 5;
    public int index = 0;
    public float rotate = 0;
    private void Start()
    {
        Vector3 pointMau = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
        Vector3 pointz = pointMau - transform.position;
        Vector3 point1 = Point[0].position - transform.position;
        rotate = Vector3.SignedAngle(pointz, point1, Vector3.up);
    }
    public void DiChuyen()
    {
        if (index < Point.Length)
        {
            Vector3 targetpoint = Point[index].position;
            transform.position = Vector3.MoveTowards(transform.position, targetpoint, speed * Time.fixedDeltaTime);
            float distance = Vector3.Distance(transform.position, targetpoint);
            if (distance < 0.1f)
            {
                index++;
                if (index == 8) 
                { 
                    index = 0; 
                }
                if (index < Point.Length)
                {
                    rotate += 45;
                    transform.rotation = Quaternion.Euler(0f, rotate, 0f);
                    // Tính toán hướng quay để rẽ phải
                    //                Vector3 rightDirection = transform.right;
                    //                Vector3 toNextWaypoint = Point[index].position - transform.position;
                    //                float angle = Vector3.SignedAngle(rightDirection, toNextWaypoint, Vector3.up);
                    //                transform.Rotate(Vector3.up, angle);
                }
            }
        }
    }
    private void Update()
    {
        DiChuyen();
    }
    
}
