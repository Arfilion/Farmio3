using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speed : PowerUp
{
    
    public float counter;
    public int bonus;

    // Start is called before the first frame update
    void Start()
    {
        duration = 20;
        name = "speed";

    }

    // Update is called once per frame
   
          
    protected override void DoLogic()
    {
        Player player = FindObjectOfType<Player>().GetComponent<Player>();
        counter = 0;
        PickUp();
        player.speed += bonus;
        player.isSpeedBuffed = true;
    }

}
