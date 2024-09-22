
/*
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowRealTimeSinceStartup : MonoBehaviour
{
    // Reference to the UI Text element

    public TMP_Text TimerText;

    void Update()
    {
        // Get the time since the game started
        float timeSinceStartup = Time.realtimeSinceStartup;

        // Convert the time to a readable format (e.g., minutes and seconds)
        int hours = Mathf.FloorToInt(timeSinceStartup / 3600F);
        int minutes = Mathf.FloorToInt(timeSinceStartup / 60F);
        int seconds = Mathf.FloorToInt(timeSinceStartup % 60F);
        

        // Format the time into a string
        string formattedTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        // Display the time in the UI Text element
        TimerText.text = "Run Time: " + formattedTime;
    }
}

*/

using UnityEngine;
using TMPro;

public class StartTimerOnTriggerTMP : MonoBehaviour
{
    // Reference to the TextMeshProUGUI element
    public TextMeshProUGUI timeText;

    // Variables to track time and state
    private bool timerStarted = false;
    private float startTime;

    // Start the timer when the player enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Start the timer
            timerStarted = true;
            startTime = Time.realtimeSinceStartup;  // Record the time at which the player enters
        }
    }

    void Update()
    {
        // If the timer has started, update the timer text
        if (timerStarted)
        {
            // Calculate the elapsed time since the timer started
            float timeSinceStartup = Time.realtimeSinceStartup - startTime;

            // Convert the time to a readable format (hours, minutes, seconds, and milliseconds)
            int hours = Mathf.FloorToInt(timeSinceStartup / 3600F);
            int minutes = Mathf.FloorToInt((timeSinceStartup % 3600F) / 60F);
            int seconds = Mathf.FloorToInt(timeSinceStartup % 60F);

            // Format the time into a string
            string formattedTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

            // Display the time in the TextMeshProUGUI element
            timeText.text = "Run Time: " + formattedTime;
        }
    }
}

