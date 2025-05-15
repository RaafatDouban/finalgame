using UnityEngine;

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
        
    }
}
