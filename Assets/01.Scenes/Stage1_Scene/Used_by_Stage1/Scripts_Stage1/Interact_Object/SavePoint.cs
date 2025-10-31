using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    [Header("���̺� ������Ʈ ���� �÷��̾�")]
    [SerializeField] private Player_SaveLoad player_SaveLoad;

    [Header("���̺� ��ư")]
    [SerializeField] private Button saveButton; // ����� ��ư

    private void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ ���
        if (saveButton != null)
        {
            saveButton.onClick.AddListener(SaveGame);
        }
        else
        {
            Debug.Log("player_SaveLoad ���� ����");
        }
    }

    private void SaveGame()
    {
        player_SaveLoad.Save();
        Debug.Log("����Ϸ�");

    }
}
