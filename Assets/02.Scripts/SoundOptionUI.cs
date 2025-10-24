using UnityEngine;
using UnityEngine.UI;

public class SoundOptionUI : MonoBehaviour
{
    [Header("UI 슬라이더")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // 슬라이더 초기값을 현재 오디오 믹서(GetVolume) 값으로 설정
        bgmSlider.value = SoundManager.Instance.GetVolume(SoundType.BGM);
        sfxSlider.value = SoundManager.Instance.GetVolume(SoundType.EFFECT);

        // 슬라이더 값 변경 시 호출될 이벤트 등록
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetEffectVolume);
    }

    private void OnDestroy()
    {
        // 이벤트 등록 해제 -중복방지
        bgmSlider.onValueChanged.RemoveListener(SetBGMVolume);
        sfxSlider.onValueChanged.RemoveListener(SetEffectVolume);
    }

    //사운드 매니저 에서 만들어둔 설정할 값 SetVolume 들고오기
    private void SetBGMVolume(float value)
    {
        SoundManager.Instance.SetVolume(SoundType.BGM, value);
    }

    private void SetEffectVolume(float value)
    {
        SoundManager.Instance.SetVolume(SoundType.EFFECT, value);
    }
}
