using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public CharacterMovement characterMovement;

    void Start()
    {
        // Try to find CharacterMovement if not assigned
        if (characterMovement == null)
        {
            characterMovement = GetComponentInParent<CharacterMovement>();
        }
    }

    // Make sure this method name matches exactly with the animation event
    public void PlayerAttack()  // Changed from playerattack to PlayerAttack
    { 

        Debug.Log("PlayerAttack called");
        if (characterMovement != null)
        {
            
        }
    }
}
