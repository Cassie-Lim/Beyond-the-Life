using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // public void LoadLevel(int levelIndex)
    // {
    //     SceneManager.LoadScene(levelIndex);
    // }
    // Function to toggle pause
    // public Button pauseButton; // Reference to the pause button
    // public Sprite pauseIcon;   // Reference to the pause icon
    // public Sprite continueIcon; // Reference to the continue icon

    // Reference to the player GameObjects. Assign these in the inspector or find them at runtime.
    public List<GameObject> players; 


    public static GameManager Instance {get; private set;}
    private bool isLoadingSavedGame = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Setting GameManager instance");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Destroy GameManager instance");
        }
    }
    // Function to toggle pause
    // public void TogglePause()
    // {
    //     Debug.Log("clicked toggle pause");
    //     if (Time.timeScale == 1)
    //     {
    //         Time.timeScale = 0; // Pauses the game
    //         pauseButton.image.sprite = continueIcon; // Change to continue icon
    //     }
    //     else
    //     {
    //         Time.timeScale = 1; // Resumes the game
    //         pauseButton.image.sprite = pauseIcon; // Change back to pause icon
    //     }
    // }
    // Function to exit the game
    // public void ExitGame()
    // {
    //     Application.Quit();
    //     // Note: Application.Quit() does not work in the Unity editor. 
    //     // Use #if UNITY_EDITOR preprocessor directive if needed.
    // }
    // // Call this method to save the current game state
    void Start()
    {
        // Find all player objects in the scene (you could use tags, names, etc.)
        players = GameObject.FindGameObjectsWithTag("Player")
             .OrderBy(go => go.name)
             .ToList();

    }

    // ... (Other methods)

    public void SaveGameState()
    {
        PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex);

        // Save each player's position and direction
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
            {
                PlayerPrefs.SetFloat("PlayerPosX_" + i, players[i].transform.position.x);
                PlayerPrefs.SetFloat("PlayerPosY_" + i, players[i].transform.position.y);
                PlayerPrefs.SetFloat("PlayerPosZ_" + i, players[i].transform.position.z);
                PlayerPrefs.SetFloat("PlayerDirX_" + i, players[i].transform.forward.x);
                PlayerPrefs.SetFloat("PlayerDirY_" + i, players[i].transform.forward.y);
                PlayerPrefs.SetFloat("PlayerDirZ_" + i, players[i].transform.forward.z);
            }
        }

        PlayerPrefs.Save();
    }

    public void LoadGameState()
    {
        isLoadingSavedGame = true; // Set the flag
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1);
        SceneManager.LoadScene(savedLevel, LoadSceneMode.Single);
        // Load each player's position and direction after the scene has loaded
        // This part will likely need to be called after the scene has finished loading
    }
    public void StartNewGame()
    {
        isLoadingSavedGame = false; // Set the flag
        SceneManager.LoadScene("Level0", LoadSceneMode.Single);
        // Load each player's position and direction after the scene has loaded
        // This part will likely need to be called after the scene has finished loading
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!isLoadingSavedGame) return;
        // Assuming you have a method to find or reassign the player references in the new scene
        players = GameObject.FindGameObjectsWithTag("Player")
             .OrderBy(go => go.name)
             .ToList();

        for (int i = 0; i < players.Count; i++)
        {
            GameObject player = players[i];
            if (player != null)
            {
                Vector3 position = new Vector3(
                    PlayerPrefs.GetFloat("PlayerPosX_" + i, 0),
                    PlayerPrefs.GetFloat("PlayerPosY_" + i, 0),
                    PlayerPrefs.GetFloat("PlayerPosZ_" + i, 0)
                );

                Vector3 direction = new Vector3(
                    PlayerPrefs.GetFloat("PlayerDirX_" + i, 0),
                    PlayerPrefs.GetFloat("PlayerDirY_" + i, 0),
                    PlayerPrefs.GetFloat("PlayerDirZ_" + i, 1)
                );

                player.transform.position = position;
                player.transform.forward = direction;
            }
        }
        isLoadingSavedGame = true;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
