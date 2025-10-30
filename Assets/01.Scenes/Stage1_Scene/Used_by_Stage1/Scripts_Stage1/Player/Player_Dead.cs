using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dead : MonoBehaviour
{
    private Character_HP hp;

    private void Start()
    {
        hp = GetComponent<Character_HP>();
    }

    private void Update()
    {
        if (hp != null && hp.isDead)
        {
            // ��� �� �� ���� ó���ϱ� ���� Update���� ó�� �� ��Ȱ��ȭ �Ǵ� ���� �÷��� �߰� ����
            // ���� ���� �Ŵ��� ��ũ��Ʈ �߰��ؾ���.
            GameOverManager manager = FindObjectOfType<GameOverManager>();
            if (manager != null)
            {
                manager.ShowGameOver();
            }
            //enabled = false; // �ѹ� ó�� �� ��ũ��Ʈ ��Ȱ��ȭ
        }
    }
}
