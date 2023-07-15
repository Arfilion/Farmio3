using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class digger : enemyEntity
{
    public SkinnedMeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        canBeHit = true;
        Find();
        Hide();
        readyToHit = false;
    }

    // Update is called once per frame
    void Update()
    {
       // walkCounter += Time.deltaTime;

        counter += Time.deltaTime;
        Hit(spawnPoint);
        Checker();
        Movement();
        if (DayNightCycle.instance.isNight == false)
        {
            Destroy(gameObject);
        }
    }
    public void Hide()
    {
        canBeHit = false;
        renderer.enabled = false;
    }
    public void Reveal()
    {
        canBeHit = true;
        renderer.enabled = true;

        GetComponent<digger>().DestroyChild();
    }
    public void DestroyChild()
    {
        // Obtener el primer objeto hijo del padre
        Transform child = transform.GetChild(0);

        if (child != null)
        {
            // Destruir el objeto hijo
            Destroy(child.gameObject);
        }
    }
}
