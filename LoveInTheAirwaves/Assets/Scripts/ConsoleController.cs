using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleController : MonoBehaviour {

    public Text printToConsole;

    public PlayerController playerController;

    private bool closed;

    private int counter = 0;

	// Use this for initialization
	void Start () {

        closed = false;

        counter = 0;

        printToConsole.text = "Commands To Perform:" + "\n";
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.C) && closed == true)
        {
            printToConsole.enabled = true;
            closed = false;
        }
        else if (Input.GetKeyDown(KeyCode.C) && closed == false)
        {
            printToConsole.enabled = false;
            closed = true;
        }
    }

    public void Liststuff()
    {
        printToConsole.text = "Commands To Perform:" + "\n";

        for (int i = 0; i < playerController.GetActionListSize(); i++)
        {
            counter = i + 1;

            Action action = playerController.GetActionFromList(i);

            switch (action)
            {
                case Action.MOVE_UP:
                    printToConsole.text = printToConsole.text + counter.ToString() + " - MOVE UP " + "\n";                    
                    break;

                case Action.MOVE_DOWN:
                    printToConsole.text = printToConsole.text + counter.ToString() + " - MOVE DOWN" + "\n";
                    break;

                case Action.MOVE_LEFT:
                    printToConsole.text = printToConsole.text + counter.ToString() + " - MOVE LEFT" + "\n";
                    break;

                case Action.MOVE_RIGHT:
                    printToConsole.text = printToConsole.text + counter.ToString() + " - MOVE RIGHT" + "\n";
                    break;
            }
        }
    }  
}
