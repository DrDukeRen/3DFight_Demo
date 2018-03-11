using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBunnyStates : MonoBehaviour {

    public float health = 10f;
    public int numHeldItemMin = 1;//随机最小值
    public int numHeldItenMax = 3;//随机最大值
    public GameObject pickUp1;
    public GameObject pickUp2;
    private bool isDead = false;

    private Animation anima;
    private void Start()
    {
        anima = GetComponent<Animation>();
    }
    public void ApplayDamage(float damage)
    {
        if (health < 0)
        {
            return;
        }
        anima.Play("EBunny_Hit");
        health -= damage;
        if (health<=0&&!isDead)
        {
            health = 0;
            isDead = true;
            StartCoroutine(Die()); 
        }
    }

    IEnumerator Die()
    {
        anima.Stop();
        anima.Play("EBunny_Death");
        Destroy(this.GetComponent<EnemyAICtroller>());

        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
        Vector3 itemLocation = this.transform.position;//获取当前怪物的死亡地点
        int reWardItem = Random.Range(numHeldItemMin, numHeldItenMax);
        for (int i = 0; i < reWardItem;i++)
        {
            Vector3 randomItemLocation = itemLocation;
            randomItemLocation += new Vector3(Random.Range(-2, 2), 0.2f, Random.Range(-2, 2));//死亡地点附近的随机分布
            if(Random.value>0.5f)
            {
                Instantiate(pickUp1,randomItemLocation,pickUp1.transform.rotation);
            }
            else
            {
                Instantiate(pickUp2,randomItemLocation,pickUp2.transform.rotation);

            }
        }
    }

    public bool isGameObjectDeadí()
    {
        return isDead;
    }
}
