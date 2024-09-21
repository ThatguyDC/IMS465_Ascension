using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSet : MonoBehaviour
{


    [SerializeField] public string ActiveSceneName;

    private void Start()
    {
        // Get the currently active scene
        Scene activeScene = SceneManager.GetActiveScene();

        // Set the value of ActiveSceneName to the name of the active scene
        ActiveSceneName = activeScene.name;

        // Display the name of the active scene in the console
        Debug.Log("Active Scene Name: " + ActiveSceneName);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadTitle()
    {
        SceneManager.LoadScene("TitleScreen");

    }

    public void LoadTestingGrounds()
    {
        SceneManager.LoadScene("TestingGrounds");
    }
    /*
     * Copy and paste above functions for future scenes/saves as needed.
     * 
     * 
    */

}