using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetAttackController : MonoBehaviour
{

    public Move move;
    public Animator animator;

    public float attackTime =0.5f;//   发动攻击的时间
    public Vector3 attackPosition = new Vector3(0, 1, 0);
    public float attackRadius = 3f;
    public float time = 0f;//计时器
    public float damage = 3f;//伤害值
    public bool isBusy = false;
    public Vector3 ourLocation;
    private GameObject[] enemies;//敌人数组

    private void Start()
    {
        move = GetComponent<Move>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        time = time + Time.deltaTime;
        if (time > attackTime && !isBusy && Input.GetButtonDown("Attack"))
        {
            time = 0;//计时器归零
            Debug.Log("攻击键z被按下");

            isBusy = true;
            StartCoroutine(DidAttack());
        }
    }
    IEnumerator DidAttack()
    {
        animator.SetBool("isTaser", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("isTaser", false);

        ourLocation = transform.TransformPoint(attackPosition);//在主角的上面（0，1，0）位置
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            EBunnyStates ebunnyStates = enemy.GetComponent<EBunnyStates>();

            if (ebunnyStates == null)
            {
                continue;
            }
            if (Vector3.Distance(enemy.transform.position, ourLocation) < attackRadius)
            {
                ebunnyStates.ApplayDamage(damage);
            }

            isBusy = false;
        }
    }
}
 