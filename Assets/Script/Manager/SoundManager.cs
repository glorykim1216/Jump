using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    static int soundMaxNum = 5; // 사운드 동시 출력 최대 갯수
    public AudioSource[] audioSource = new AudioSource[soundMaxNum];
    private Dictionary<string, AudioClip> DicAudioClip = new Dictionary<string, AudioClip>();
    int FileID;
    int SoundID;
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
        AndroidNativeAudio.makePool();

        //FileID = new int[System.Enum.GetValues(typeof(eSound)).Length];
        //SoundID = new int[System.Enum.GetValues(typeof(eSound)).Length];
        //SoundString = System.Enum.GetNames(typeof(eSound));
        //for (int i=0; i< System.Enum.GetValues(typeof(eSound)).Length; i++)
        //{
        //    //Debug.Log(SoundString[i]);
        //    if(i==0)
        //        FileID[i] = AndroidNativeAudio.load("Sound/" + SoundString[i] + ".mp3");
        //    else
        //        FileID[i] = AndroidNativeAudio.load("Sound/" + SoundString[i] + ".wav");

        //    Debug.Log(SoundString[i]);


        //}

        //FileID = AndroidNativeAudio.load("Sound/drip.wav");
        //SoundID = AndroidNativeAudio.play(FileID);
        //SoundID[1] = AndroidNativeAudio.play(FileID[1], 1, -1, 1, 1);

    }

    void OnApplicationQuit()
    {
        // Clean up when done
        //for (int i = 0; i < System.Enum.GetValues(typeof(eSound)).Length; i++)
        //{
            
        //}
        AndroidNativeAudio.unload(FileID);
        AndroidNativeAudio.releasePool();
    }
    public void PlaySound(eSound _clip, int _loop = 0, float _volume = 1.0f)
    {
        //SoundID[FileID[(int)_clip]] = AndroidNativeAudio.play(FileID[(int)_clip], _volume,-_volume,1, _loop);
        
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
