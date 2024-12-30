using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollisionManager : MonoBehaviour
{
    [SerializeField] private GameObject uiObject;
    void Start()
    {
        uiObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F))

        {
            // Perform your action when the key is pressed

            Debug.Log("Key pressed: F");

        }
    }
    public void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            StartCoroutine(DisplayUI());
        }
    }
    private IEnumerator DisplayUI()
    {
        uiObject.SetActive(true);
        yield return new WaitForSeconds(10);
        uiObject.SetActive(false);
    }
}
