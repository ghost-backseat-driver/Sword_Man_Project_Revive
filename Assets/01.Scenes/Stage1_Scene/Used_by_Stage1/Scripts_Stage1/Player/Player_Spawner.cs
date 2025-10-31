using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Spawner : MonoBehaviour
{
    [Header("�÷��̾� ������")]
    [SerializeField] private GameObject playerPrefab;

    [Header("�⺻ ���� ����Ʈ(���� �ȵǾ����� ����)")]
    [SerializeField] private GameObject defaultSpawnPoint;

    private void Awake()
    {
        //�¾��ε�� ü�ΰɱ�-���� �̺�Ʈ �ɱ�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        //ü�� Ǯ��-�ߺ�������-�̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //�� �ε�ɶ� ü�ΰɾ ���� ������-�̺�Ʈ��
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ����� ������ �ҷ�����
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 spawnPos = Vector3.zero;
        bool savedPos = false;

        //���� ������ ������
        if (data != null)
        {
            spawnPos = data.playerPos;
            savedPos = true;
        }
        // ���� �����Ͱ� ������ �⺻ ������ġ�� �����ǰ�
        else
        {
            spawnPos = defaultSpawnPoint.transform.position;
        }

        // �÷��̾� ����
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        // �ε�� ������ ����
        Player_SaveLoad player_SaveLoad = player.GetComponent<Player_SaveLoad>();
        if (savedPos && player_SaveLoad != null)
        {
            player_SaveLoad.Load();
        }
    }
}
