using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool canMove;
    Transform playerTransform;
    Vector3 defaultPosition;


    private void Start()
    {
        canMove = false;
        playerTransform = transform;
        defaultPosition = transform.position;
    }

    public void resetPosition()
    {
        playerTransform.position = defaultPosition;

    }

    public void toggleMove()
    {
        canMove = !canMove;
    }

}
