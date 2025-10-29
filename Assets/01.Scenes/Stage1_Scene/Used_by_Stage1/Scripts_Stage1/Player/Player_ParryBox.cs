using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ParryBox : MonoBehaviour
{
    [SerializeField] private ParryEffectManager parryEffectManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyAtk"))
        {
            //�и� ����Ʈ �����̴ϱ� �и��Ŵ������� ��������
            parryEffectManager.OnParrySuccess();
            //SoundManager.Instance.PlayEffect("�и� ���� ����");

            //�и��ݶ��̴��� ���� �ݶ��̴� ��Ȱ��ȭ
            collision.gameObject.SetActive(false);
        }
    }
}
