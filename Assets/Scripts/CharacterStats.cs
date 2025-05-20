using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Maximum health
    [SerializeField] private int killscore = 200;
    [SerializeField] public float power = 10f;
    [SerializeField] private int currentHealth; // This will show in Inspector

    public int CurrentHealth => currentHealth;
    private Animator animator;

    [SerializeField] private HealthBar healthBar; // Reference to the HealthBar

    void Start()
    {
        DifficultyManager.EnsureExists();
        // Set stats based on difficulty if this is the player
        if (gameObject.CompareTag("Player"))
        {
            switch (DifficultyManager.Instance.CurrentDifficulty)
            {
                case Difficulty.Easy:
                    maxHealth = 200;
                    power = 20f;
                    break;
                case Difficulty.Medium:
                    maxHealth = 100;
                    power = 10f;
                    break;
                case Difficulty.Hard:
                    maxHealth = 90;
                    power = 7f;
                    break;
            }
        }
        currentHealth = maxHealth; // Initialize health
        animator = GetComponentInChildren<Animator>(); // Get the Animator component

        // Initialize the HealthBar
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health stays within bounds
        Debug.Log($"{gameObject.name} Health: {currentHealth}");

        // Update the HealthBar
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public float GetPower()
    {
        return power; // Return the power value for damage calculation
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        // Trigger the Die animation
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Wait for 1 second before destroying the GameObject and loading main menu if player
        StartCoroutine(HandleDeath());
    }

    private System.Collections.IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(1f);

        if (gameObject.CompareTag("Player"))
        {
            int currentScene = 0;
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene() != null)
                currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            if (currentScene != 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            if (LevelManager.Instance != null)
                LevelManager.Instance.score += killscore;
            Destroy(gameObject);
            yield return null;
            // Only call if EnemyController class exists and method is accessible
            if (System.Type.GetType("EnemyController") != null)
            {
                // Use reflection to call static method if it exists
                var method = System.Type.GetType("EnemyController").GetMethod("CheckAllEnemiesDead");
                if (method != null)
                    method.Invoke(null, null);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is tagged as "Health"
        if (other.CompareTag("Health"))
        {
            // Ensure this script is attached to the player
            if (gameObject.CompareTag("Player"))
            {
                Debug.Log("Health Picked Up by Player");
                Destroy(other.gameObject); // Destroy the health pickup
                ChangeHealth(20); // Increase health by 20
                if (healthBar != null)
                {
                    healthBar.SetHealth(currentHealth); // Update the health bar
                }
                Debug.Log("Player Health: " + currentHealth);
            }
        }
    }
}
