using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public struct Boss
{
    public string Name;
    public int Damage;
    public float RotationSpeed;
    public int Health;

    public Boss(string name, int damage, float rotationSpeed, int health)
    {
        Name = name;
        Damage = damage;
        RotationSpeed = rotationSpeed;
        Health = health;
    }
}

public class boss : MonoBehaviour
{
    public float counter;
    public int damage;
    public bool casting;
    public bool transitionCasting;
    private float rotationSpeed;
    public float roarTimming;
    public float insideRayTimming=8f;
    public float sideRayTimming;
    public float inncerCircleTimming;
    public float enrageTimming;
    [SerializeField] Animator bossAnim;
    [SerializeField] ListGenerator<Center> centerList = new ListGenerator<Center>();
    [SerializeField] ListGenerator<Side> sideList = new ListGenerator<Side>();
    [SerializeField] ListGenerator<Roar> roarList = new ListGenerator<Roar>();
    [SerializeField] ListGenerator<Enrage> enrageList = new ListGenerator<Enrage>();
    [SerializeField] ListGenerator<InnerCircle> innerCircleList = new ListGenerator<InnerCircle>();
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject strikePrefab;
    [SerializeField] GameObject poisonPrefab;
    public float distance;
    public static boss instance;
    public int health;
    public int random;
    public delegate void BossAttacksHandler();
    public event BossAttacksHandler RoarSound;
    public event BossAttacksHandler SideSound;
    public event BossAttacksHandler InsideSound;
    public event BossAttacksHandler EnrageSound;
    public event BossAttacksHandler SwingSound;




    public int Health
    {
        get { return health; }
        set
        {
            if (value < 0)
            {
                health = 0;
            }
            else if (value > 5000)
            {
                health = 5000;
            }
            else
            {
                health = value;
            }
        }
    }
    public float RotationSpeed
    {
        get { return rotationSpeed; }
        set { rotationSpeed = value; }
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public void Awake()
    {
    }
    public void Start()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        Player player = FindObjectOfType<Player>().GetComponent<Player>();
        casting = false;
        transitionCasting = false;
        Boss boss = new Boss("Boss", 10, 2f, 5001);

        Health = boss.Health;
        name = boss.Name;
        RotationSpeed = boss.RotationSpeed;
        damage = boss.Damage;
        counter = 0;
        distance = Vector3.Distance(transform.position, player.transform.position);
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
    public void Update()
    {
        
        
        Player player = FindObjectOfType<Player>().GetComponent<Player>();

        distance = Vector3.Distance(transform.position, player.transform.position);
        RotationUpdate();
        Attacks attacksRandom = (Attacks)random;
        switch (attacksRandom)
        {
            case Attacks.Roar:
                Roar();
                if (casting != transitionCasting)
                {
                    transitionCasting = casting;
                }
                break;
            case Attacks.SideRay:
                SideRay();
                if (casting != transitionCasting)
                {
                    transitionCasting = casting;
                }
                break;
            case Attacks.InsideRay:
                InsideRay();
                if (casting != transitionCasting)
                {
                    transitionCasting = casting;
                }
                break;
            case Attacks.InnerClap:
                InnerClap();
                if (casting != transitionCasting)
                {
                    transitionCasting = casting;
                }
                break;
            case Attacks.Enrage:
                Enrage();
                if (casting != transitionCasting)
                {
                    transitionCasting = casting;
                }
                break;

        }
    }

    private void RotationUpdate()
    {
        if (casting == false)
        {
            Player player = FindObjectOfType<Player>().GetComponent<Player>();

            Vector3 directionToPlayer = player.transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public int RandomNumberGenerator()
    {
        if (casting == false)
        {
            if (distance < 25)
            {
                if (health > 10)
                {
                    random = 3;
                }
                else if (health < 10)
                {
                    random = 4;
                }
            }
            else if (distance >= 25)
            {
                if (health > 10)
                {
                    random = UnityEngine.Random.Range(0, 3);
                }
                else
                {
                    random = 4;
                }
            }

        }
        counter = 0;

        return random;
    }
    private void Roar()
    {
        
        if (casting == false)
        {

            bossAnim.SetTrigger("Roar");
            
            foreach (Roar item in roarList.hitboxes)
            {
                item.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
        else
        {
            if (counter > roarTimming)
            {
                RoarSound?.Invoke();
                StartCoroutine(roarList.ActivateColliders());
            }
        }
    }
    private void SideRay()
    {
        if (casting == false)
        {

            bossAnim.SetTrigger("SideRay");        
            foreach (Side item in sideList.hitboxes)
            {
                item.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
        else
        {
            if (counter > sideRayTimming)
            {
                SideSound?.Invoke();
                StartCoroutine(sideList.ActivateColliders());
            }
        }
    }
    private void InsideRay()
    {
        if (casting == false)
        {
            bossAnim.SetTrigger("InsideRay");
            

            foreach (Center item in centerList.hitboxes)
            {
                item.GetComponentInChildren<ParticleSystem>().Play();


            }
        }
        else
        {
            if (counter > insideRayTimming)
            {
                InsideSound?.Invoke();
                StartCoroutine(centerList.ActivateColliders());
            }          
        }
    }
    private void Enrage()
    {
        if (casting == false)
        {
            bossAnim.SetTrigger("Enrage");
            

            enrageList.ActivateColliders();
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
       

            innerCircleList.ActivateColliders();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Bullet bulletPrefab = collision.gameObject.GetComponent<Bullet>();
        Poison poisonPrefab = collision.gameObject.GetComponent<Poison>();
        strike strikePrefab = collision.gameObject.GetComponent<strike>();
       
        if (bulletPrefab)
        {
            TakeDamage2(100);
        }
        else if (poisonPrefab)
        {

            TakeDamage2(1);

        }
        else if (strikePrefab)

        {
            TakeDamage2(175);
        }


    }
    public void TakeDamage2(int _damage)
    {
        
            health -= _damage;
            if (health <= 0)
            {
            SceneManager.LoadScene(4);
            }
        
    }
   
}
public enum Attacks
{
    Roar=0,
    SideRay=1,
    InsideRay=2,
    InnerClap=3,
    Enrage=4
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
        yield return new WaitForSeconds(0.5f);
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






