using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
/**
 * 音频管理器
 */
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private Dictionary<string , AudioClip> audios = new Dictionary<string , AudioClip>();
    private AudioSource audioShot;
    private AudioSource audioScroe;

    //初始化
    public void Awake() {
        instance = this;
        audioShot = new GameObject("audioShot").AddComponent<AudioSource>();
        audioScroe = new GameObject("audioScroe").AddComponent<AudioSource>();
        var assets = Resources.LoadAll<AudioClip>("sounds");
        foreach (var clip in assets)
        {
            audios.Add(clip.name , clip);
        }
    }
    /// <summary>
    ///  播放声音
    /// </summary>
    public void AudioShotPlay(string audioName) {
        audioShot.clip = GetAudioClip(audioName);
        audioShot.Play();
    }
    public void AudioScorePlay(string audioName)
    {
        audioScroe.clip = GetAudioClip(audioName);
        audioScroe.Play();
    }

    private AudioClip GetAudioClip(string audioName){
        if(!audios.ContainsKey(audioName)) return null;
        return audios[audioName];
    }

}