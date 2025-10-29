using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ATKBox1 : MonoBehaviour
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
                enemyHp.TakeDamage(1, transform.position);
            }
            //Ÿ�ݼ��� ����
            //SoundManager.Instance.PlayEffect("Player_ATK1_SFX");
        }
    }
}
