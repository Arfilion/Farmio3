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
    public void Awake()
    {
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



        // Crear un diccionario para almacenar habilidades
        Dictionary<string, Action> skillDictionary = new Dictionary<string, Action>();

        // Definir las habilidades como acciones y agregarlas al diccionario
        skillDictionary.Add("Fireball", () => Roar());
        skillDictionary.Add("Ice Blast", () => SideRay());
        skillDictionary.Add("Thunderbolt", () => InsideRay());

        // Llamar a las habilidades utilizando el diccionario
        skillDictionary["Fireball"].Invoke();
        skillDictionary["Ice Blast"].Invoke();
        skillDictionary["Thunderbolt"].Invoke();
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
        foreach (T hitbox in hitboxes)
        {
            Collider collider = hitbox.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
        yield return new WaitForSeconds(3f);

        foreach (T hitbox in hitboxes)
        {
            Collider collider = hitbox.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            Debug.Log("holi");
        }
    }
  
}

