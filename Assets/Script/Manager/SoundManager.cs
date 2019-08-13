using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    static int soundMaxNum = 5; // 사운드 동시 출력 최대 갯수
    public AudioSource[] audioSource = new AudioSource[soundMaxNum];
    private Dictionary<string, AudioClip> DicAudioClip = new Dictionary<string, AudioClip>();
    int[] FileID;
    int[] SoundID;
    int MusicID;
    string[] SoundString;
    public void LoadSound()
    {
        // 사운드 동시 출력 최대 갯수 만큼 AudioSource 생성
        for (int i = 0; i < soundMaxNum; i++)
        {
            audioSource[i] = this.gameObject.AddComponent<AudioSource>();
            audioSource[i].bypassEffects = true;
            audioSource[i].bypassListenerEffects = true;
            audioSource[i].bypassReverbZones = true;
        }

        // 사운드 로드
        AudioClip[] resourcesAudioClip = Resources.LoadAll<AudioClip>("Sound");
        for (int i = 0; i < resourcesAudioClip.Length; i++)
        {
            DicAudioClip.Add(resourcesAudioClip[i].name, resourcesAudioClip[i]);
        }
        //안드로이드 사운드
        // Set up Android Native Audio
        AndroidNativeAudio.makePool(16);

       FileID = new int[System.Enum.GetValues(typeof(eSound)).Length];
        Debug.Log("System.Enum.GetValues(typeof(eSound)).Length" + System.Enum.GetValues(typeof(eSound)).Length);
        Debug.Log("FileID.Length" + FileID.Length);

        SoundID = new int[System.Enum.GetValues(typeof(eSound)).Length];

       
        Debug.Log("SoundID.Length" + SoundID.Length);

        SoundString = System.Enum.GetNames(typeof(eSound));
        for (int i = 0; i < System.Enum.GetValues(typeof(eSound)).Length; i++)
        {
            //Debug.Log(SoundString[i]);
            if (i == 0)
                FileID[i] = AndroidNativeAudio.load("Sound/" + SoundString[i] + ".mp3");
            else
                FileID[i] = AndroidNativeAudio.load("Sound/" + SoundString[i] + ".wav");

            Debug.Log(SoundString[i]);

            //SoundID[i] = AndroidNativeAudio.play(FileID[i], 1, -1, 1, 1);
        }
    }
    void Loaded(int musicID)
    {
        // Get music duration
        Debug.Log("load end");
    }
    void StoppedStrings(int musicID)
    {
        Debug.Log("play start");
    }
    void OnApplicationQuit()
    {
        // Clean up when done
        for (int i = 0; i < System.Enum.GetValues(typeof(eSound)).Length; i++)
        {
            AndroidNativeAudio.unload(FileID[i]);
        }
        AndroidNativeAudio.releasePool();
        ANAMusic.release(MusicID);
    }
    public void PlaySound(eSound enumS)
    {
        AndroidNativeAudio.setVolume(SoundID[(int)enumS], GameManager.Instance.AudioVolume);
        SoundID[(int)enumS] = AndroidNativeAudio.play(FileID[(int)enumS], GameManager.Instance.AudioVolume);
        AndroidNativeAudio.setVolume(SoundID[(int)enumS], GameManager.Instance.AudioVolume);
        //for (int i = 0; i < System.Enum.GetValues(typeof(eSound)).Length; i++)
        //{
        //    SoundID[i] = AndroidNativeAudio.play(FileID[i]);
        //}
        Debug.Log("GameManager.Instance.AudioVolume" + GameManager.Instance.AudioVolume);
    }
    // 음원 재생 (음원, 반복 여부, 음량)
    public void PlaySound(string _clip, bool _loop = false, float _volume = 1.0f)
    {
        for (int i = 0; i < soundMaxNum; i++)
        {
            // 재생 중이 아닌 오디오 소스 탐색
            if (audioSource[i].isPlaying == false)
            {
                //오디오 클립 변경
                audioSource[i].clip = DicAudioClip[_clip];

                // 반복 설정
                audioSource[i].loop = _loop;

                // 볼륨 설정
                audioSource[i].volume = _volume;

                // 재생
                audioSource[i].Play();

                break;
            }
        }
    }

    // 음원 정지
    public void StopSound(string _clip)
    {
        for (int i = 0; i < soundMaxNum; i++)
        {
            // 재생 중인 오디오 소스 탐색
            if (audioSource[i].isPlaying == true)
            {
                // 오디오 클립 이름 확인
                if (audioSource[i].clip.name == _clip)
                {
                    // 정지
                    audioSource[i].Stop();
                }
            }
        }
    }

    // 모든 음원 재생 정지
    public void StopAllSound()
    {
        for (int i = 0; i < soundMaxNum; i++)
        {
            audioSource[i].Stop();
        }
    }

    // 모든 사운드 볼륨 변경
    public void SetAllVolume(float _value)
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i].volume = _value;
        }
    }
}
