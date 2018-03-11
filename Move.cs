using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家移动控制
/// </summary>
public class Move : MonoBehaviour {

    public float moveSpeed = 6.0f;//物体的移动速度
    public float rotateSpeed = 4.0f;//物体的旋转速度
    public CharacterController controller;

    public float jumpSpeed = 10f;//跳跃速度
    public float fastSpeed = 2f;//加速度值
    public float duckSpeed = 0.5f;//半蹲速度
    public float duckHeight = 1f;//半蹲高度
    public float normalHeight = 2f;//正常高度
    public bool isDucking = false;//是否半蹲
    public bool isControllable=true;//是否可以控制

    public Vector3 moveDirection = Vector3.zero;
    public Vector3 rotateDirection = Vector3.zero;

    public float gravity = 20f;
    public bool isGround;
    private float moveHorz = 0f;
    private bool isBoost = false;
  

    private WidgetStates m_WidgetStates;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        m_WidgetStates = GetComponent<WidgetStates>();
    }
    private void FixedUpdate()
    {
        if (!isControllable)
        {
            Input.ResetInputAxes();
        }
        else
        {
            if (isGround)
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                moveDirection = new Vector3(h, 0, v);
                moveDirection *= moveSpeed;

                moveHorz = Input.GetAxis("Horizontal");
                if (moveHorz > 0)//向右转
                {
                    rotateDirection = new Vector3(0, 1, 0);
                }
                else if (moveHorz < 0)//向左转
                {
                    rotateDirection = new Vector3(0, -1, 0);
                }
                else
                {
                    rotateDirection = new Vector3(0, 0, 0);
                }
                if (Input.GetButton("Jump"))//跳跃设置
                {
                    moveDirection.y = jumpSpeed;
                }
                if (Input.GetButton("Boost"))//加速设置     //left shift 键控制
                {
                    if (m_WidgetStates)
                    {
                        m_WidgetStates.DemageEnergy();
                        moveDirection *= fastSpeed;
                        isBoost = true;
                    }

                }
                if (Input.GetButtonUp("Boost"))//left shift 键控制
                {
                    isBoost = false;
                }
                if (Input.GetButton("Duck"))//半蹲状态设置
                {
                    controller.height = duckHeight;
                    controller.center = new Vector3(controller.center.x, controller.height / 2 + 0.25f, controller.center.z);
                    moveDirection *= duckSpeed;
                    isDucking = true;
                }
                if (Input.GetButtonUp("Duck"))//取消半蹲状态设置
                {
                    controller.height = normalHeight;
                    controller.center = new Vector3(controller.center.x, controller.height / 2, controller.center.z);
                    moveDirection *= fastSpeed;
                    isDucking = false;
                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                    m_WidgetStates.AddHealth(2);
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    m_WidgetStates.ApplayDamage(2);
                }

            }


            moveDirection.y -= gravity * Time.deltaTime;//模拟的重力
            controller.transform.Rotate(rotateDirection * Time.deltaTime, rotateSpeed);//玩家的旋转
            CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);//游戏对象的移动，并且返回一个碰撞状态

            isGround = ((flags & CollisionFlags.Below) != 0);//根据碰撞判断是否在地面上
        }
    }
    public bool IsMoving()
    {
        return moveDirection.magnitude > 0.5f;
    }
    public bool IsBoosting()
    {
        return isBoost;
    }
    public bool IsDucking()
    {
        return isDucking;
    }
    public bool IsGround()
    {
        return isGround;
    }
}
