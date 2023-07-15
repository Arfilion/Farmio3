using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scythe : Weapon
{
    // Update is called once per frame
    public event Action OnPlayerClick;

    protected override void DoLogic()
    {
        if (counter >= reload && playerWeaponCode == 0)
        {
            OnPlayerClick?.Invoke();

            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            counter = 0;
        }
    }
}

