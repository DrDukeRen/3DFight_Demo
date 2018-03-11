using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPiont : MonoBehaviour {

    public float spawnRange = 30f;
    public Transform target;
    public GameObject enemy;
    public bool isOutSideRange;//
    private Vector3 distanceToPlayer;
    private GameObject currentEnemy;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        distanceToPlayer = transform.position - target.position;
        if (distanceToPlayer.magnitude<spawnRange)
        {
            if (!currentEnemy)
            {
                currentEnemy = Instantiate(enemy,transform.position,transform.rotation)as GameObject;

            }
            isOutSideRange = false;
        }
        else
        {
            if (currentEnemy)
            {
                Destroy(currentEnemy);
            }
            isOutSideRange = true;    
        }

    }

}
