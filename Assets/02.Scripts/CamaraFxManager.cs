using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//�ó׸ӽ� Ȱ���ؼ� �и� ����Ʈ ������ ��ũ��Ʈ
public class CamaraFxManager : MonoBehaviour
{
    [Header("�ó׸ӽ� ����ī�޶�")]
    [SerializeField] private CinemachineVirtualCamera vcamMain;
    [Header("�ó׸ӽ� ȿ���ߵ� ī�޶�")]
    [SerializeField] private CinemachineVirtualCamera vcamFX;

    [Header("ȭ�� ���ο� �ӵ�")]
    [SerializeField] private float slowScale = 0.5f;
    [Header("ȭ�� ���ο� �ð�")]
    [SerializeField] private float slowTime = 0.5f;

    //ī�޶� ����ŷ �κ� ���޽��ҽ� ����ȵ�
    [Header("ī�޶� ��鸲 (���޽��ҽ�)")]
    [SerializeField] private CinemachineImpulseSource impulseSource;

    //�������ٰ� �����ؾ� �Ǵϱ�, ���� ȭ��ӵ� �����
    private float originalTimeScale;

    public void OnCameraFX()
    {
        StartCoroutine(CamaraFxCO());
    }

    private IEnumerator CamaraFxCO()
    {

        //���ο��� ����==
        //�������� = Ÿ�ӽ�����=> ���ο콺����
        originalTimeScale = Time.timeScale;
        Time.timeScale = slowScale;

        //ī�޶� �켱���� ����-�и� ī�޶� priority ���� ũ���
        vcamFX.Priority = 30;

        //���޽�(ī�޶� ��鸲)
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }

        //���ο� �ð���ŭ ��� ����ϰ�,
        yield return new WaitForSecondsRealtime(slowTime);

        //Ÿ�ӽ����� �������η� ����
        Time.timeScale = originalTimeScale;

        //ī�޶� �켱���� �������� ����
        vcamFX.Priority = 10;
    }
}
