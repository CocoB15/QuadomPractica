using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float rotateSpeed = 10f;
    [SerializeField]private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    private float playerRadius = .7f;
    private float playerHeight = 2f;
    private float moveDistance;
    private bool isWalking;
    private Vector3 lastInteractDir;
    
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
        
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float interactionDistance = 2f;
        if (moveDir!=Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactionDistance,layerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //Has  ClearCounter
                clearCounter.Interact();
            }
            Debug.Log(raycastHit.transform);
        }
        else
        {
            Debug.Log("-");
        }
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float interactionDistance = 2f;
        if (moveDir!=Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactionDistance,layerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //Has  ClearCounter
                
            }
            
        }
        else
        {
            
        }
    }
    
    private void HandleMovement()
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
                
                //Attempt moving on Z axis
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
