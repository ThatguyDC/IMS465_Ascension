using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class uiManager : MonoBehaviour
{

    [Header("Script Comms")]

    public PlayerTestScript PlayerScript;
    public DifficultyManager DifficultyScript;
    public SceneSet SceneScript;

    [Header("Difficulties")]
   
    //Difficulties
    public GameObject EasyButton;
    public GameObject RuggedButton;
    public GameObject ExpertButton;

    [Header("Medal Placeholders")]
    //Medal Placeholders

    public GameObject EasyPH;
    public GameObject MedPH;
    public GameObject HardPH;


    [Header("Medals")]
    //Medals
    public GameObject EasyMedal;
    public GameObject MedMedal;
    public GameObject HardMedal;


    [Header("Start/Quit")]
    //Game Start/Quit
    public GameObject NewGameButton;
    public GameObject QuitButton;


    [Header("Pause UI")]

    public GameObject PauseMenu;
    public TMP_Text DifficultyLabel;
    private int DifficultyNum;


    [Header("Fail UI")]
    public GameObject FailMenu;


    [Header("Mechanics UI")]
    //Gameplay UI

    public TMP_Text GoldCounter;
    private GameObject Interact;



    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
        UpdateDifficulty();
        if (SceneScript.ActiveSceneName == "TitleScreen")
        {
            UpdateMedals(); //checks player prefs for medal conditions being met in-game
        }
        else
        {
            //We are in-game, and medals can't be updated.
        }
        OpenMenu();
    }

    private void OpenMenu()
    {
        if (Input.GetKey(PlayerScript.PauseKey)) //open menu
        {
            //on key click 
            //unlock cursor
            //toggle main menu on
            //on button click 
            
            Cursor.lockState = CursorLockMode.None; //lock the cursor in the game window
            PauseMenu.SetActive(true);
        }
    }



    private void UpdateText()
    {
        if (GoldCounter != null) {
            GoldCounter.text = "Minerals Collected: " + PlayerScript.collectableCount.ToString(); // Update the TextMeshPro text
        }
        else
        {
            //technically an error, but it's fine for this project
        }
    }

    private void UpdateDifficulty()
    {

        DifficultyNum = PlayerPrefs.GetInt("Difficulty");

        if (DifficultyLabel != null)
        {
            if (DifficultyNum == 1) //easy
            {
                DifficultyLabel.text = ("Selected: " + "Greenhorn");

            }
            else if (DifficultyNum == 2) //medium
            {
                DifficultyLabel.text = ("Selected: " + "Rugged");

            }
            else if (DifficultyNum == 3) //hard
                {
                DifficultyLabel.text = ("Selected: " + "Expert");
            }
        }
        else
        {
            //technically an error, but it's fine for this project
        }
    }

    private void UpdateMedals()
    {
        

        if (PlayerPrefs.GetInt("EasyMedalWon") == 1)
        {
            EasyPH.SetActive(false);
            EasyMedal.SetActive(true);

        }
        else
        {
            //no medal earned
        }
        if (PlayerPrefs.GetInt("MedMedalWon") == 1)
        {
            MedPH.SetActive(false);
            MedMedal.SetActive(true);
        }
        else
        {
            //no medal earned
        }
        if (PlayerPrefs.GetInt("HardMedalWon") == 1)
        {
            HardPH.SetActive(false);
            HardMedal.SetActive(true);
            
        }
        else
        {
            //no medal earned
        }

    }


        //Quit and Resume

        public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; //lock the cursor in the game window
        PlayerScript.IsPaused = false;
        PlayerScript.InputDisabled = false;
    }

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
