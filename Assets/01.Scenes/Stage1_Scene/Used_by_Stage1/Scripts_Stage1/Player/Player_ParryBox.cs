using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ParryBox : MonoBehaviour
{
    [SerializeField] private ParryEffectManager parryEffectManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyAtk"))
        {
            //패리 이펙트 성공이니까 패리매니저에서 가져오고
            parryEffectManager.OnParrySuccess();
            //SoundManager.Instance.PlayEffect("패리 성공 사운드");

            //패링콜라이더에 닿은 콜라이더 비활성화
            collision.gameObject.SetActive(false);
        }
    }
}
