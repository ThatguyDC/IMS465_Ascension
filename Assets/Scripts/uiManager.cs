using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiManager : MonoBehaviour
{

    [Header("Main Menu UI")]
   
    //Difficulties
    public GameObject EasyButton;
    public GameObject RuggedButton;
    public GameObject ExpertButton;

    //Game Start/Quit
    public GameObject NewGameButton;
    public GameObject QuitButton;




    [Header("Mechanics UI")]
    private GameObject Interact;




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
