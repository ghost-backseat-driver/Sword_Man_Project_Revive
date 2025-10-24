using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SoundType
{
    BGM,
    EFFECT,
}

public class SoundManager : Singleton<SoundManager>
{
    [Header("오디오 믹서 & 오디오 그룹")]
    [SerializeField] private AudioMixer audioMixer; //전체 오디오 믹서용
    [SerializeField] private AudioMixerGroup bgmGroup; //그룹으로 나누어진 BGM용
    [SerializeField] private AudioMixerGroup effectGroup; //그룹으로 나누어진 EFFECT용

    [Header("사용할 오디오클립 불러오기")]
    [SerializeField] private AudioClip[] preloadClips;

    private Dictionary<string, AudioClip> audioClipsDic; //이름으로 오디오 클립 찾기위한 dic

    private AudioSource bgmSource; //BGM 재생용
    private AudioSource effectSource; // EFFECT 재생용

    protected override void Awake()
    {
        base.Awake(); //싱글톤 초기화 -중복방지

        // AudioSource 초기화
        bgmSource = gameObject.AddComponent<AudioSource>();//BGM 재생 컴포넌트
        bgmSource.outputAudioMixerGroup = bgmGroup; //BGM은 BGM믹서 그룹이랑 묶어버리고
        bgmSource.loop = true; //BGM은 기본적으로 루프

        effectSource = gameObject.AddComponent<AudioSource>(); //EFFECT 재생 컴포넌트
        effectSource.outputAudioMixerGroup = effectGroup; //마찬가지로 해당 그룹이랑 묶어버리고

        // 클립 딕셔너리 초기화
        audioClipsDic = new Dictionary<string, AudioClip>();
        foreach (var clip in preloadClips)
        {
            audioClipsDic.Add(clip.name, clip); //이름으로 찾은거->클립으로
        }

        // 씬 로드 시 BGM 자동 교체 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // sceneLoaded 에서 OnSceneLoaded 제거 -자동교체 중복방지
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //BGM 재생 함수
    public void PlayBGM(string clipName)
    {
        //디버그용 하나 만들고
        if (!audioClipsDic.TryGetValue(clipName, out var clip))
        {
            Debug.LogWarning($"BGM {clipName} not found!");
            return;
        }

        //이미 재생중이면 내비둬
        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.clip = clip; //클립 가져오고,
        bgmSource.Play(); //재생
    }

    //BGM 정지 함수
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    //EFFECT 재생 함수 loop아니니까 정지는 필요없음
    public void PlayEffect(string clipName)
    {
        if (!audioClipsDic.TryGetValue(clipName, out var clip))
        {
            Debug.LogWarning($"Effect {clipName} not found!");
            return;
        }

        effectSource.PlayOneShot(clip); //한번 재생
    }

    //볼륨 컨트롤-나중에 UI 슬라이더에 연동시킬 용도
    //설정할거(저장 할 값), 저장된 값(가져올거) 두개
    public void SetVolume(SoundType type, float sliderValue)
    {
        // 슬라이더로 조절. Lerp로 슬라이더->decibel값(-80.0f~0.0f)으로 변환
        float decibel = Mathf.Lerp(-80.0f, 0.0f, sliderValue);
        // BGM||EFFECT를 문자열로 변환, 믹서의 파라미터 이름과 매칭하기 위함
        audioMixer.SetFloat(type.ToString(), decibel);
        
    }

    public float GetVolume(SoundType type)
    {
        //Set의 역순으로 들어오는 구조 InverseLerp decibel->슬라이더 값(0~1)로 변환
        audioMixer.GetFloat(type.ToString(), out float decibel);
        return Mathf.InverseLerp(-80.0f, 0.0f, decibel);
    }

    //씬 전환시 BGM 자동 변경해줄 함수
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 이름에 따라 BGM 자동 변경
        switch (scene.name)
        {
            case "Title_Scene":
                PlayBGM("TitleBGM");
                break;
            case "Stage1_Scene":
                PlayBGM("Stage1BGM");
                break;
            case "Boss_Scene":
                PlayBGM("BossBGM");
                break;
            default:
                StopBGM();
                break;
        }
    }
}
