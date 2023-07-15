using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    
    public float duration;
    private void Start()
    {
        Destroy(gameObject, 10);

    }

    void Update()
    {
        if (DayNightCycle.instance.isNight == false)
        {
            Destroy(gameObject);
        }
    }
    protected void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "player")
        {
            DoLogic();
        }
       
    }
    protected abstract void DoLogic();

    public virtual void PickUp()
    {
        Destroy(gameObject);
    }
    
}
