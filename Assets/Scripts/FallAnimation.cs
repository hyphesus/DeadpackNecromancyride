using UnityEngine;

public class FallAnimation : StateMachineBehaviour
{
    private PlayerController playerController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController = animator.GetComponent<PlayerController>();
        animator.SetBool("falling", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController != null)
        {
            bool isGrounded = playerController.IsGrounded();
            float verticalSpeed = playerController.GetVerticalSpeed();

            animator.SetBool("isGrounded", isGrounded);
            animator.SetFloat("sameJumpSpeed", verticalSpeed);

            if (isGrounded)
            {
                animator.SetBool("falling", false);
                animator.SetBool("isGrounded", true);
            }

            if (verticalSpeed > 1.1f && !isGrounded)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("falling", false);
            }
        }
    }
}
