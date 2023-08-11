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
          
            if (enemy)
            {

                enemy.TakeDamage(damage);
            }           
           
        }
        else if (collider.gameObject.tag == "digger")
        {
            digger digger = collider.gameObject.GetComponent<digger>();
            if (digger)
            {
                digger.Reveal();
                digger.renderer.enabled=true;
                digger.TakeDamage(damage);
            }
        }
    }
}
