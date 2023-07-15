using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Repellent : Weapon
{
    // Start is called before the first frame update

    public event Action OnPlayerClick;

    protected override void DoLogic()
    {
        if (counter >= reload && playerWeaponCode == 2)
        {
            OnPlayerClick?.Invoke();

            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            counter = 0;
        }
    }

}

