using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
public struct Boss
{
    public string Name;
    public int Health;

    public Boss(string name, int health)
    {
        Name = name;
        Health = health;
    }
}
public class boss : MonoBehaviour
{
    public bool casting;
    public Player player;
    private float rotationSpeed = 2f;
    [SerializeField] Animator bossAnim;
    [SerializeField] ListGenerator<Center> centerList = new ListGenerator<Center>();
    [SerializeField] ListGenerator<Side> sideList = new ListGenerator<Side>();
    [SerializeField] ListGenerator<Roar> roarList = new ListGenerator<Roar>();
    [SerializeField] ListGenerator<Enrage> enrageList = new ListGenerator<Enrage>();
    [SerializeField] ListGenerator<InnerCircle> innerCircleList = new ListGenerator<InnerCircle>();
    public float distance;
    public static boss instance;
    public int health;
    public int random;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        casting = false;
        Boss boss = new Boss("Boss",100);
        health = boss.Health;
        name = boss.Name;
        distance= Vector3.Distance(transform.position, player.transform.position);
        roarList.Gatherer();
        sideList.Gatherer();
        centerList.Gatherer();
        enrageList.Gatherer();
        innerCircleList.Gatherer();
        Dictionary<string, Action> skillDictionary = new Dictionary<string, Action>();
        skillDictionary.Add("Roar", () => Roar());
        skillDictionary.Add("SideRay", () => SideRay());
        skillDictionary.Add("InsideRay", () => InsideRay());
        skillDictionary.Add("Enrage", () => Enrage());
        skillDictionary.Add("InnerClap", () => InnerClap());

        RandomNumberGenerator();
    }
    public void Start()
    {

    }
    public void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        print(casting);
        RotationUpdate();
       
        switch (random)
        {
            case 0:
                Roar();
                break;
            case 1:
                SideRay();
                break;
            case 2:
                InsideRay();
                break;            
            case 3:
                InnerClap();
                break;
            case 4:
                Enrage();
                break;

        }
    }

    private void RotationUpdate()
    {
        if (casting == false)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
   
    public int RandomNumberGenerator()
    {
        if (casting == false )
        {
            if (distance < 25)
            {
                if (health > 10)
                {
                    random = 3;
                }
                else if (health<10)
                {
                    random = 4;
                }
            }
            else if (distance >= 25)
            {
                if (health > 10 )
                {
                    random = UnityEngine.Random.Range(0, 3);
                    print(random);
                }
                else
                {
                    random = 4;
                }
            }           
            
        }
        print(random);
        return random;
    }
    private void Roar()
    {
        if (casting == false)
        {
       
            bossAnim.SetTrigger("Roar");
            StartCoroutine(roarList.ActivateColliders());
            foreach(Roar item in roarList.hitboxes)
            {
                item.GetComponentInChildren<ParticleSystem>().Play();
            }

        }
        
    }
    private void SideRay()
    {
        if (casting == false)
        {
        
            bossAnim.SetTrigger("SideRay");
            StartCoroutine(sideList.ActivateColliders());
            foreach (Side item in sideList.hitboxes)
            {
                item.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
        
        
    }
    private void InsideRay()
    {
        if (casting == false)
        {
      
            bossAnim.SetTrigger("InsideRay");
            StartCoroutine(centerList.ActivateColliders());
            foreach (Center item in centerList.hitboxes)
            {
                item.GetComponentInChildren<ParticleSystem>().Play();
            }
        }

           
    }
    private void Enrage()
    {
        if (casting == false )
        {
       
            bossAnim.SetTrigger("Enrage");
            StartCoroutine(enrageList.ActivateColliders());     
            foreach (Enrage item in enrageList.hitboxes)
            {
                item.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
           
    }
   
   
    private void InnerClap()
    {
        if (casting == false)
        {
  
            bossAnim.SetTrigger("InnerClap");
            StartCoroutine(innerCircleList.ActivateColliders());
        }
                       
    }
    
    
}
public class ListGenerator<T> where T : MonoBehaviour
{
    public List<T> hitboxes = new List<T>();
    public void Gatherer()
    {
        T[] actualHitboxes = UnityEngine.Object.FindObjectsOfType<T>();
        hitboxes.AddRange(actualHitboxes);
    }

    public List<T> GetList()
    {
        return hitboxes;
    }
    
    public IEnumerator ActivateColliders()
    {
        foreach (T hitbox in hitboxes)
        {
            Collider collider = hitbox.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }

        }
        yield return new WaitForSeconds(7f);
        foreach (T hitbox in hitboxes)
        {
            Collider collider = hitbox.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;

            }
        }
    } 
}

