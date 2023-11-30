using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //播放的音效
    private AudioSource bgmSource;

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayBGM(string name , bool isLoop = true)
    {
        //加载bgm声音剪辑
        AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);

        bgmSource.clip =clip;//设置音频

        bgmSource.loop = isLoop;

        bgmSource.Play();

    }

    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name);
        Debug.Log("播放声音");
        AudioSource.PlayClipAtPoint(clip , transform.position);
    }
}
