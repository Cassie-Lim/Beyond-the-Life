using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using System.Collections;

public class IntroVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource; // Reference to the separate AudioSource for echo effect
    public GameObject videoUI; // Reference to the GameObject that displays the video
    public float fadeDuration = 2.0f; // Duration of the fade
    public float echoDelay = 0.5f; // Duration of the fade

    private CanvasGroup videoCanvasGroup; // Canvas Group for fading
    void Start()
    {
        // Ensure you have a CanvasGroup attached to the videoUI GameObject
        videoCanvasGroup = videoUI.GetComponent<CanvasGroup>();
        if (videoCanvasGroup == null) {
            Debug.LogError("CanvasGroup component missing on videoUI GameObject");
            return;
        }

        videoPlayer.loopPointReached += EndReached;
        videoPlayer.prepareCompleted += VideoPrepared;
        videoPlayer.Prepare();

    }
    void VideoPrepared(VideoPlayer vp)
    {
        new WaitForSeconds(echoDelay);
        audioSource.Play(); // Play the audio when the video is ready
    }
    void EndReached(VideoPlayer vp)
    {
        StartCoroutine(FadeOutVideo());
    }

    IEnumerator FadeOutVideo()
    {
        float elapsedTime = 0.0f;

        // Fade out
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            videoCanvasGroup.alpha = 1.0f - (elapsedTime / fadeDuration);
            yield return null;
        }

        videoUI.SetActive(false);
        // SceneManager.LoadScene("SampleScene"); // Replace with your main scene name
    }
}