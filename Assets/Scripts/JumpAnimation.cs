using UnityEngine;

public class JumpAnimation : StateMachineBehaviour
{
    private Jump jumpScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (jumpScript == null)
        {
            jumpScript = animator.GetComponent<Jump>();
        }
        animator.SetBool("isJumping", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (jumpScript != null)
        {
            float sameJumpSpeed = jumpScript.sameJumpSpeed;
            animator.SetFloat("sameJumpSpeed", sameJumpSpeed);

            if (sameJumpSpeed <= 1.1f)
            {
                animator.SetBool("isJumping", false);
            }
            if (sameJumpSpeed < 0f)
            {
                animator.SetBool("falling", true);
            }
        }
    }
}
