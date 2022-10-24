using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZ : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime*moveSpeed;
    }
}
