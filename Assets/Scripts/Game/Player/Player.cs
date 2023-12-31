using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    Rigidbody _rb;
    [SerializeField] Camera mainCam;
    [SerializeField] Animator playerAnim;
    public float forceJump, v, h;
    public float speed, walkCounter, footstepInterval;
    public float speedRotation;
    private float disabledSpeed = 0, disabledForceJump = 0, disabledSpeedRot = 0;
    private float enabledSpeed, enabledForceJump, enabledSpeedRot;
    public Transform _spawnPoint;
    public GameObject bulletPrefab, strikePrefab, poisonPrefab, enhanceDamagePrefab, speedPrefab, shieldPrefab;

    public MeshFilter weaponModel;
    public List<MeshFilter> weaponModels = new List<MeshFilter>();

    public float counter, speedBuffCounter, damageBuffCounter, shieldBuffCounter, speedBuffDuration;
    public float reload, reloadPoison;
    public Vector3 angle;
    public int ammo, weaponCode;
    public bool isSpeedBuffed, isDamageBuffed, isShieldBuffed;
    public List<GameObject> plants;
    public event Action OnPlayerMove;
    public delegate void BuffEventHandler<T>(T buff);
    public delegate void PowerUpPicker();
    public event PowerUpPicker PickedSpeedPowerUp;
    public event PowerUpPicker PickedDamagePowerUp;
    public event PowerUpPicker PickedShieldPowerUp;
    [SerializeField] GameObject Canvas;
    public float health;


    private void Awake()
    {
        health = 100;        
        _rb = GetComponent<Rigidbody>();
        instance = this;
        ammo = 6;
        weaponCode = 0;
        speed = 10;
        enabledSpeed = speed;
        enabledForceJump = forceJump;
        enabledSpeedRot = speedRotation;
        isSpeedBuffed = false;
        isDamageBuffed = false;
        isShieldBuffed = false;
        damageBuffCounter = 0;
        speedBuffCounter = 0;
        shieldBuffCounter = 0;
        strike strikeDamage = strikePrefab.GetComponent<strike>();
        strikeDamage.damage = 50;
        Poison poisonDamage = poisonPrefab.GetComponent<Poison>();
        poisonDamage.damage = 7;
        Bullet bulletDamage = bulletPrefab.GetComponent<Bullet>();
        bulletDamage.damage = 20;
    }

    void Update()
    {
        bossSpawnedPlayerHealth.instance.BarView();
        bossSpawned.instance.BarView();

        BuffHandler(isSpeedBuffed);
        BuffHandler(isShieldBuffed);
        BuffHandler(isDamageBuffed);

        walkCounter += Time.deltaTime;
        counter += Time.deltaTime;
       
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        playerAnim.SetFloat("Vertical", v);
        playerAnim.SetFloat("Horizontal", h);

        Movement(v, h);
        SwapWeapon();

        if (DayNightCycle.instance.isNight)
        {
            weaponModel.sharedMesh = weaponModels[0].sharedMesh;
        }
        else
        {
            weaponModel.sharedMesh = weaponModels[3].sharedMesh;
        }
       
    }
    

    public void SwapWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            switch (weaponCode)
            {
                case 0:
                    weaponModel.sharedMesh = weaponModels[0].sharedMesh;
                    weaponCode += 1;
                    break;
                case 1:
                    weaponModel.sharedMesh = weaponModels[1].sharedMesh;
                    weaponCode += 1;
                    break;
                case 2:
                    weaponModel.sharedMesh = weaponModels[2].sharedMesh;
                    weaponCode = 0;
                    break;
                default:
                    break;
            }
        }
    }

   
    public void BuffHandler<T>(T isBuffed) 
    {
        if (isSpeedBuffed)
        {
            speedBuffCounter += Time.deltaTime;
            if (speedBuffCounter >= speedPrefab.GetComponent<speed>().duration)
            {
                isSpeedBuffed = false;
                speedBuffCounter = 0;
                speed = 10;
            }
        }
        else if (isDamageBuffed)
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
                poisonDamage.damage = 7;
                Bullet bulletDamage = bulletPrefab.GetComponent<Bullet>();
                bulletDamage.damage = 20;

            }
            
        }
        else if (isShieldBuffed)
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
        }

    }
    private void OnCollisionEnter(Collision collider)
    {
        Center center = collider.gameObject.GetComponent<Center>();
        if (collider.gameObject.tag == "buff speed")
        {
            isSpeedBuffed = true;
            PickedSpeedPowerUp?.Invoke();

        }
        else if (collider.gameObject.tag == "buff damage")
        {
            isDamageBuffed = true;
            PickedDamagePowerUp?.Invoke();

        }
        else if (collider.gameObject.tag == "buff shield")
        {
            isShieldBuffed = true;
            PickedShieldPowerUp?.Invoke();

        }
        else if (collider.gameObject.tag == "buff health")
        {
            health += 20;
            //PickedShieldPowerUp?.Invoke();

        }

    }
    void Movement(float v, float h)
    {
        transform.position += transform.forward * v * speed * Time.deltaTime;
        transform.position += transform.right * h * speed * Time.deltaTime;
        if ((v != 0 || h != 0) && walkCounter >= footstepInterval)
        {
            OnPlayerMove?.Invoke();
            walkCounter = 0;
            walkCounter += Time.deltaTime;

        }
    }


    public void DisableMovement()
    {
        forceJump = disabledForceJump;
        speed = disabledSpeed;
        speedRotation = disabledSpeedRot;
    }

    public void EnableMovement()
    {
        forceJump = enabledForceJump;
        speed = enabledSpeed;
        speedRotation = enabledSpeedRot;
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public void SetPosition(Vector3 newPosition)
    {
        this.transform.position = newPosition;
    }
    public void TakeDamage()
    {
        Player.instance.health -= 0.5f;
    }
    
}