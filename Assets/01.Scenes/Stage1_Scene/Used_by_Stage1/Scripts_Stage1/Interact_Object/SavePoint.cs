using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    //���� SerializeField �Ⱦ�. ���ε� �±� �ؾߵ�
    private Player_SaveLoad player_SaveLoad;

    [Header("���̺� ��ư")]
    [SerializeField] private Button saveButton; //����� ��ư

    private void Start()
    {
        //��ư�� Ŭ�� �̺�Ʈ ���
        if (saveButton != null)
        {
            saveButton.onClick.AddListener(SaveGame);
        }

        //��Ÿ�Ӷ� ������ �÷��̾� ã��
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
            Debug.LogWarning("���� ����: Player_SaveLoad�� �Ҵ���� ����");
            return;
        }

        player_SaveLoad.Save();
        Debug.Log("����Ϸ�");

    }
}
