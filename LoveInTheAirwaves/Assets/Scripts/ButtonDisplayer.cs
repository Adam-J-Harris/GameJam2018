using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisplayer : MonoBehaviour {

    public GameObject menu;
    public GameObject nextLevel;

    public void DisplayButtons()
    {
        menu.SetActive(true);
        nextLevel.SetActive(true);
    }
}
