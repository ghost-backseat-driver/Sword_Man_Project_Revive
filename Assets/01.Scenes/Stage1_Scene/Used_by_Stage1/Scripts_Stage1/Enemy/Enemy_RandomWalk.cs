using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ĳ���� ���� �ް�, ���� ��ũ �Է°��� ����
public class Enemy_RandomWalk : MonoBehaviour
{
    private Character_Move move;
    private Vector2 walkDir = Vector2.zero;

    [Header("�̵� ������ȯ �ð� ����")]
    [SerializeField] private float walkingTime = 1.0f;

    private void Awake()
    {
        move = GetComponent<Character_Move>();
    }

    private void OnEnable()
    {
        StartCoroutine(RandomWalkCo());
    }

    private void Update()
    {
        move.SetDir(walkDir);
    }

    //�̵����� ���� �ڷ�ƾ
    private IEnumerator RandomWalkCo()
    {
        while (true)
        {
            int randDir = Random.Range(-1, 2); // -1, 0, 1 ��,��,��
            walkDir = new Vector2(randDir, 0);
            yield return new WaitForSeconds(walkingTime);
        }
    }
    /*
     ���� �� ��ũ��Ʈ�� ������ ��
    -ĳ���� ���꿡 �������� (Enemy)�̵� �¿� ���� ���� �Է°�
     */
}
