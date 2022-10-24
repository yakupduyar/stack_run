using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed=2.5f;

    private float centerX;
    private void OnEnable()
    {
        StackController.Instance.onPathPlaced.AddListener(OnPathPlaced);
    }

    void Update()
    {
        MoveZ();
        CenteringX();
    }

    private void MoveZ()
    {
        transform.position += Vector3.forward * Time.deltaTime*moveSpeed;
    }

    private void CenteringX()
    {
        transform.position += Vector3.right * Time.deltaTime*(centerX - transform.position.x);
    }

    private void OnPathPlaced(Vector3 pathPos)
    {
        centerX = pathPos.x;
    }
    
}
