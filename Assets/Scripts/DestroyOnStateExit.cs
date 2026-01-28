using UnityEngine;

public class DestroyOnStateExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator != null && animator.gameObject != null)
        {
            Lazer lazer = animator.gameObject.GetComponentInParent<Lazer>();

            if (lazer != null)
            {
                Destroy(lazer.gameObject);
            }

            Destroy(animator.gameObject);
        }
    }
}
