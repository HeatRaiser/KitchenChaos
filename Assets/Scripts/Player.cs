using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask wallsLayer;

    private bool isWalking;
    
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        var step = moveDir * moveDistance;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        
        bool canMove = !Physics.CapsuleCast(transform.position , transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance, wallsLayer);

        if (!canMove)
        {
            var moveDirX = new Vector3(moveDir.x, 0, 0);
            bool canMoveX = !Physics.CapsuleCast(transform.position , transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance, wallsLayer);
            if (canMoveX)
            {
                canMove = true;
                step = moveDirX * moveDistance;
            }

            if (!canMove)
            {
                var moveDirZ = new Vector3(0, 0, moveDir.z);
                bool canMoveZ = !Physics.CapsuleCast(transform.position , transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance, wallsLayer);
                if (canMoveZ)
                {
                    canMove = true;
                    step = moveDirZ * moveDistance;
                }
            }
        }
        
        if (canMove)
        {
            transform.position += step;
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
