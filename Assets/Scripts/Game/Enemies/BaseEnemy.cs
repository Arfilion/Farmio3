using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class BaseEnemy : enemyEntity
{
    public event Action OnEnemyMove;

    // Start is called before the first frame update
    void Start()
    {
        walkCounter += Time.deltaTime;

        canBeHit = true;
        Find();
        readyToHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.instance != null)
        {
            Destroy(gameObject);
        }
        counter += Time.deltaTime;
        Hit(spawnPoint);
        Checker();
        Movement();
        if (DayNightCycle.instance.isNight == false)
        {
            Destroy(gameObject);
        }
    }
   
}
