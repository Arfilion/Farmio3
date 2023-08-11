using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBXManager : MonoBehaviour
{
    public List<AudioClip> musicList;
    // Start is called before the first frame update
    private void Start()
    {
        Scythe scythe = FindObjectOfType<Scythe>();
        Rifle rifle = FindObjectOfType<Rifle>();
        Repellent repellent = FindObjectOfType<Repellent>();
        Player player = FindObjectOfType<Player>().GetComponent<Player>();
        Interactor interactor = FindObjectOfType<Interactor>();
        Soil soil = FindObjectOfType<Soil>();
        speed speed = FindObjectOfType<speed>();
        enhanceDamage enhancedamage = FindObjectOfType<enhanceDamage>();
        Shield shield = FindObjectOfType<Shield>();
        // Inventory inventory = FindObjectOfType<Inventory>();

        if (scythe != null)
        {
            scythe.OnPlayerClick += PlayProjectileSound;
        }
        if (rifle != null)
        {
            rifle.OnPlayerClick += PlayProjectileSound;
            rifle.OnPlayerR += PlayReloadSound;
        }
        if (repellent != null)
        {
            repellent.OnPlayerClick += PlayProjectileSound;
        }
        if (player != null)
        {
            player.OnPlayerMove += PlayWalkSound;
            player.PickedSpeedPowerUp += PlaySpeedPickupSound;
            player.PickedDamagePowerUp += PlayDamagePickupSound;
            player.PickedShieldPowerUp += PlayShieldPickupSound;

        }
        if (interactor != null)
        {
            interactor.OnPlayerInteract += PlayInteractSound;
        }
        
        else
        {
            print("bossisnull");
        }
        if (soil != null)
        {
            soil.OnPlayerPlant += PlayPlantSound;
        }
       



        /* if (inventory != null)
         {
             inventory.OnPlayerFill += PlayFillSound;
         }*/
    }
    


    private void PlayProjectileSound()
    {

        Player player = FindObjectOfType<Player>();
        switch (player.weaponCode)
        {
            case 0:
                AudioSource.PlayClipAtPoint(musicList[0], transform.position);
                break;
            case 1:
                AudioSource.PlayClipAtPoint(musicList[1], transform.position);
                break;
            case 2:
                AudioSource.PlayClipAtPoint(musicList[2], transform.position);
                break;
            default:
                break;
        }

    }
    private void PlayWalkSound()
    {
        AudioSource.PlayClipAtPoint(musicList[3], transform.position);
    }
    private void PlayReloadSound()
    {
        AudioSource.PlayClipAtPoint(musicList[4], transform.position);
    }
    private void PlayInteractSound()
    {
        AudioSource.PlayClipAtPoint(musicList[5], transform.position);

    }
    private void PlayPlantSound()
    {
        AudioSource.PlayClipAtPoint(musicList[4], transform.position);

    }
    private void PlaySpeedPickupSound()
    {
        AudioSource.PlayClipAtPoint(musicList[7], FindObjectOfType<Player>().transform.position);

    }
    private void PlayDamagePickupSound()
    {
        AudioSource.PlayClipAtPoint(musicList[8], FindObjectOfType<Player>().transform.position);

    }
    private void PlayShieldPickupSound()
    {
        AudioSource.PlayClipAtPoint(musicList[9], FindObjectOfType<Player>().transform.position);

    }

}

