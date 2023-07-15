using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    
   /* private float speedBuffCounter;

    // Start is called before the first frame update
    void Start()
    {
        speedBuffCounter = 0;

        Player player = FindObjectOfType<Player>();
        
        if (player != null)
        {
            player.OnSpeedBuff += EndingBuff;
        }

    }

    private void EndingBuff(float buffDuration)
    {
        print("empece");
        Player player = FindObjectOfType<Player>();

        if (player.isSpeedBuffed)
        {
            speedBuffCounter += Time.deltaTime;
            if (speedBuffCounter >= player.speedPrefab.GetComponent<speed>().duration)
            {
                player.isSpeedBuffed = false;
                speedBuffCounter = 0;
                player.speed = 10;
            }
        }
        if (isDamageBuffed)
        {
            damageBuffCounter += Time.deltaTime;
            if (damageBuffCounter >= enhanceDamagePrefab.GetComponent<enhanceDamage>().duration)
            {
                isDamageBuffed = false;
                damageBuffCounter = 0;
                strike strikeDamage = strikePrefab.GetComponent<strike>();
                strikeDamage.damage = 50;
                print(strikeDamage);
                Poison poisonDamage = poisonPrefab.GetComponent<Poison>();
                poisonDamage.damage = 70;
                Bullet bulletDamage = bulletPrefab.GetComponent<Bullet>();
                bulletDamage.damage = 20;
            }
        }
        if (isShieldBuffed)
        {
            shieldBuffCounter += Time.deltaTime;
            if (shieldBuffCounter >= shieldPrefab.GetComponent<Shield>().duration)
            {
                isShieldBuffed = false;
                shieldBuffCounter = 0;
                plants = GameObject.FindGameObjectsWithTag("plant").ToList();
                foreach (GameObject plant in plants)
                {
                    plant.GetComponent<Plant>().inmune = false;
                }
            }
        }*/
    }
   

