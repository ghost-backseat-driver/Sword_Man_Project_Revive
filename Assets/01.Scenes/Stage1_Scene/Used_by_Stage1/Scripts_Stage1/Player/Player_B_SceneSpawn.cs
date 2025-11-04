using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_B_SceneSpawn : MonoBehaviour
{
    [Header("플레이어 프리팹")]
    [SerializeField] private GameObject playerPrefab;

    [Header("보스씬 고정 스폰 포인트")]
    [SerializeField] private GameObject spawnPoint;

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
        spawnPos = spawnPoint.transform.position;

        // 플레이어 생성
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        // 로드된 데이터 적용
        Player_SaveLoad player_SaveLoad = player.GetComponent<Player_SaveLoad>();
        if (player_SaveLoad != null)
        {
            player_SaveLoad.Load();
        }
        //위치 스폰위치로 변경
        player.transform.position = spawnPos;
    }
}
