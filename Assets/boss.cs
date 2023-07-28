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
    public static boss instance;

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



        Dictionary<string, Action> skillDictionary = new Dictionary<string, Action>();

        skillDictionary.Add("Roar", () => Roar());
        skillDictionary.Add("SideRay", () => SideRay());
        skillDictionary.Add("InsideRay", () => InsideRay());

        skillDictionary["Roar"].Invoke();
        skillDictionary["SideRay"].Invoke();
        skillDictionary["InsideRay"].Invoke();
    }
    public void Update()
    {
        counter += Time.deltaTime;
        RotationUpdate();
        Roar();
        SideRay();
        InsideRay();
        StateChecker(state);

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

    // Métodos que representan las habilidades
    private void Roar()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            counter = 0;
            counter += Time.deltaTime;
            casting = true;
            state = durationRoar;
            bossAnim.SetTrigger("Roar");
            StartCoroutine(roarList.ActivateColliders());
        }
    }
    private void SideRay()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            counter = 0;
            counter += Time.deltaTime;
            casting = true;
            state = durationOutside;
            bossAnim.SetTrigger("SideRay");
            StartCoroutine(sideList.ActivateColliders());
        }
    }
    private void InsideRay()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            counter = 0;
            counter += Time.deltaTime;
            casting = true;
            state = durationInside;
            bossAnim.SetTrigger("InsideRay");
            StartCoroutine(centerList.ActivateColliders());


        }
    }
    public void StateChecker(float _state)
    {
        if (counter >= state)
        {
            counter = 0;
            casting = false;
            counter += Time.deltaTime;
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
        yield return new WaitForSeconds(3f);
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

