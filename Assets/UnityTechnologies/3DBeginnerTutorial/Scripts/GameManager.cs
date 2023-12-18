using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // public void LoadLevel(int levelIndex)
    // {
    //     SceneManager.LoadScene(levelIndex);
    // }
    // Function to toggle pause
    public Button pauseButton; // Reference to the pause button
    public Sprite pauseIcon;   // Reference to the pause icon
    public Sprite continueIcon; // Reference to the continue icon

    // Function to toggle pause
    public void TogglePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0; // Pauses the game
            pauseButton.image.sprite = continueIcon; // Change to continue icon
        }
        else
        {
            Time.timeScale = 1; // Resumes the game
            pauseButton.image.sprite = pauseIcon; // Change back to pause icon
        }
    }
    // Function to exit the game
    public void ExitGame()
    {
        Application.Quit();
        // Note: Application.Quit() does not work in the Unity editor. 
        // Use #if UNITY_EDITOR preprocessor directive if needed.
    }
}
