using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single); // Load menu scene
    }

}
