using UnityEngine;
using UnityEngine.AI;
using System.Collections;  // Add this for IEnumerator
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    private bool isDead = false;
    private NavMeshAgent agent;
    public float attackRange = 10f;
    public Animator animator;
    bool CanAttack = true;
    public float attackCooldown = 3f;
    CharacterStats characterStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterStats = GetComponent<CharacterStats>();
        animator = GetComponentInChildren<Animator>();
        // Find player if not assigned
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
        Debug.Log("[EnemyController] Start called on: " + gameObject.name);

        switch (DifficultyManager.Instance.CurrentDifficulty)
        {
            case Difficulty.Easy:

                characterStats.power = 5f; // Lower damage
                agent.speed = 2f; // Slower
                break;
            case Difficulty.Medium:

                characterStats.power = 10f;
                agent.speed = 3.5f;
                break;
            case Difficulty.Hard:

                characterStats.power = 20f; // Higher damage
                agent.speed = 5f; // Faster
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Only update if we have a player reference
        if (player != null)
        {
            Debug.Log("[EnemyController] Update running for: " + gameObject.name);
            animator.SetFloat("speed", agent.velocity.magnitude);
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                agent.SetDestination(player.position);
                if (distanceToPlayer <= agent.stoppingDistance && CanAttack)
                {
                    StartCoroutine(cooldown());
                    animator.SetTrigger("Attack");
                    CanAttack = false;
                    AttackPlayer(); // Call the attack logic
                    Invoke("ResetAttack", attackCooldown);
                }
            }
        }
    }
    IEnumerator cooldown()
    {
        CanAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[EnemyController] OnTriggerEnter called by: " + other.name + " with tag: " + other.tag);
        if (other.CompareTag("AttackCollider"))
        {
            Debug.Log("[EnemyController] Player hit!");

            // Get the CharacterStats component from the player
            CharacterStats playerStats = other.GetComponentInParent<CharacterStats>();
            if (playerStats != null)
            {
                // Apply damage to the enemy using the player's power
                float damage = playerStats.GetPower();
                characterStats.ChangeHealth(-(int)damage); // Cast damage to int
            }
        }
    }

    private void AttackPlayer()
    {
        if (player != null)
        {
            CharacterStats playerStats = player.GetComponent<CharacterStats>();
            if (playerStats != null)
            {
                // Apply damage to the player using the enemy's power
                float damage = characterStats.GetPower();
                playerStats.ChangeHealth(-(int)damage); // Cast damage to int
            }
        }
    }

    public void ResetAttack()
    {
        // Your reset logic here
    }

    public static void CheckAllEnemiesDead()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            SceneManager.LoadScene(0); // Load main menu
        }
    }
}
