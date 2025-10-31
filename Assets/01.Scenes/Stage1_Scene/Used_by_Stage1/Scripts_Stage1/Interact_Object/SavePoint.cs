using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    [Header("세이브 컴포넌트 넣은 플레이어")]
    [SerializeField] private Player_SaveLoad player_SaveLoad;

    [Header("세이브 버튼")]
    [SerializeField] private Button saveButton; // 재시작 버튼

    private void Start()
    {
        // 버튼에 클릭 이벤트 등록
        if (saveButton != null)
        {
            saveButton.onClick.AddListener(SaveGame);
        }
        else
        {
            Debug.Log("player_SaveLoad 참조 없음");
        }
    }

    private void SaveGame()
    {
        player_SaveLoad.Save();
        Debug.Log("저장완료");

    }
}
