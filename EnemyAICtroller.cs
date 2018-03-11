using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAICtroller : MonoBehaviour {

    public Transform target;
    private CharacterController characterController;
    private Animation animation1;
    public float rotateSpeed = 30f;//怪物转向的速度
    public float directionSpeed = 2f;//怪物转向的时间间隔
    public float attackMoveSpeed = 5f;
    public float timeToNewDirection = 0f;
    public float walkSpeed = 3f;//怪物闲逛的速度
    public float directionTraveltime = 2f;//怪物转向的时间间隔
    public float idleTime = 1.5f;//转向时候的思考时间
    public Vector3 distanceToPlayer ;//巡逻范围
    public float attackDistance=15f;

    public float damage = 1f;
    private Vector3 attackPosition = new Vector3(0, 1, 0);//
    private float lastAttackTime = 0f;
    private float attackRadius = 2.5f;//怪物攻击半径
    bool isAttacking;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (!target)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
        animation1 = GetComponent<Animation>();
        animation1.wrapMode = WrapMode.Loop;
        animation1["EBunny_Death"].wrapMode = WrapMode.Once;
        animation1["EBunny_Death"].layer = 5;
        animation1["EBunny_Hit"].layer =3;
        animation1["EBunny_Attack"].layer = 1;
        StartCoroutine(InitEnemy());
    }
    //AI的主要逻辑
    IEnumerator InitEnemy()
    {
        while(true)
        {
            yield return StartCoroutine(Idle());
            yield return StartCoroutine(Attack());
        }
    }
    //等待状态
    IEnumerator  Idle()
    {
        if(Time.time > timeToNewDirection)  //当了转方向的时候
        {
            yield return new WaitForSeconds(idleTime);//等待IdleTime时间，模拟怪物思考的这个行为
            //通过产生随机值，让怪物随机转换方向
            if (Random.value>0.5)
            {
                transform.Rotate(new Vector3(0, 5, 0), rotateSpeed);

            }
            else
            {
                transform.Rotate(new Vector3(0, -5, 0), rotateSpeed);

            }
            timeToNewDirection = Time.time + directionTraveltime;
        }
        Vector3 walkForWard = transform.TransformDirection(Vector3.forward);
        characterController.SimpleMove(walkForWard*walkSpeed);

        distanceToPlayer = transform.position - target.position;
        if (distanceToPlayer.magnitude<attackDistance)//如果两者之间的距离小于巡逻范围，意味着主角进入了攻击范围
        {
            yield break;
        }
        yield return null;
    }
    //攻击状态
    IEnumerator Attack()
    {
        isAttacking = true;
        animation1.Play("EBunny_Attack");
        transform.LookAt(target);

        Vector3 direction =transform.TransformDirection(Vector3.forward * attackMoveSpeed);
        characterController.SimpleMove(direction);

        bool lostSight = false;

        if(!lostSight)
        {
            Vector3 location = transform.TransformPoint(attackPosition)-target.position;
            if (Time.time>lastAttackTime+2f&&location.magnitude<attackRadius)
            {
                target.SendMessage("ApplayDamage",damage);
                lastAttackTime = Time.time;
            }
            if (location.magnitude < attackRadius)
            {
                lostSight = true;
                yield break;
            }
            yield return null;
        }       
        isAttacking = false;
    }
}
