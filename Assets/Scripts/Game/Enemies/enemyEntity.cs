using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;



public class enemyEntity : MonoBehaviour
{
    public int life;
    public float speedRot, range, rangeRay, reload, counter, footstepInterval = 0.48f, walkCounter;
    public GameObject Acid;
    protected Vector3 dirEnemy,dirShoot;
    public NavMeshAgent agent;
    public LayerMask layerMask;
    public bool readyToHit, canBeHit;
    public Transform spawnPoint;
    protected int n;
    public List<GameObject> plants;
    [SerializeField] Animator enemyAnim;
    public event Action OnEnemyMove;




    public int Life
    {
        get { return life; }
        set { 
            if (life <= 100)
            {
                life = value;
            }
            else if(life<0)
            {
                life = 0;
        
            }
            else if (life > 100)
            {
                life = 100;
            }
        }

    }
    public float Reload
    {
        get { return reload; }
        set {
            if (reload <= 3)
            {
                reload = value;
            }
            else if (reload < 0)
            {
                reload = 0;

            }
            else if (reload > 3)
            {
                reload = 3;
            }
        }

    }
    public float Counter
    {
        get { return counter; }
        set { counter = value; }

    }
    public float SpeedRot
    {
        get { return speedRot; }
        set { speedRot = value; }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Find()
    {
        plants = GameObject.FindGameObjectsWithTag("plant").ToList();
        n = UnityEngine.Random.Range(0, plants.Count-1);
    }
    public virtual void Movement()
    {
        
        dirEnemy = plants[n].transform.position - transform.position;
        if (dirEnemy.magnitude > range || Physics.Raycast(transform.position,dirEnemy, rangeRay, layerMask) == true)
        {
            if ( walkCounter >= footstepInterval)
            {
                OnEnemyMove?.Invoke();
                walkCounter = 0;
                walkCounter += Time.deltaTime;

            }
            enemyAnim.SetBool("isWalking", true);
            agent.SetDestination(plants[n].transform.position);
         


        }
        if (dirEnemy.magnitude <= range && Physics.Raycast(transform.position, dirEnemy, rangeRay, layerMask) == false)
        {
            agent.isStopped=true;

            if (agent.isStopped == true)
            {
                enemyAnim.SetBool("isWalking", false);
                Checker();
                readyToHit = true;

            }
        }
       
    }
    public void Checker()
    {
        if (plants[n] == null)
        {
            plants.RemoveAt(n);
            Find();
            agent.isStopped = false;
        }
    }


    public virtual void Hit(Transform _spawnPoint)
    {      
        if (counter >= reload && dirEnemy.magnitude <= range)
        {
            if (readyToHit == true)
            {
                enemyAnim.SetTrigger("attack");

                Instantiate(Acid, _spawnPoint.position, _spawnPoint.rotation);
                counter = 0;


            }
        }   
        
    }
    public void TakeDamage(int _damage)
    {
        if (canBeHit==true)
        {

            life -= _damage;
            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }        
    }

}
