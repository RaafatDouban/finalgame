using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu panel
    public static bool GameIsPaused = false;
    private const int MAIN_MENU_INDEX = 0;
    private const int LEVEL_1_INDEX = 1;
    private const int LEVEL_2_INDEX = 2;
    private const int LEVEL_3_INDEX = 3;
    private const int OPTIONS_INDEX = 4;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Load the main menu scene (index 0)
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void StartEasyGame()
{
    DifficultyManager.Instance.SetDifficulty(Difficulty.Easy);
    SceneManager.LoadScene(LEVEL_1_INDEX);
}

public void StartMediumGame()
{
    DifficultyManager.Instance.SetDifficulty(Difficulty.Medium);
    SceneManager.LoadScene(LEVEL_2_INDEX);
}

public void StartHardGame()
{
    DifficultyManager.Instance.SetDifficulty(Difficulty.Hard);
    SceneManager.LoadScene(LEVEL_3_INDEX);
}
}