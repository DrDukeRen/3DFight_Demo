using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家动画控制，配合动画控制器设置的参数来控制
/// </summary>
public class WidgetAnimation : MonoBehaviour {

    private Move move;
    private Animator animator;

    private void Start()
    {
        move = GetComponent<Move>();
        animator = GetComponent<Animator>();
        if (animator.layerCount == 2)
        {
            animator.SetLayerWeight(1, 1);
        }
    }
    private void Update()
    {
        if (move.isGround)
        {
            animator.SetBool("isFallDown", false);
            if (move.IsBoosting())
            {
                animator.SetBool("isBoost", true);
            }
            else
            {
                animator.SetBool("isBoost", false);

            }

            if (move.IsDucking())
            {
                animator.SetBool("isDuck", true);
            }
            else
            {
                animator.SetBool("isDuck", false);

            }
            if (move.IsMoving())
            {
                animator.SetFloat("Speed", Input.GetAxis("Vertical"));
            }
        }
        else
        {
            animator.SetBool("isFallDown", true);//如果不在地面上就要掉下来
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("isJump", true);
            }
            if (Input.GetButtonUp("Jump"))
            {
                animator.SetBool("isJump", false);
            }
        }
    }
    public void GetHit()
    {
        animator.SetBool("isGotHit", true);
    }
    public void PlayDie()
    {
        animator.SetBool("isDie", true);
    }
    public void ReBorn()//复活
    {
        animator.SetBool("isDie", false);
    }



}
