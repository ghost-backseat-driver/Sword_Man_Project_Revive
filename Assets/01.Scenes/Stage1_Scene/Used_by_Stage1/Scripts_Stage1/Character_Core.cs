using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��� ĳ���͵��� ���� ��� ���ųֱ�-�����ũ���� �ҷ��;� �Ұ� ����,
//�߰��ؾ� �Ұ� ������, �� �߰��ߴ��� ����ϰ�, ������������ ������ �� �� ��¥ ū�ϳ�
public class Character_Core : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    /*
    ���� �� ��ũ��Ʈ�� ������ ��
    ĳ����(enemy,player) ��ο� ���������� ���� ������,�ִϸ�����,��������Ʈ ������ ����(�ھ��)
     */
}
