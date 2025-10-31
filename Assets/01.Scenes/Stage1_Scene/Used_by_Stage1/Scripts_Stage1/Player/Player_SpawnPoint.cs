using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SpawnPoint : MonoBehaviour
{
    //일단 베이스 스폰위치-지정 스폰 위치에서 나오게
    public Vector3 GetSpawnPoint()
    {
        return transform.position;
    }
}
