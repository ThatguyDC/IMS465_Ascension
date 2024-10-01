using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uiManager : MonoBehaviour
{

    [Header("Script Comms")]

    public PlayerTestScript PlayerScript;


    [Header("Main Menu UI")]
   
    //Difficulties
    public GameObject EasyButton;
    public GameObject RuggedButton;
    public GameObject ExpertButton;

    //Game Start/Quit
    public GameObject NewGameButton;
    public GameObject QuitButton;


    //Gameplay UI
    public TMP_Text GoldCounter;




    [Header("Mechanics UI")]
    private GameObject Interact;




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        GoldCounter.text = "Gold Collected: " + PlayerScript.collectableCount.ToString(); // Update the TextMeshPro text
    }

    //Quit 
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowUI(GameObject uiElement) //takes game obj as arg to display 
    {
        uiElement.SetActive(true);  
    }

    public void HideUI(GameObject uiElement)
    {
        uiElement.SetActive(false);
    }
}
