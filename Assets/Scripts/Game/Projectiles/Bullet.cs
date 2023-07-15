using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletEntity
{
    
    // Start is called before the first frame update
   
    void Start()
    {
        speed =70;
        lifetime = 2;
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
            if (digger)
            {

                digger.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
