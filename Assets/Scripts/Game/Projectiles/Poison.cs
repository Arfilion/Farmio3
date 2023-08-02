using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : BulletEntity
{

    // Start is called before the first frame update
    void Start()
    {
        speed = 7;
        lifetime = 0.15f;
        Impact();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "enemy")
        {
            enemyEntity enemy = collider.gameObject.GetComponent<enemyEntity>();
            digger digger = enemy.GetComponentInChildren<digger>();
          
            if (enemy)
            {

                enemy.TakeDamage(damage);
            }
           
            else if (digger)
            {

                digger.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
