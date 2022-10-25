using UnityEngine;

public class Player : MonoBehaviour
{
    #region Singleton

    private static Player _instance;

    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Player>();
            }

            return _instance;
        }
    }

    #endregion

    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerAnimator playerAnimator;

    public PlayerMove Move => playerMove;
    public PlayerAnimator Animator => playerAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Animator.Dance();
            GameManager.Instance.SuccessLevel();
        }
    }
}
