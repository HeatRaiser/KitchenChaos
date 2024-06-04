using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private bool isWalking;
    
    private void Update()
    {
        Vector2 inputVector = Vector2.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1f;
        }
        
        inputVector = inputVector.normalized;
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerSize = .8f;
        bool canMove = !Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0f), moveDir, playerSize);

         if (canMove)
         {
             transform.position += moveDir * (moveSpeed * Time.deltaTime);
         }
        

        isWalking = moveDir != Vector3.zero;
        
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    
}
