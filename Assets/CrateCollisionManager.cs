using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateCollisionManager : MonoBehaviour
{
    [SerializeField] private GameObject uiItem;
    void Start()
    {
        uiItem.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F))

        {
            uiItem.SetActive(true);
        }
    }
}
