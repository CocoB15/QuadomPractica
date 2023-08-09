using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float rotateSpeed = 10f;
    [SerializeField]private GameInput gameInput;
    private float playerRadius = .7f;
    private float playerHeight = 2f;
    private float moveDistance;
    private bool isWalking;
    
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        
        moveDistance = moveSpeed * Time.deltaTime;
        
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position+Vector3.up*playerHeight , playerRadius , moveDir, moveDistance);
        if (!canMove)
        {
            //Cannot move towards moveDir
            
            //Attempt only move on X axis
            Vector3 moveDirX = new Vector3(moveDir.x,0,0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //can only move on X axis
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move on X axis
                
                //Attempt oving on Z axis
                Vector3 moveDirZ = new Vector3(0,0,moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //can only move on Z axis
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move period.
                }
                
                
            }

        }
        
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        
        
        transform.forward=Vector3.Slerp(transform.forward,moveDir,Time.deltaTime*rotateSpeed);
        
        isWalking = moveDir != Vector3.zero;

        

    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
