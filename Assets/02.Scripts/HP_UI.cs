using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour
{
    //���̺� �ε忡 �ʿ��ؼ� �׳� �̱������� ����������
    public static HP_UI Instance;

    [Header("HP������Ʈ �޷��ִ� �÷��̾� ������")]
    private Character_HP character_HP; //���� ���ε��±׷� �˾Ƽ� ã�ƿðž�.

    [Header("ü�¹� �����̴�")]
    [SerializeField] private Slider hpSlider;

    [Header("ü�¹� �پ��� �ӵ�")]
    [SerializeField] private float slideSpeed = 1.0f;

    //���� ü�¹ٿ��� ǥ�õ� Hp
    private float trueHP;

    private void Awake()
    {
        // �̱��� �ʱ�ȭ
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
        //�� �ε�ɶ�, ��ü�� �±׷� ã�Ƽ� ������Ʈ�� ����
        //������ ���Ŵϱ� �翬�� null�ϰž�.
        if (character_HP == null)
        {
            //�±׷� ã�ƿ���
            GameObject player = GameObject.FindGameObjectWithTag("player");
            if (player != null)
            {
                //�÷��̾ ����� HP������Ʈ�� ������
                character_HP = player.GetComponent<Character_HP>();
            }
        }

        //���� HP ����
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
