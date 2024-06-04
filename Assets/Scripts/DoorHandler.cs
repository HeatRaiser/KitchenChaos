using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private float threshold = 2.2f;
    
    private Animator animator;
    private bool playerOnTheOtherSide;
    private Vector3 playerEnterPosition;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnterPosition = other.transform.position;
            
            if (!playerOnTheOtherSide)
            {
                animator.Play("OpenDoors");
            }
            else
            {
                animator.Play("OpenDoorsOpposite");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 playerExitPosition = other.transform.position;
            float distance = Vector3.Distance(playerEnterPosition, playerExitPosition);
            Debug.Log(distance);
            
            if (!playerOnTheOtherSide)
            {
                animator.Play("CloseDoors");
            }
            else
            {
                animator.Play("CloseDoorsOpposite");
            }

            if (distance > threshold)
            {
                playerOnTheOtherSide = !playerOnTheOtherSide;
            }
        }
    }
}
