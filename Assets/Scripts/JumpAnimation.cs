using UnityEngine;

public class JumpAnimation : StateMachineBehaviour
{
    private PlayerController playerController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController = animator.GetComponent<PlayerController>();
        animator.SetBool("isJumping", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController != null)
        {
            bool isGrounded = playerController.IsGrounded();
            float verticalSpeed = playerController.GetVerticalSpeed();

            animator.SetBool("isGrounded", isGrounded);
            animator.SetFloat("sameJumpSpeed", verticalSpeed);

            if (verticalSpeed <= 1.1f && !isGrounded)
            {
                animator.SetBool("isJumping", false);
            }
            if (verticalSpeed < 0f && !isGrounded)
            {
                animator.SetBool("falling", true);
            }
        }
    }
}
