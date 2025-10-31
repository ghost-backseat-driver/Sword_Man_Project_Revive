using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ATKBox2_2 : MonoBehaviour
{
    [Header("�÷��̾� ���ùڽ�3 ���ݷ�")]
    [SerializeField] private int ATK2_2Power = 1;

    //�ܺο��� ������ �� �ִ� Getter,Setter �߰� -> ���̺�+���׷��̵��
    public int GetATK2_2Power() => ATK2_2Power;
    public void SetATK2_2Power(int value) => ATK2_2Power = value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            // ���ʹ� hp ���ҽ�Ű��
            Character_HP enemyHp = collision.GetComponent<Character_HP>();
            if (enemyHp != null)
            {
                //�� �����, ������ ��ġ 
                enemyHp.TakeDamage(ATK2_2Power, transform.position);
            }
            //Ÿ�ݼ��� ����
            //SoundManager.Instance.PlayEffect("Player_ATK2_SFX");
        }
    }
}
