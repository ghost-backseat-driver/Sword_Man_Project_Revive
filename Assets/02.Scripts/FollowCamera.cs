using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("설정")]
    public Transform target;

    //y축 개답답해서 따로 y축 올려줄 수단이 필요
    [Header("카메라 오프셋")]
    [SerializeField] private float yOffset = 2.0f;
    private void LateUpdate()
    {
        if (target != null)
        {
            //Lerp 안쓸거야. 오히려 엄청 깨져보여서
            transform.position = new Vector3(target.position.x, target.position.y + yOffset, -10.0f);
        }
    }
}
