using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed=2.5f;

    private float centerX,realMoveSpeed;
    private void OnEnable()
    {
        StackController.Instance.onPathPlaced.AddListener(OnPathPlaced);
        GameManager.Instance.OnLevelStart.AddListener(OnLevelStart);
        GameManager.Instance.OnLevelFail.AddListener(OnLevelFail);
    }

    void Update()
    {
        MoveZ();
        CenteringX();
    }

    private void MoveZ()
    {
        transform.position += Vector3.forward * Time.deltaTime*realMoveSpeed;
    }

    private void CenteringX()
    {
        transform.position += Vector3.right * Time.deltaTime*(centerX - transform.position.x)*moveSpeed;
    }

    public void StopMove()
    {
        realMoveSpeed = 0;
    }
    
    private void OnPathPlaced(Vector3 pathPos)
    {
        centerX = pathPos.x;
    }

    private void OnLevelStart()
    {
        realMoveSpeed = moveSpeed;
        Player.Instance.Animator.SetSpeed(1);
    }

    private void OnLevelFail()
    {
        moveSpeed = 0;
    }
}
