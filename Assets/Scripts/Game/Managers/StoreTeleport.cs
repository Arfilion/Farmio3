using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTeleport : Interactable
{
    public Transform InsideSpawn;
    public Transform OutsideSpawn;

    public override void Interact()
    {
        print(this.name);
            if(this.name == "Inside")
            {
                Player.instance.transform.position = OutsideSpawn.position;
            }else if(this.name == "Outside")
            {
                Player.instance.transform.position = InsideSpawn.position;
            }
    }
}
