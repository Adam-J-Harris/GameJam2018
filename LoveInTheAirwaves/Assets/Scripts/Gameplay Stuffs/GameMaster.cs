using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public ButtonDisplayer displayer;
    public GameObject endLevelCanvas;

    public Dialogue dialogue;
    public ParticleSystem heartsPS;

    public Text endLevelText;
    public Color32 winColor;
    public Color32 loseColor;

    private bool goalReached = false;
    public bool GoalReached { get { return goalReached; } }

    private bool gameOver = false;
    public bool GameOver { get { return gameOver; } }

    void Start ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        endLevelText.text = "";
	}


    public void EndGame(string reasonForGameOver = "")
    {
        if (gameOver)
        {
            return;
        }

        endLevelCanvas.SetActive(true);

        endLevelText.text = reasonForGameOver;
        endLevelText.color = loseColor;

        gameOver = true;
    }


    public void ReachGoal(int moves)
    {
        endLevelCanvas.SetActive(true);

        endLevelText.text = "Level Complete";
        endLevelText.color = winColor;

        goalReached = true;

        heartsPS.Play();

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
