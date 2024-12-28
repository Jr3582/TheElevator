using UnityEngine;

public class SpikeScript : MonoBehaviour
{

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
        
        Debug.DrawLine(transform.position, other.transform.position, Color.red, 2f);
        
        if (other.CompareTag("Player"))
        {
            HealthScript playerHealth = other.GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                playerHealth.DepleteHealth(1f);
            }
        }    
        else if (other.CompareTag("GroundCheck"))
        {
            Debug.Log("Collided with GroundCheck");
        }
    }


}
