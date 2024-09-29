using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Timer code, Throw it on legacy text object

public class Timer : MonoBehaviour
{

    public float timeLeft = 180;
    public bool counting = true;
    private Text TimerText;
    // Start is called before the first frame update
    void Start()
    {
        counting = true;
        TimerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counting && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            updateTime(timeLeft);
        }//else{
        //     timeLeft = 0;
        //     Debug.Log("Time is up!");
        //     counting = false;
        // }
    }

    void updateTime(float currentTime)
    {
        TimerText.text = string.Format("Time: " + (int)currentTime);

    }
}