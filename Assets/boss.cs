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
    public float state;
    public Transform player;
    public float rotationSpeed = 2f;
    [SerializeField] Animator bossAnim;
    public List<GameObject> prefabList = new List<GameObject>();
    [SerializeField] ListGenerator<Center> centerList = new ListGenerator<Center>();
    [SerializeField] ListGenerator<Side> sideList = new ListGenerator<Side>();
    [SerializeField] ListGenerator<Roar> roarList = new ListGenerator<Roar>();
    [SerializeField] ListGenerator<Enrage> enrageList = new ListGenerator<Enrage>();
    [SerializeField] ListGenerator<InnerCircle> innerCircleList = new ListGenerator<InnerCircle>();

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
        RandomNumberGenerator();
        casting = false;
        Boss boss = new Boss("Boss",100);
        health = boss.Health;
        name = boss.Name;
    }
    public void Start()
    {

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


        


    }
    public void Update()
    {
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
            Vector3 directionToPlayer = player.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
   
    public int RandomNumberGenerator()
    {
        if (casting == false)
        {
            if (health > 10)
            {
                random = UnityEngine.Random.Range(0, 3);
                print(random);
            }
            else
            {
                random = 4;
            }
            
        }
        return random;
    }
    private void Roar()
    {
        if (casting == false)
        {
       
            bossAnim.SetTrigger("Roar");
            StartCoroutine(roarList.ActivateColliders());

        }
        
    }
    private void SideRay()
    {
        if (casting == false)
        {
        
            bossAnim.SetTrigger("SideRay");
            StartCoroutine(sideList.ActivateColliders());
        }
        
        
    }
    private void InsideRay()
    {
        if (casting == false)
        {
      
            bossAnim.SetTrigger("InsideRay");
            StartCoroutine(centerList.ActivateColliders());
        }
           
    }
    private void Enrage()
    {
        if (casting == false )
        {
       
            bossAnim.SetTrigger("Enrage");
            //StartCoroutine(enrageList.ActivateColliders());
        }
           
    }
   
   
    private void InnerClap()
    {
        if (casting == false)
        {
  
            bossAnim.SetTrigger("InnerClap");
            //StartCoroutine(outerCircleList.ActivateColliders());
        }
                       
    }
    
    public enum AttackSequence
    {
        Roar=1,
        SideRay=2,
        InsideRay=3,
        InnerClap=4,
        Enrage=5
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

