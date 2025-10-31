using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    //이제 SerializeField 안씀. 파인드 태그 해야돼
    private Player_SaveLoad player_SaveLoad;

    [Header("세이브 버튼")]
    [SerializeField] private Button saveButton; //재시작 버튼

    private void Start()
    {
        //버튼에 클릭 이벤트 등록
        if (saveButton != null)
        {
            saveButton.onClick.AddListener(SaveGame);
        }

        //런타임때 스폰된 플레이어 찾기
        if (player_SaveLoad == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("player");
            if (player != null)
            {
                player_SaveLoad = player.GetComponent<Player_SaveLoad>();
            }
        }
    }

    private void SaveGame()
    {
        if (player_SaveLoad == null)
        {
            Debug.LogWarning("저장 실패: Player_SaveLoad가 할당되지 않음");
            return;
        }

        player_SaveLoad.Save();
        Debug.Log("저장완료");

    }
}
