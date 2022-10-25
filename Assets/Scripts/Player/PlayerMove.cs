using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed=2.5f;
    [SerializeField] private Rigidbody rb;

    private float centerX,realMoveSpeed;
    private bool canFall;
    private void OnEnable()
    {
        StackController.Instance.onPathPlaced.AddListener(OnPathPlaced);
        GameManager.Instance.OnLevelStart.AddListener(OnLevelStart);
        GameManager.Instance.OnLevelSuccess.AddListener(OnLevelSuccess);
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
        transform.position += Vector3.right * Time.deltaTime*(centerX - transform.position.x)*realMoveSpeed;
    }

    public void StopMove()
    {
        realMoveSpeed = 0;
    }
    
    private void OnPathPlaced(Vector3 pathPos, int combo)
    {
        centerX = pathPos.x;
        canFall = false;
    }

    private void OnLevelStart()
    {
        realMoveSpeed = moveSpeed;
        Player.Instance.Animator.SetSpeed(1);
    }

    private void OnLevelSuccess()
    {
        StopMove();
    }

    private void OnLevelFail()
    {
        StopMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            canFall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Path") && canFall && !StackController.Instance.PathCompleted)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            CinemachineManager.Instance.StopTrack();
        }
    }
}
