using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Key")
        {
            other.gameObject.SetActive(false);
            Debug.Log("Key item collected.");
        }
    }
}
