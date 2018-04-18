using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            GameMaster.instance.ReachGoal(playerController.GetActionListSize());
            GetComponent<Animator>().SetTrigger("levelComp");
        }
    }

    public void KillPlayer()
    {
        GetComponent<Animator>().SetTrigger("death");
        GameMaster.instance.EndGame("Oh no! The Turret Shot Buddy!");
    }
}
