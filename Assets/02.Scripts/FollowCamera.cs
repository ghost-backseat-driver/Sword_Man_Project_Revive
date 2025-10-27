using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("����")]
    public Transform target;

    //y�� ������ؼ� ���� y�� �÷��� ������ �ʿ�
    [Header("ī�޶� ������")]
    [SerializeField] private float yOffset = 2.0f;
    private void LateUpdate()
    {
        if (target != null)
        {
            //Lerp �Ⱦ��ž�. ������ ��û ����������
            transform.position = new Vector3(target.position.x, target.position.y + yOffset, -10.0f);
        }
    }
}
