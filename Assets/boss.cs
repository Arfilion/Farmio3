using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    private bool casting;
    public Transform player;
    public float rotationSpeed = 2f;
    [SerializeField] Animator bossAnim;
    public void Awake()
    {
        casting = false;
    }
    public void Start()
    {
        // Crear un diccionario para almacenar habilidades
        Dictionary<string, Action> skillDictionary = new Dictionary<string, Action>();

        // Definir las habilidades como acciones y agregarlas al diccionario
        skillDictionary.Add("Fireball", () => Roar());
        skillDictionary.Add("Ice Blast", () => OutsideSlam());
        skillDictionary.Add("Thunderbolt", () => InsideRay());

        // Llamar a las habilidades utilizando el diccionario
        skillDictionary["Fireball"].Invoke();
        skillDictionary["Ice Blast"].Invoke();
        skillDictionary["Thunderbolt"].Invoke();
    }
    public void Update()
    {
        RotationUpdate();
        Roar();
        OutsideSlam();
        InsideRay();
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
    private  void Roar()
    {
         if (Input.GetKeyDown(KeyCode.M))
         {
            bossAnim.SetTrigger("Roar");
            casting = true;
            print("roar");
         }
    }
    private  void OutsideSlam()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            bossAnim.SetTrigger("Strike");
            casting = true;
            print("outside");
        }
    }
    private  void InsideRay()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            bossAnim.SetTrigger("Ray");
            casting = true;
            print("inside");
        }
    }
}
