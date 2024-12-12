using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public void SetEasyDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 1); // Easy = 1
        PlayerPrefs.Save(); // Ensures data is written to disk
    }

    public void SetMediumDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 2); // Medium = 2
        PlayerPrefs.Save();
    }

    public void SetHardDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 3); // Hard = 3
        PlayerPrefs.Save();
    }
}
