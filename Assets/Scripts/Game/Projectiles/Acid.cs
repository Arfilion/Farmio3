using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : BulletEntity
{

    // Start is called before the first frame update
    void Start()
    {
        damage = 10;
        lifetime = 4;
        Impact();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "target")
        {
            Plant plant = collider.gameObject.GetComponentInParent<Plant> ();
            plant.TakeDamage(damage);

            Destroy(gameObject);

        }       
    }

}
