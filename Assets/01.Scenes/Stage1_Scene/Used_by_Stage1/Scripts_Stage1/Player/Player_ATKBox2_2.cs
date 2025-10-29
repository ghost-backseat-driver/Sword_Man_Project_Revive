using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ATKBox2_2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            // ���ʹ� hp ���ҽ�Ű��
            Character_HP enemyHp = collision.GetComponent<Character_HP>();
            if (enemyHp != null)
            {
                //�� �����, ������ ��ġ 
                enemyHp.TakeDamage(3, transform.position);
            }
            //Ÿ�ݼ��� ����
            //SoundManager.Instance.PlayEffect("Player_ATK2_SFX");
        }
    }
}
