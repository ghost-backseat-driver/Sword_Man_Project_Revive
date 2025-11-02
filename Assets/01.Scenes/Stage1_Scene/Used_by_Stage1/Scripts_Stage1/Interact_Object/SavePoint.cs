using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    //이제 SerializeField 안씀. 파인드 태그 해야돼
    private Player_SaveLoad player_SaveLoad;

    [Header("세이브 버튼")]
    [SerializeField] private Button saveButton;

    [Header("저장완료 알림패널")]
    [SerializeField] private GameObject SaveSuccessPanel;

    [Header("저장확인 버튼")]
    [SerializeField] private Button SaveOkButton;

    private void Start()
    {
        //버튼에 클릭 이벤트 등록
        saveButton.onClick.AddListener(SaveGame);

        SaveOkButton.onClick.AddListener(PanleDisabls);

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
        SaveSuccessPanel.SetActive(true);
        SoundManager.Instance.PlayEffect("OK_SFX");
        Debug.Log("저장완료");
    }

    private void PanleDisabls()
    {
        SoundManager.Instance.PlayEffect("Cancel_SFX");
        SaveSuccessPanel.SetActive(false);
    }
}
