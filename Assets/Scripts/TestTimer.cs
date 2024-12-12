
using UnityEngine;
using UnityEngine.UI;

//Timer code, Throw it on legacy text object

public class Timer : MonoBehaviour
{

    public PlayerTestScript Player;
    public uiManager uiScript;

    public float EasyTime = 150; //difficulty 1, 2.5 mins.
    public float MedTime = 120;  //difficulty 2, 2 mins.
    public float HardTime = 60;  //difficulty 3, 1 min.

    public float timeLeft;
    public bool counting = true;
    private Text TimerText;
    // Start is called before the first frame update
    void Start()
    {
        counting = true;
        TimerText = GetComponent<Text>();

        if (PlayerPrefs.GetInt("Difficulty") == 1) 
        {
        timeLeft = EasyTime;
        }
        else if (PlayerPrefs.GetInt("Difficulty") == 2)
        {
            timeLeft = MedTime;
        }
        else if (PlayerPrefs.GetInt("Difficulty") == 3)
        {
            timeLeft = HardTime;
        }
        else
        {
            timeLeft = 180;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (counting && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            updateTime(timeLeft);
        }

        else
        {
            timeLeft = 0;
            Player.GameOver = true;
            Cursor.lockState = CursorLockMode.None; //frees up cursor for player to click on failure screen buttons
            Debug.Log("Time is up!");
            counting = false;
        }
    }

    void updateTime(float currentTime)
    {
        TimerText.text = string.Format("Time: " + (int)currentTime);

    }
}