using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CarMoveAuto : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip runClip;
    public Vector3[] Point;
    #region SetupField
    [SerializeField]
    private float defaultSpeed = 10;
    [SerializeField]
    private float rotateDuration = 1f;
    [SerializeField]
    private float adjustRotationTime = 0.5f;
    #endregion
    int indexPoint = 0;
    Vector3 targetPos;
    Vector3 directionToTarget;
    float curInterpolationVal;
    [SerializeField]
    private float speed;
    float elapsedTime;
    const float DELTA_ANGLE = 2;
    Quaternion targetQuarternion;
    Quaternion startQuarternion;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();  
        audioSource.clip = runClip;
        speed = defaultSpeed;
        SetTarget();
    }
    private void Update()
    {      
        //Tính toán liên tục hướng xe đến đích
        CalculateDirectionToTarget();
        if (IsReachTarget())
        {
            OnReachTarget();
        }
        //Điểu chỉnh hướng xe đến khi trùng với hướng đến đích
        LerpDirectionToTarget();
        MoveForward();
        audioSource.Play();
    }
    //Các việc thực hiện khi chạm đến điểm đích
    public void OnReachTarget()
    {    
        //Chuyển đến điểm đích tiếp theo
        IncreaseIndex();
        SetTarget();
        //Tính toán lại hướng của đích và hướng hiện tại của xe
        CalculateDirectionToTarget();
        CalculateStartTargetQuarternion();
        //Tính toán lại vận tốc và đặt lại thời gian tăng nội suy
        SetSpeed();
        ResetLerpWhenReachTarget();
    }
    //Di chuyển theo hướng trước mặt
    public void MoveForward()
    {
        transform.position += speed * transform.forward * Time.deltaTime;        
    }
    //Xoay hướng đến điểm đích
    public void LerpDirectionToTarget()
    {
        //Tính góc giữa hướng trước mặt xe và hướng từ xe đến điểm đích
        float angle = Vector3.Angle(directionToTarget, transform.forward);
        if (angle < DELTA_ANGLE)
        {
            transform.forward = directionToTarget;
            return;
        }
        //Nội suy curInterpolationVal chạy từ 0 => 1 tương ứng với hướng của startQuarternion
        //đang chuyển dần thành hướng targetQuarternion
        elapsedTime += Time.deltaTime;
        curInterpolationVal = elapsedTime / rotateDuration;
        if (curInterpolationVal >= 1)
        {
            curInterpolationVal = 1;
        }
        targetQuarternion = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(startQuarternion, targetQuarternion, curInterpolationVal);
    }
    //Reset lại thời gian để tăng nội suy
    public void ResetLerpWhenReachTarget()
    {
        elapsedTime = 0;
    }
    //Tích nội hướng của 2 vector <=0 nghĩa là góc giữa 2 vector >=90%
    //Xét xem đã đến điểm đích chưa
    public bool IsReachTarget()
    {
        return Vector3.Dot(transform.forward, directionToTarget) <= 0;
    }
    //Chuyển điểm đích đến vị trí tiếp theo
    public void IncreaseIndex()
    {
        indexPoint ++;
        if (indexPoint >= Point.Length)
        {
            indexPoint = 0;
        }
    }
    public void SetSpeed()
    {
        //Quãng đường từ vị trí xe đến điểm đích tiếp theo
        float distance = directionToTarget.magnitude;
        //Vận tốc để đi quãng đường đó
        float targetSpeedToRotate = distance / (rotateDuration + adjustRotationTime);
        if (targetSpeedToRotate > defaultSpeed)
        {
            targetSpeedToRotate = defaultSpeed;
        }
        speed = targetSpeedToRotate;
    }
    //Thiết lập lại điểm đích mới
    public void SetTarget()
    {
        targetPos = Point[indexPoint];
    }
    //Tính toán hướng ban đầu của xe
    public void CalculateStartTargetQuarternion()
    {
        startQuarternion = Quaternion.LookRotation(transform.forward);
    }
    //Tính toán hướng từ xe đến đích
    public void CalculateDirectionToTarget()
    {
        directionToTarget = targetPos - transform.position;
    }
   
}
