using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//캐릭터 무브 받고, 랜덤 워크 입력값만 구현
public class Enemy_RandomWalk : MonoBehaviour
{
    private Character_Move move;
    private Vector2 walkDir = Vector2.zero;

    [Header("이동 방향전환 시간 간격")]
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

    //이동방향 랜덤 코루틴
    private IEnumerator RandomWalkCo()
    {
        while (true)
        {
            int randDir = Random.Range(-1, 2); // -1, 0, 1 왼,중,오
            walkDir = new Vector2(randDir, 0);
            yield return new WaitForSeconds(walkingTime);
        }
    }
    /*
     현재 이 스크립트에 구현된 것
    -캐릭터 무브에 전달해줄 (Enemy)이동 좌우 방향 랜덤 입력값
     */
}
