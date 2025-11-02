using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour
{
    //세이브 로드에 필요해서 그냥 싱글톤으로 만들어버리기
    public static HP_UI Instance;

    [Header("HP컴포넌트 달려있는 플레이어 프리팹")]
    private Character_HP character_HP; //이제 파인드태그로 알아서 찾아올거야.

    [Header("체력바 슬라이더")]
    [SerializeField] private Slider hpSlider;

    [Header("체력바 줄어드는 속도")]
    [SerializeField] private float slideSpeed = 1.0f;

    //실제 체력바에서 표시될 Hp
    private float trueHP;

    private void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //씬 로드될때, 객체를 태그로 찾아서 컴포넌트로 연결
        //프리팹 쓸거니까 당연히 null일거야.
        if (character_HP == null)
        {
            //태그로 찾아오고
            GameObject player = GameObject.FindGameObjectWithTag("player");
            if (player != null)
            {
                //플레이어에 연결된 HP컴포넌트를 가져와
                character_HP = player.GetComponent<Character_HP>();
            }
        }

        //현재 HP 갱신
        if (character_HP != null)
        {
        UpdateHP(character_HP.GetHP(), character_HP.GetMaxHP());
        }
    }

    public void UpdateHP(int current, int max)
    {
        trueHP = Mathf.Lerp(trueHP, current, Time.deltaTime * slideSpeed);
        hpSlider.value = trueHP / max;
    }
}
