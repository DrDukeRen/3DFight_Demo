using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 摄像机跟随玩家控制脚本，可通用
/// </summary>
public class WidgetCamera : MonoBehaviour {

    public Transform target;//目标位置
    public float distance=10f;//距离
    public float height=5f;//高度
    public float heightDamping=2.0f;//摄像机高度上进行调整时的阻尼
    public float rotationDamping=3.0f;//摄像机旋转时候的阻尼
    public float distanceDampingX=0.5f;//摄像机在X轴上进行调整时的阻尼
    public float distanceDampingZ=0.2f;//摄像机在Z轴上进行调整时的阻尼
    public float camSpeed = 2.0f;//摄像机的速度
    public bool isSmoothed = true;//摄像机是否平滑

    private float wantedRotationAngle;//摄像机要达到的角度
    private float wantedHeight;//摄像机想达到的高度
    private float wantedDistanceX;//摄像机想达到的X轴位置
    private float wantedDistanceZ;//摄像机想达到的Z轴位置

    private float currentRotationAngle;//摄像机当前的角度
    private float currentHeight;//摄像机当前的高度
    private float currentDistanceX;//摄像机当前的X轴位置
    private float currentDistanceZ;//摄像机当前的Z轴位置

    private Quaternion currentRotation;

    private void LateUpdate()
    {
        if (!target)
        {
            return;
        }
        wantedRotationAngle = target.eulerAngles.y;//取得当前摄像机要到达的角度值
        wantedHeight = target.position.y + height;//取得摄像机要达到的高度值
        wantedDistanceX = target.position.x - distance;//取得摄像机要达到的X轴位置
        wantedDistanceZ = target.position.z - distance;//取得摄像机要达到的Z轴位置

        currentRotationAngle = transform.eulerAngles.y;//获取当前的角度位置
        currentHeight = transform.position.y;//获取当前的位置信息
        currentDistanceX = transform.position.x;
        currentDistanceZ = transform.position.z;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);//获取当前的角度信息
        currentHeight = Mathf.LerpAngle(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        currentDistanceX = Mathf.LerpAngle(currentDistanceX, wantedDistanceX, distanceDampingX * Time.deltaTime);
        currentDistanceZ = Mathf.LerpAngle(currentDistanceZ, wantedDistanceZ, distanceDampingZ * Time.deltaTime);

        currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);//将当前的角度值转化为角度

        transform.position -= currentRotation * Vector3.forward * distance;//摄像机向目标移动

        transform.position = new Vector3(currentDistanceX, currentHeight, currentDistanceZ);//不断地更新位置

        LookAtMe();
    }
    void LookAtMe()
    {
        if (isSmoothed)
        {
            Quaternion camrotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, camrotation, Time.deltaTime*camSpeed);
        }
        else
        {
            transform.LookAt(target);//始终盯着目标物体

        }
    }
}
