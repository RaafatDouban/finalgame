using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    // Scene indices - update these numbers to match your Build Settings order
    private const int MAIN_MENU_INDEX = 0;
    private const int OPTIONS_INDEX = 4;
    private const int LEVEL_1_INDEX = 1;
    private const int LEVEL_2_INDEX = 2;
    private const int LEVEL_3_INDEX = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Any initialization code can go here
    }

    // Update is called once per frame
    void Update()
    {
        // Any update code can go here
    }

    // Load Easy Level (Level 1)
    public void StartEasyGame()
    {
        DifficultyManager.Instance.SetDifficulty(Difficulty.Easy);
        SceneManager.LoadScene(LEVEL_1_INDEX);
    }

    // Load Medium Level (Level 2)
    public void StartMediumGame()
    {
        DifficultyManager.Instance.SetDifficulty(Difficulty.Medium);
        SceneManager.LoadScene(LEVEL_2_INDEX);
    }

    // Load Hard Level (Level 3)
    public void StartHardGame()
    {
        DifficultyManager.Instance.SetDifficulty(Difficulty.Hard);
        SceneManager.LoadScene(LEVEL_3_INDEX);
    }

    // Load Options Menu
    public void Openlevels()
    {
        SceneManager.LoadScene(OPTIONS_INDEX);
    }

    // Return to Main Menu
    public void QuitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
