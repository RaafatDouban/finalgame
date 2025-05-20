using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;
    public Transform player;
    public int score = 0;
    public int levelitems;


    private void Awake()
    {


        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        // Level 1: go to Level 2 at 1000
        if (currentScene == 1 && score >= 800)
        {
            SceneManager.LoadScene(2);
        }
        // Level 2: go to Level 3 at 1400
        else if (currentScene == 2 && score >= 1200)
        {
            SceneManager.LoadScene(3);
        }
        // Level 3: go to Main Menu at 1800
        else if (currentScene == 3 && score >= 2000)
        {
            SceneManager.LoadScene(0);
        }
    }
}
