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
            //�и��� ī�޶� ����Ʈ-> �׳� ī�޶�ȿ���� ��Ա����
            damagedFxManager.OnCameraFX();
        }
    }
}
