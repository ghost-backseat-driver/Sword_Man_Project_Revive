using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_ATKBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            // ���ʹ� hp ���ҽ�Ű��
            Character_HP enemyHp = collision.GetComponent<Character_HP>();
            if (enemyHp != null)
            {
                enemyHp.TakeDamage(3);
            }
            //ATK1 ������ ����
            SoundManager.Instance.PlayEffect("Player_ATK1_SFX");
        }
    }
}
