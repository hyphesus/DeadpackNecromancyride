using UnityEngine;

public class FallAnimation : StateMachineBehaviour
{
    private Jump jumpScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (jumpScript == null)
        {
            jumpScript = animator.GetComponent<Jump>();
        }
        animator.SetBool("falling", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (jumpScript != null)
        {
            float sameJumpSpeed = jumpScript.sameJumpSpeed;
            animator.SetFloat("sameJumpSpeed", sameJumpSpeed);

            if (sameJumpSpeed >= 0f)
            {
                animator.SetBool("falling", false);
                animator.SetBool("isGrounded", true);
            }
        }
    }
}
