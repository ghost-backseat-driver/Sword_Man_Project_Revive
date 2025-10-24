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
    [Header("����� �ͼ� & ����� �׷�")]
    [SerializeField] private AudioMixer audioMixer; //��ü ����� �ͼ���
    [SerializeField] private AudioMixerGroup bgmGroup; //�׷����� �������� BGM��
    [SerializeField] private AudioMixerGroup effectGroup; //�׷����� �������� EFFECT��

    [Header("����� �����Ŭ�� �ҷ�����")]
    [SerializeField] private AudioClip[] preloadClips;

    private Dictionary<string, AudioClip> audioClipsDic; //�̸����� ����� Ŭ�� ã������ dic

    private AudioSource bgmSource; //BGM �����
    private AudioSource effectSource; // EFFECT �����

    protected override void Awake()
    {
        base.Awake(); //�̱��� �ʱ�ȭ -�ߺ�����

        // AudioSource �ʱ�ȭ
        bgmSource = gameObject.AddComponent<AudioSource>();//BGM ��� ������Ʈ
        bgmSource.outputAudioMixerGroup = bgmGroup; //BGM�� BGM�ͼ� �׷��̶� ���������
        bgmSource.loop = true; //BGM�� �⺻������ ����

        effectSource = gameObject.AddComponent<AudioSource>(); //EFFECT ��� ������Ʈ
        effectSource.outputAudioMixerGroup = effectGroup; //���������� �ش� �׷��̶� ���������

        // Ŭ�� ��ųʸ� �ʱ�ȭ
        audioClipsDic = new Dictionary<string, AudioClip>();
        foreach (var clip in preloadClips)
        {
            audioClipsDic.Add(clip.name, clip); //�̸����� ã����->Ŭ������
        }

        // �� �ε� �� BGM �ڵ� ��ü �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // sceneLoaded ���� OnSceneLoaded ���� -�ڵ���ü �ߺ�����
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //BGM ��� �Լ�
    public void PlayBGM(string clipName)
    {
        //����׿� �ϳ� �����
        if (!audioClipsDic.TryGetValue(clipName, out var clip))
        {
            Debug.LogWarning($"BGM {clipName} not found!");
            return;
        }

        //�̹� ������̸� �����
        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.clip = clip; //Ŭ�� ��������,
        bgmSource.Play(); //���
    }

    //BGM ���� �Լ�
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    //EFFECT ��� �Լ� loop�ƴϴϱ� ������ �ʿ����
    public void PlayEffect(string clipName)
    {
        if (!audioClipsDic.TryGetValue(clipName, out var clip))
        {
            Debug.LogWarning($"Effect {clipName} not found!");
            return;
        }

        effectSource.PlayOneShot(clip); //�ѹ� ���
    }

    //���� ��Ʈ��-���߿� UI �����̴��� ������ų �뵵
    //�����Ұ�(���� �� ��), ����� ��(�����ð�) �ΰ�
    public void SetVolume(SoundType type, float sliderValue)
    {
        // �����̴��� ����. Lerp�� �����̴�->decibel��(-80.0f~0.0f)���� ��ȯ
        float decibel = Mathf.Lerp(-80.0f, 0.0f, sliderValue);
        // BGM||EFFECT�� ���ڿ��� ��ȯ, �ͼ��� �Ķ���� �̸��� ��Ī�ϱ� ����
        audioMixer.SetFloat(type.ToString(), decibel);
        
    }

    public float GetVolume(SoundType type)
    {
        //Set�� �������� ������ ���� InverseLerp decibel->�����̴� ��(0~1)�� ��ȯ
        audioMixer.GetFloat(type.ToString(), out float decibel);
        return Mathf.InverseLerp(-80.0f, 0.0f, decibel);
    }

    //�� ��ȯ�� BGM �ڵ� �������� �Լ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� �̸��� ���� BGM �ڵ� ����
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
