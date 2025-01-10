using UnityEngine;

public class SpikeScript : MonoBehaviour
{

    
    private void OnTriggerEnter2D(Collider2D other)
    {
                
        if (other.CompareTag("Player"))
        {
            HealthScript playerHealth = other.GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                playerHealth.DepleteHealth(0.5f);
            }
        }    
        else if (other.CompareTag("GroundCheck"))
        {
            Debug.Log("Collided with GroundCheck");
        }
    }


}
