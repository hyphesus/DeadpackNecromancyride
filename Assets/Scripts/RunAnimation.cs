using UnityEngine;

public class RunAnimation : StateMachineBehaviour
{
    private Jump jumpScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (jumpScript == null)
        {
            jumpScript = animator.GetComponent<Jump>();
        }
        animator.SetBool("isJumping", false);
        animator.SetBool("falling", false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (jumpScript != null)
        {
            float sameJumpSpeed = jumpScript.sameJumpSpeed;
            animator.SetFloat("sameJumpSpeed", sameJumpSpeed);

            if (sameJumpSpeed > 1.1f)
            {
                animator.SetBool("isJumping", true);
            }
            else if (sameJumpSpeed < 0f)
            {
                animator.SetBool("falling", true);
            }
        }
    }
}
