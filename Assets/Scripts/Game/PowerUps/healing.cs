using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healing : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Player>();
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
            if (player.health >= 80)
            {
                player.health = 100;

            }
            else
            {
                player.health += 20;

            }
            Destroy(gameObject);
        }

    }

    
    
}
