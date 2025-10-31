using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignObject_UI : MonoBehaviour
{
    [Header("상호작용 패널")]
    [SerializeField] private GameObject signPanle;

    private void Start()
    {
        signPanle.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        signPanle.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        signPanle.SetActive(false);
    }
}
