using UnityEngine;

public class BoulderRoll : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D boulderRb;
    public float dropThreshold = 0.5f;
    public float verticalThreshold = 1f;
    public float friction = 0.5f; // Adjust friction for slower rolling
    public float angularDrag = 5f; // Adjust angular drag to slow the rotation

    private bool hasDropped = false;

    void Start()
    {
        // Optionally, set custom physics material for friction
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.friction = friction; // Set friction (0 = no friction, 1 = max friction)
        material.bounciness = 0f; // No bounciness
        boulderRb.sharedMaterial = material;

        // Set the angular drag to slow the rotation
        boulderRb.angularDrag = angularDrag;
    }

    void Update()
    {
        if (hasDropped) return;

        // Check if the player is within range horizontally and below the boulder vertically
        if (Mathf.Abs(player.position.x - transform.position.x) <= dropThreshold)
        {
            if (player.position.y < transform.position.y && Mathf.Abs(player.position.y - transform.position.y) <= verticalThreshold)
            {
                DropBoulder();
            }
        }
    }

    private void DropBoulder()
    {
        hasDropped = true;
        boulderRb.gravityScale = 0.9f; // Enable gravity to make the boulder fall
    }
}