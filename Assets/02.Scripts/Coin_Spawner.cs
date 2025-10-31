using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Spawner : MonoBehaviour
{
    [Header("���� ������ & Ǯ")]
    [SerializeField] private Coin_Launch coinPrefab;
    [SerializeField] private int initPoolCount = 20;

    [Header("���� ��ġ")]
    [SerializeField] private Transform spawnPoint;

    [Header("���ν��� �ּҰ���")]
    [SerializeField] private int minGold = 10;

    [Header("���ν��� �ִ밹��")]
    [SerializeField] private int maxGold = 20;

    [Header("���� �Ѹ��� ��")]
    [SerializeField] private float spawnForce = 5.0f;

    [Header("���� �Ѹ� x����")]
    [SerializeField] private float spreadX = 1.0f;

    [Header("���� �Ѹ� y����")]
    [SerializeField] private float spreadY = 1.0f;

    private void OnEnable()
    {
        //�� ���� �� Ǯ ����
        GameManagers.Pool.CreatePool(coinPrefab, initPoolCount, transform);
    }

    //�ִϸ��̼� �̺�Ʈ �ݶ��̴� Ȱ��ȭ-���ʹ� �����ǿ� ����
    public void EnableSpawnGold()
    {
        int count = Random.Range(minGold, maxGold + 1); //���������� ��Ʈ���� �ִ밪 ���Ծȵ� +1����

        for (int i = 0; i < count; i++)
        {
            Coin_Launch coin = GameManagers.Pool.GetFromPool(coinPrefab);
            if (coin == null) continue;

            //������ġ ������ġ�� ����ְ�
            coin.transform.position = spawnPoint.position;

            // ���� ���� (���� Ƣ�� x�� y�� ������ �� �ؾ��ҵ�)
            Vector2 dir = new Vector2(Random.Range(-spreadX, spreadX),Random.Range(-spreadY, spreadY)).normalized;

            //�������� ��������ŭ �߻�
            coin.Launch(dir, spawnForce);
        }
        //���� �߰�
        SoundManager.Instance.PlayEffect("CoinDrop_SFX");
    }
}
