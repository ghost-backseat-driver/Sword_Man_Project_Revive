using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SpawnPoint : MonoBehaviour
{
    //�ϴ� ���̽� ������ġ-���� ���� ��ġ���� ������
    public Vector3 GetSpawnPoint()
    {
        return transform.position;
    }
}
