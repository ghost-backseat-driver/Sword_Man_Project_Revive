using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    [Header("�ó׸ӽ� ����ī�޶�")]
    [SerializeField] private CinemachineVirtualCamera vcamMain;
    [Header("�ó׸ӽ� �и�ī�޶�")]
    [SerializeField] private CinemachineVirtualCamera vcamParry;

    private bool isLinked = false;

    private void Update()
    {
        //��ũ ���¸� ���᳡
        if (isLinked) return;

        //�±׷� �÷��̾� ã�Ƽ�
        GameObject player = GameObject.FindGameObjectWithTag("player");
        if (player != null)
        {
            //���� ī�޶� Follow/LookAt�� �÷��̾� ��ġ ����
            if (vcamMain != null)
            {
                vcamMain.Follow = player.transform;
                vcamMain.LookAt = player.transform;
            }

            if (vcamParry != null)
            {
                vcamParry.Follow = player.transform;
                vcamParry.LookAt = player.transform;
            }

            //��ũ �Ϸ�
            isLinked = true;
        }
    }
}
