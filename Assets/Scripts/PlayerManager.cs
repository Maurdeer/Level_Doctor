using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool canMove;
    [SerializeField] private BoxCollider2D playerCollider;
    Transform playerTransform;
    Vector3 defaultPosition;
    Rigidbody2D playerRB;


    private void Start()
    {
        canMove = false;
        playerTransform = transform;
        defaultPosition = transform.position;
        playerRB = gameObject.GetComponent<Rigidbody2D>(); 
    }

    public void resetPosition()
    {
        playerTransform.position = defaultPosition;
        playerRB.linearVelocity = new Vector2(0, 0);
    }

    public void toggleMove()
    {
        canMove = !canMove;

    }

    public void SetPlayerColliderActive(bool value)
    {
        playerRB.linearVelocity = new Vector2(0, 0);
        if (value) playerRB.gravityScale = 1f;
        else playerRB.gravityScale = 0f;
        playerCollider.enabled = value;
    }

    public void ZeroXVelocity()
    {
        playerRB.linearVelocityX = 0;
    }

}
