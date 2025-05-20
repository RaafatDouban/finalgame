using UnityEngine;

public enum Difficulty { Easy, Medium, Hard }

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;
    public Difficulty CurrentDifficulty { get; private set; } = Difficulty.Easy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("DifficultyManager created and set to: " + CurrentDifficulty);
        }
        else
        {
            Debug.Log("Duplicate DifficultyManager destroyed");
            Destroy(gameObject);
        }
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        CurrentDifficulty = difficulty;
        Debug.Log("Difficulty set to: " + difficulty);
    }

    public static void EnsureExists()
    {
        if (Instance == null)
        {
            GameObject prefab = Resources.Load<GameObject>("DifficultyManager");
            if (prefab != null)
            {
                Instantiate(prefab);
            }
            else
            {
                Debug.LogError("DifficultyManager prefab not found in Resources!");
            }
        }
    }
}
