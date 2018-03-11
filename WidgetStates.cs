using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家状态，生命值，能量值的控制等
/// </summary>
public class WidgetStates : MonoBehaviour {

    public float hphealth = 10f;
    public float maxHphealth = 10f;
    public float boost = 10f;
    public float maxBoost = 10f;
    public float weigitaUseBoost = 2f;//能量值减少速度
    private  CharacterController m_Controller;
    public AudioClip getHitSound;//受伤音效
    public AudioClip deathSound;//死亡音效

    private  Move move;
    private WidgetAnimation widgetAnimation;
    
    private void Start()
    {
        move = GetComponent<Move>();
        widgetAnimation = GetComponent<WidgetAnimation>();
        m_Controller = GetComponent<CharacterController>();
    }

    //增加生命值
    public void AddHealth(float health)
    {
        hphealth += health  ;
        if (hphealth >= maxHphealth)
        {
            hphealth = maxHphealth;
        }
    }
    //减少生命值
   public void ApplayDamage(float health)
    {
        if (getHitSound)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = getHitSound;
            audio.Play();
        }
        hphealth -= health  ;
        if (hphealth <= 0)
        {
            hphealth = 0;
            StartCoroutine (Die());
        }
    }
    //增加能量值
    public void AddEnergy(float tempBoost)
    {
        boost += tempBoost;
        if (boost >= maxBoost)
        {
            boost = maxBoost;
        }
    }
    //减少能量值
    public void DemageEnergy()
    {
        if (boost > 0)
        {
            boost -= weigitaUseBoost ;
        }
        else
        {
            boost = 0;
        }
    }
    //死亡
 IEnumerator Die()
    {

        Debug.Log("你挂掉了，呜呜！");
        //释放玩家控制权
        //move.isControllable = false;
        //播放死亡动画
        widgetAnimation.PlayDie();
     
        //等待几秒时间，
        yield return StartCoroutine(WaitForDie());
        if (getHitSound)    //播放死亡音效
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = deathSound;
            audio.Play();
        }
        //隐藏玩家
        HideCharactor();
        //等待几秒时间，角色死亡到复活到时间
        yield return StartCoroutine(WaitForOneSeconde());
        //复活，复活点复活
        if (CheckPiont.isActivePT)
        {
            m_Controller.transform.position = CheckPiont.isActivePT.transform.position;
            m_Controller.transform.position = new Vector3(m_Controller.transform.position.x, m_Controller.transform.position.y + 0.5f, m_Controller.transform.position.z);
        }

        ShowCharactor();
        //重置生命值能量值
        hphealth = maxHphealth;
        boost = maxBoost;
        widgetAnimation.ReBorn();
    }

    IEnumerator WaitForDie()
    {
        yield return new WaitForSeconds(3.5f);
    }
    IEnumerator WaitForOneSeconde()
    {
        yield return new WaitForSeconds(3f);
    }
    //隐藏角色
    void  HideCharactor()
    {
        GameObject.FindGameObjectWithTag("Body").GetComponent<SkinnedMeshRenderer>().enabled = false;
        GameObject.FindGameObjectWithTag("Wheels").GetComponent<SkinnedMeshRenderer>().enabled = false;
        GameObject.FindWithTag("Body").GetComponent<SkinnedMeshRenderer>().enabled = false;
        GameObject.FindWithTag("Wheels").GetComponent<SkinnedMeshRenderer>().enabled = false;

        move.isControllable = false;
        print("玩家已死亡，隐藏");
    }
    //显示角色
    void ShowCharactor()
    {
        GameObject.FindGameObjectWithTag("Body").GetComponent<SkinnedMeshRenderer>().enabled = true;
        GameObject.FindGameObjectWithTag("Wheels").GetComponent<SkinnedMeshRenderer>().enabled = true;
        move.isControllable = true;
        print("玩家已复活，显示");

    }
}
