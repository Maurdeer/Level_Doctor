using UnityEngine;

public class Fan : MonoBehaviour {
    public float maxRange = 5f;

    // How strongly it pulls the player toward the target hover point
    public float hoverStrength = 20f;

    // Smooth damping (prevents jitter)
    public float damping = 5f;

    // Small bobbing effect
    public float bobStrength = 2f;
    public float bobSpeed = 2f;

    private void OnTriggerStay2D(Collider2D collision) {
        if (!collision.CompareTag("Player")) return;

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        Vector2 fanPos = transform.position;
        Vector2 fanUp = transform.up;

        Vector2 toPlayer = (Vector2)collision.transform.position - fanPos;

        float distanceAlongUp = Vector2.Dot(toPlayer, fanUp);

        // Only affect player in front of fan
        if (distanceAlongUp <= 0f) return;

        // Target hover point = maxRange in front of fan
        float error = maxRange - distanceAlongUp;

        // Spring force toward hover position
        float force = error * hoverStrength;

        // Damping (stops oscillation exploding)
        float velocityAlongUp = Vector2.Dot(rb.linearVelocity, fanUp);
        float damp = -velocityAlongUp * damping;

        // Bobbing (small oscillation around hover point)
        float bob = Mathf.Sin(Time.time * bobSpeed) * bobStrength;

        Vector2 finalForce = fanUp * (force + damp + bob);

        rb.AddForce(finalForce, ForceMode2D.Force);
    }
}