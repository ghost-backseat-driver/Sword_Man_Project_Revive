using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_ATKBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            //�÷��̾� hp ���ҽ�Ű��
            Character_HP playerHp = collision.GetComponent<Character_HP>();
            if (playerHp != null)
            {
                //�� �����, ������ ��ġ 
                playerHp.TakeDamage(2, transform.position);
            }
            //Ÿ�ݼ��� ����
            //SoundManager.Instance.PlayEffect("Player_ATK1_SFX");
        }
    }
}
