using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollisionManager : MonoBehaviour
{
    [SerializeField] private GameObject uiTutorial;
    [SerializeField] private GameObject uiItem;
    void Start()
    {
        uiTutorial.SetActive(false);
        uiItem.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F))

        {
            uiItem.SetActive(true);
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
        uiTutorial.SetActive(true);
        yield return new WaitForSeconds(10);
        uiTutorial.SetActive(false);
    }
}
