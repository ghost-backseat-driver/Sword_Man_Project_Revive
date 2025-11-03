using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float_Rock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            SoundManager.Instance.PlayEffect("Active_Trap_SFX");
            Destroy(gameObject);
        }
    }
}
