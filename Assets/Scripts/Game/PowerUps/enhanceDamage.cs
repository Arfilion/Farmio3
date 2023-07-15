using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enhanceDamage : PowerUp
{
    public delegate void PowerUpPicker();
    public event PowerUpPicker PickedPowerUp;
    public GameObject strikePrefab;
    public GameObject poisonPrefab;
    public GameObject bulletPrefab;
    public Player player;
    public int multiplier;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Player>();
        duration = 20;
        name = "enhanceDamage";
    }
    

    // Update is called once per frame


    protected override void DoLogic()
    {
        if (!player.isDamageBuffed)
        {
            return;
        }
        else
        {
            PickedPowerUp?.Invoke();

            strike strikeDamage = strikePrefab.GetComponent<strike>();
            strikeDamage.damage *= multiplier;
            Poison poisonDamage = poisonPrefab.GetComponent<Poison>();
            poisonDamage.damage *= multiplier;
            Bullet bulletDamage = bulletPrefab.GetComponent<Bullet>();
            bulletDamage.damage *= multiplier;
            player.isDamageBuffed = true;
            PickUp();
        }
        
    }
}
