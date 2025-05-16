using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5f;
    private Transform cam;
    private float gravity = -9.81f;
    private float verticalVelocity;
    public float jumpHeight = 2f;
    public float sprintMultiplier = 2.5f;
    private Animator animator;
    private bool isAttacking = false;
    private BoxCollider attackCollider;
    public float attackDuration = 0.5f;
    public AudioSource attackSound;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        animator = GetComponentInChildren<Animator>();
        attackCollider = transform.Find("Collider")?.GetComponent<BoxCollider>();
        if (attackCollider != null)
        {
            attackCollider.enabled = false; // Ensure collider is disabled at the start
            attackCollider.gameObject.tag = "AttackCollider";
        }
    }

    void Update()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // Handle animations
        UpdateAnimations(moveDirection);

        // Handle attack
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartAttack();
        }

        // Handle jumping and gravity
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f; // Small negative value to keep grounded

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        // Handle movement and rotation
        if (moveDirection.magnitude >= 0.1f && !isAttacking)
        {
            // Calculate rotation
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            // Calculate movement direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Apply sprint
            float currentSpeed = IsSprinting() ? speed * sprintMultiplier : speed;

            // Apply movement
            moveDir = moveDir * currentSpeed;
            moveDir.y = verticalVelocity;

            controller.Move(moveDir * Time.deltaTime);
        }
        else
        {
            // Still apply gravity when not moving
            controller.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
        }
    }

    private void UpdateAnimations(Vector3 moveDirection)
    {
        float speedValue = moveDirection.magnitude;
        if (IsSprinting())
        {
            speedValue *= 2f;
        }
        Debug.Log("Blend value: " + speedValue);
        animator.SetFloat("Blend", speedValue);
    }

    private bool IsSprinting()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    private void StartAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");

            // Play attack sound
            if (attackSound != null)
            {
                attackSound.Play();
            }

            // Enable the attack collider
            if (attackCollider != null && !attackCollider.enabled)
            {
                attackCollider.enabled = true;
            }

            // End the attack after the duration
            Invoke(nameof(EndAttack), attackDuration);
        }
    }

    private void EndAttack()
    {
        isAttacking = false;

        // Disable the attack collider
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }

        // Reset the attack trigger to allow retriggering
        if (animator != null)
        {
            animator.ResetTrigger("Attack");
        }
    }

    public void DoAttack()
    {
        if (attackCollider != null && !attackCollider.enabled)
        {
            Debug.Log("Enabling AttackCollider");
            attackCollider.enabled = true;
            StartCoroutine(HideCollider());
            if (attackSound != null)
            {
                attackSound.Play();
            }

        }
    }

    private IEnumerator HideCollider()
    {
        yield return new WaitForSeconds(attackDuration);
        if (attackCollider != null)
        {
            Debug.Log("Disabling AttackCollider");
            attackCollider.enabled = false;
        }
    }
}