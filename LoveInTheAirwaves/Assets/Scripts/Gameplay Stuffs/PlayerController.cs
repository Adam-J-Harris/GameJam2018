using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    MOVE_UP,
    MOVE_DOWN,
    MOVE_LEFT,
    MOVE_RIGHT
}

public class PlayerController : MonoBehaviour {

    public ConsoleController consoleController;
    public GameObject controlsCanvas;
    public GameObject consoleCanvas;
    public Dialogue dialogue;

    public PlayerMovement playerMovement;
    private List<Action> playerActions;


    public AudioClip beep;
    public AudioClip happyBeep;
    public AudioClip undoBeep;

    private AudioSource BeeperFX;


    void Start ()
    {
        playerActions = new List<Action>();
        BeeperFX = GetComponent<AudioSource>();
    }

    public void DialogueStarter()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void AddMovementZ(bool up)
    {
        switch (up)
        {
            case true:
                playerActions.Add(Action.MOVE_UP);
                consoleController.Liststuff();
                BeeperFX.PlayOneShot(beep, 0.7f);
                break;

            case false:
                playerActions.Add(Action.MOVE_DOWN);
                consoleController.Liststuff();
                BeeperFX.PlayOneShot(beep, 0.7f);
                break;
        }
    }

    public void AddMovementX(bool left)
    {
        switch (left)
        {
            case true:
                playerActions.Add(Action.MOVE_LEFT);
                consoleController.Liststuff();
                BeeperFX.PlayOneShot(beep, 0.7f);
                break;

            case false:
                playerActions.Add(Action.MOVE_RIGHT);
                consoleController.Liststuff();
                BeeperFX.PlayOneShot(beep, 0.7f);
                break;
        }
    }

    public void ExecuteMovement()
    {
        BeeperFX.PlayOneShot(happyBeep, 0.7f);
        controlsCanvas.SetActive(false);
        consoleCanvas.SetActive(false);
        StartCoroutine(playerMovement.PerformNextAction());
        
    }

    public void Undo()
    {
        BeeperFX.PlayOneShot(undoBeep, 0.7f);
        playerActions.Clear();
        consoleController.Liststuff();
        
    }


    public int GetActionListSize()
    {
        return playerActions.Count;
    }

    public Action GetActionFromList(int i)
    {
        return playerActions[i];
    }
}
