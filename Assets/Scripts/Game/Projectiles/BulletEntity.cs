using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEntity : MonoBehaviour
{
    public int damage;
    public float lifetime;
    public  float speed;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (DayNightCycle.instance.isNight == false)
        {
            Destroy(gameObject);
        }
    }
    protected virtual void Impact()
    {
        Destroy(gameObject, lifetime);
    }
  

     protected virtual void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

    }
}
