using System;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour, IKitchenObjectParent
{
    //public static Player Instance {get; private set;}

    public event EventHandler OnPickedSomething;
    public event EventHandler <OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;

    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private LayerMask colliderLayerMask;
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    private KitchenObject kitchenObject;
    private bool isWalking;
    private Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;

    private void Awake()
    {
        //Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }
    
    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        Debug.Log("Entered");
        
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
            Debug.Log("Interacted");
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        Debug.Log("Entered");
        if (!GameManager.Instance.IsGamePlaying()) return;

        Debug.Log("Further");
        
        if (selectedCounter != null)
        {
            Debug.Log("Further");
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        
        //Debug.Log(inputVector);
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDirection = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactRange, countersLayerMask))
        {
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                SetSelectedCounter(baseCounter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        var step = moveDir * moveDistance;
        
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        
        bool canMove = !Physics.CapsuleCast(transform.position , transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance, colliderLayerMask);

        if (!canMove)
        {
            var moveDirX = new Vector3(moveDir.x, 0, 0);
            bool canMoveX = !Physics.CapsuleCast(transform.position , transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance, colliderLayerMask);
            if (canMoveX)
            {
                canMove = true;
                step = moveDirX * moveDistance;
            }

            if (!canMove)
            {
                var moveDirZ = new Vector3(0, 0, moveDir.z);
                bool canMoveZ = !Physics.CapsuleCast(transform.position , transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance, colliderLayerMask);
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

    private void SetSelectedCounter(BaseCounter newSelectedCounter)
    {
        selectedCounter = newSelectedCounter;
        
        OnSelectedCounterChange?.Invoke(this,new OnSelectedCounterChangeEventArgs()
        {
            selectedCounter = newSelectedCounter
        });
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public bool TryGetPlateObject(out PlateKitchenObject plateKitchenObject)
    {
        if (kitchenObject.TryGetComponent(out PlateKitchenObject plate))
        {
            plateKitchenObject = plate;
            return true;
        }

        plateKitchenObject = null;
        return false;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    
}
