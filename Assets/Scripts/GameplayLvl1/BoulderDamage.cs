using UnityEngine;

public class BoulderDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            HealthScript playerHealth = collision.collider.GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                playerHealth.DepleteHealth(0.5f);
            }
        }    
    }
}
