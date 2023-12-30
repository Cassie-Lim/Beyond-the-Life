using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public Sprite pauseIcon;   // Reference to the pause icon
    public Sprite continueIcon; // Reference to the continue icon
    // Start is called before the first frame update
    public Button pauseButton;
    public void OnClick()
    {
       //Output to console the clicked GameObject's name and the following message.
        Debug.Log("Clicked pause button");
    }
    public void TogglePause()
    {
        Debug.Log("clicked toggle pause");
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
}
