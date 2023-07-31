using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontInterrupt : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.instance.casting = true;
       
    }



    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.instance.casting = false;
        boss.instance.random = boss.instance.RandomNumberGenerator();
    }
}
