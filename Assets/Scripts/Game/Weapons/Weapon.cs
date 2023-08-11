using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public GameObject prefab;
    protected float counter;
    public int ammo;
    public int playerWeaponCode;
    public float reload;
    public Transform spawnPoint;
    public ParticleSystem flush;

    // Start is called before the first frame update
    protected virtual void Update()
    {
        counter += Time.deltaTime;
        Use();
    }
    private  void Use()
    {
        Player player = GetComponent<Player>();
        playerWeaponCode = player.weaponCode;
        switch (playerWeaponCode)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Mouse0) && DayNightCycle.instance.isNight == true)
                {
                    DoLogic();
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Mouse0) && DayNightCycle.instance.isNight == true)
                {
                    DoLogic();
                }
                break;
            case 2:
                if (Input.GetKey(KeyCode.Mouse0) && DayNightCycle.instance.isNight == true)
                {
                    DoLogic();
                }
                if (Input.GetKeyDown(KeyCode.Mouse0 ) && DayNightCycle.instance.isNight == true)
                {
                    flush.Play();
                }
                if (Input.GetKeyUp(KeyCode.Mouse0) || DayNightCycle.instance.isNight == false)
                {
                    flush.Stop();
                }
                break;
        }        
    }
    protected abstract void DoLogic();
}
