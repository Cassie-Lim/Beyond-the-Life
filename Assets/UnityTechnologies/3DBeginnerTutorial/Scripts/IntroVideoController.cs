using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoUI; // Reference to the GameObject that displays the video


    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        // Load the next scene or perform any other action
        // Hide the video
        videoUI.SetActive(false);
        // SceneManager.LoadScene("SampleScene"); // Replace with your main scene name
    }
}
