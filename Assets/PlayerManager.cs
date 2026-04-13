using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool canMove;
    [SerializeField] private BoxCollider2D playerCollider;
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

    public void SetPlayerColliderActive(bool value)
    {
        Rigidbody2D playerRB = gameObject.GetComponent<Rigidbody2D>(); 
        playerRB.linearVelocity = new Vector2(0, 0);
        if (value) playerRB.gravityScale = 1f;
        else playerRB.gravityScale = 0f;
        playerCollider.enabled = value;
    }

}
