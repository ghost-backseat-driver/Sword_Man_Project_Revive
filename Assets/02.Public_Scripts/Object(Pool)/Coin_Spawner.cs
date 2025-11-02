using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Spawner : MonoBehaviour
{
    [Header("코인 프리팹 & 풀")]
    [SerializeField] private Coin_Launch coinPrefab;
    [SerializeField] private int initPoolCount = 20;

    [Header("스폰 위치")]
    [SerializeField] private Transform spawnPoint;

    [Header("코인스폰 최소갯수")]
    [SerializeField] private int minGold = 10;

    [Header("코인스폰 최대갯수")]
    [SerializeField] private int maxGold = 20;

    [Header("코인 뿌리는 힘")]
    [SerializeField] private float spawnForce = 5.0f;

    [Header("코인 뿌릴 x범위")]
    [SerializeField] private float spreadX = 1.0f;

    [Header("코인 뿌릴 y범위")]
    [SerializeField] private float spreadY = 1.0f;

    private void OnEnable()
    {
        //씬 시작 시 풀 생성
        GameManagers.Pool.CreatePool(coinPrefab, initPoolCount, transform);
    }

    //애니메이션 이벤트 콜라이더 활성화-에너미 사망모션에 넣자
    public void EnableSpawnGold()
    {
        int count = Random.Range(minGold, maxGold + 1); //랜덤레인지 인트형은 최대값 포함안됨 +1해줘

        for (int i = 0; i < count; i++)
        {
            Coin_Launch coin = GameManagers.Pool.GetFromPool(coinPrefab);
            if (coin == null) continue;

            //코인위치 스폰위치로 잡아주고
            coin.transform.position = spawnPoint.position;

            // 랜덤 방향 (위로 튀게 x값 y값 조절좀 잘 해야할듯)
            Vector2 dir = new Vector2(Random.Range(-spreadX, spreadX),Random.Range(-spreadY, spreadY)).normalized;

            //방향으로 지정힘만큼 발사
            coin.Launch(dir, spawnForce);
        }
        //사운드 추가
        SoundManager.Instance.PlayEffect("CoinDrop_SFX");
    }
}
