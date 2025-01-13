using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CrateCollisionManager : MonoBehaviour
{
    [SerializeField] private GameObject uiItem;
    private bool playerInRange = false;

    void Start()
    {
        uiItem.SetActive(false);
        Debug.Log($"Crate {gameObject.name} initialized");
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log($"F key pressed near crate: {gameObject.name}");
            uiItem.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
