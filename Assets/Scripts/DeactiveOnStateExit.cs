using UnityEngine;

public class DeactiveOnStateExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator != null && animator.gameObject != null)
        {
            if(animator.gameObject.transform.parent != null)
                animator.gameObject.transform.SetParent(null);

            animator.gameObject.SetActive(false);
        }
    }
}
