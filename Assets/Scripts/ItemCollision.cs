using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    [SerializeField] private GameObject item1;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Key")
        {
            Debug.Log("Collision with key detetced.");
            StartCoroutine(CollectItem());
            Debug.Log("Key item collected.");
        }
    }
    private IEnumerator CollectItem()
    {
        item1.SetActive(false);
        yield return null;
    }
}