using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator.speed = 0;
    }

    public void SetSpeed(float v)
    {
        animator.speed = v;
    }

    public void Dance()
    {
        animator.SetBool("Dance",true);
    }

    public void Run()
    {
        animator.SetBool("Dance",false);
    }
}
