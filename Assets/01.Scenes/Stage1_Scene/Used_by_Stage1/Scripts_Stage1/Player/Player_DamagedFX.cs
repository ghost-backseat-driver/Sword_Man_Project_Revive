using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DamagedFX : MonoBehaviour
{
    [SerializeField] private CamaraFxManager damagedFxManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyAtk"))
        {
            //패링용 카메라 이펙트-> 그냥 카메라효과로 써먹기로함
            damagedFxManager.OnCameraFX();
        }
    }
}
