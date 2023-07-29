using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public bool casting;
    public float durationRoar;
    public float durationInside;
    public float durationOutside;
    public float counter;
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
    [SerializeField] ListGenerator<OuterCircle> outerCircleList = new ListGenerator<OuterCircle>();

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
        durationRoar = 11f;
        durationInside = 6.66f;
        durationOutside = 7f;
        casting = false;
    }
    public void Start()
    {

        roarList.Gatherer();
        sideList.Gatherer();
        centerList.Gatherer();
        enrageList.Gatherer();
        innerCircleList.Gatherer();
        outerCircleList.Gatherer();




        Dictionary<string, Action> skillDictionary = new Dictionary<string, Action>();

        skillDictionary.Add("Roar", () => Roar());
        skillDictionary.Add("SideRay", () => SideRay());
        skillDictionary.Add("InsideRay", () => InsideRay());
        skillDictionary.Add("Enrage", () => Enrage());
        skillDictionary.Add("InnerClap", () => InnerClap());
        skillDictionary.Add("OuterClap", () => OuterClap());


        skillDictionary["Roar"].Invoke();
        skillDictionary["SideRay"].Invoke();
        skillDictionary["InsideRay"].Invoke();
        skillDictionary["Enrage"].Invoke();
        skillDictionary["InnerClap"].Invoke();
        skillDictionary["OuterClap"].Invoke();


    }
    public void Update()
    {
        RandomNumberGenerator();
        print(casting);
        counter += Time.deltaTime;
        RotationUpdate();
        StateChecker(state);
       
        switch (random)
        {
            case 1:
                Roar();
                break;
            case 2:
                SideRay();
                break;
            case 3:
                InsideRay();
                break;
            case 4:
                OuterClap();
                break;
            case 5:
                InnerClap();
                break;
            case 6:
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
            random = UnityEngine.Random.Range(0, 7);
        }
        return random;
    }
    private void Roar()
    {
       
            counter = 0;
            counter += Time.deltaTime;
            state = durationRoar;
            bossAnim.SetTrigger("InsideRay");
            StartCoroutine(roarList.ActivateColliders(durationRoar));
        
    }
    private void SideRay()
    {
        
            counter = 0;
            counter += Time.deltaTime;
            state = durationOutside;
            bossAnim.SetTrigger("SideRay");
            StartCoroutine(sideList.ActivateColliders(durationRoar));
        
        
    }
    private void InsideRay()
    {
            counter = 0;
            counter += Time.deltaTime;
            state = durationInside;
            bossAnim.SetTrigger("InsideRay");
            StartCoroutine(centerList.ActivateColliders(durationRoar));
           
    }
    private void Enrage()
    {
        
            counter = 0;
            counter += Time.deltaTime;
            state = durationInside;
            bossAnim.SetTrigger("InsideRay");
            StartCoroutine(enrageList.ActivateColliders(durationRoar));
           
    }
   
    private void OuterClap()
    {
        
            counter = 0;
            counter += Time.deltaTime;
            state = durationInside;
            bossAnim.SetTrigger("SideRay");
            StartCoroutine(innerCircleList.ActivateColliders(durationRoar));
                
    }
    private void InnerClap()
    {
        
            counter = 0;
            counter += Time.deltaTime;
            state = durationInside;
            bossAnim.SetTrigger("InsideRay");
            StartCoroutine(outerCircleList.ActivateColliders(durationRoar));
                       
    }
    public void StateChecker(float _state)
    {
        if (counter >= state)
        {
            counter = 0;
            counter += Time.deltaTime;
        }
    }
    public enum AttackSequence
    {
        Roar=1,
        SideRay=2,
        InsideRay=3,
        InnerClap=4,
        OuterClap=5,
        Enrage=6
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
    public IEnumerator ActivateColliders(float duration)
    {
        yield return new WaitForSeconds(7f);
        foreach (T hitbox in hitboxes)
        {
            Collider collider = hitbox.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }

        }
        boss.instance.casting = true;
        yield return new WaitForSeconds(0.5f);
        foreach (T hitbox in hitboxes)
        {
            Collider collider = hitbox.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;

            }
        }
        yield return new WaitForSeconds(7f);
        boss.instance.casting = false;
    }
    
    public class BossStats
    {
        private int damageRoar;
        private int damageSideRay;
        private int damageInsideRay;
        private int health;

        public int DamageRoar
        {
            get { return damageRoar; }
            private set { damageRoar = Mathf.Max(0, value); }
        }

        public int DamageSideRay
        {
            get { return damageSideRay; }
            private set { damageSideRay = Mathf.Max(0, value); }
        }

        public int DamageInsideRay
        {
            get { return damageInsideRay; }
            private set { damageInsideRay = Mathf.Max(0, value); }
        }

        public int Health
        {
            get { return health; }
            private set { health = Mathf.Max(0, value); }
        }

        public BossStats(int fireballDamage, int iceBlastDamage, int thunderboltDamage, int initialHealth)
        {
            DamageRoar = damageRoar;
            DamageSideRay = damageSideRay;
            DamageInsideRay = damageInsideRay;
            Health = initialHealth;
        }
    }
   
}

