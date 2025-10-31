using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Spawner : MonoBehaviour
{
    [Header("플레이어 프리팹")]
    [SerializeField] private GameObject playerPrefab;

    [Header("기본 스폰 포인트(저장 안되었을때 기준)")]
    [SerializeField] private GameObject defaultSpawnPoint;

    private void Awake()
    {
        //온씬로디드 체인걸기-씬에 이벤트 걸기
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        //체인 풀기-중복방지용-이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //씬 로드될때 체인걸어서 같이 나오게-이벤트온
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 저장된 데이터 불러오기
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 spawnPos = Vector3.zero;
        bool savedPos = false;

        //저장 데이터 있으면
        if (data != null)
        {
            spawnPos = data.playerPos;
            savedPos = true;
        }
        // 저장 데이터가 없으면 기본 스폰위치로 스폰되게
        else
        {
            spawnPos = defaultSpawnPoint.transform.position;
        }

        // 플레이어 생성
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        // 로드된 데이터 적용
        Player_SaveLoad player_SaveLoad = player.GetComponent<Player_SaveLoad>();
        if (savedPos && player_SaveLoad != null)
        {
            player_SaveLoad.Load();
        }
    }
}
