using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rifle : Weapon
{
    public GameObject particlePrefab;
    public Transform prefabTransform;    
    public delegate void RifleShootHandler();
    public event RifleShootHandler OnPlayerClick;
    public event RifleShootHandler OnPlayerR;


    // Start is called before the first frame update

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.R) && playerWeaponCode == 1)
        {
            StartCoroutine(ReloadRifle());
            OnPlayerR?.Invoke();

        }
    }

    protected override void DoLogic()
    {
        if (ammo > 0 && counter >= reload && playerWeaponCode == 1)
        {
            OnPlayerClick?.Invoke();
            Instantiate(particlePrefab, prefabTransform.position, transform.rotation);
            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            ammo -= 1;
            counter = 0;
        }
    }
    IEnumerator ReloadRifle()
    {

        yield return new WaitForSeconds(2f);
        ammo = 6;

    }
}