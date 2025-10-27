using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameQuitButton : MonoBehaviour
{
    [Header("���� ���� ��ư")]
    [SerializeField] private Button quitGameButton;
    private void Start()
    {
        //��ư �̺�Ʈ ���
        quitGameButton.onClick.AddListener(QuitGame);
    }
    public void QuitGame()
    {
        Application.Quit(); // ���� ���忡���� ���� ����
        Debug.Log("���� ����"); // �����Ϳ��� Ȯ�ο� �α�-����� �����
    }
}
