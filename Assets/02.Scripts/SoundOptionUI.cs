using UnityEngine;
using UnityEngine.UI;

public class SoundOptionUI : MonoBehaviour
{
    [Header("UI �����̴�")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // �����̴� �ʱⰪ�� ���� ����� �ͼ�(GetVolume) ������ ����
        bgmSlider.value = SoundManager.Instance.GetVolume(SoundType.BGM);
        sfxSlider.value = SoundManager.Instance.GetVolume(SoundType.EFFECT);

        // �����̴� �� ���� �� ȣ��� �̺�Ʈ ���
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetEffectVolume);
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ��� ���� -�ߺ�����
        bgmSlider.onValueChanged.RemoveListener(SetBGMVolume);
        sfxSlider.onValueChanged.RemoveListener(SetEffectVolume);
    }

    //���� �Ŵ��� ���� ������ ������ �� SetVolume ������
    private void SetBGMVolume(float value)
    {
        SoundManager.Instance.SetVolume(SoundType.BGM, value);
    }

    private void SetEffectVolume(float value)
    {
        SoundManager.Instance.SetVolume(SoundType.EFFECT, value);
    }
}
