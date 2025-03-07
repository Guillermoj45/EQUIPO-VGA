using UnityEngine;

public class AnimacionGatocidio : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime >= 1 && !animator.IsInTransition(0))
        {
            gameObject.SetActive(false); // Desactiva el objeto al terminar la animación
        }
    }
}
