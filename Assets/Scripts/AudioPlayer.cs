using UnityEngine;
using System;
using System.Collections;


//Resources以下に入っているオーディオリソースを再生する
//BGM: リピート再生する
//SE: 単発再生、複数のオーディオをチャンネル数の数だけ同時再生する
public class AudioPlayer{

	private static int DEFAULT_SE_CHANNEL_COUNT = 8;

	private static AudioPlayer _instance;
	public static AudioPlayer Instance{
		get{
			if(_instance == null){
				_instance = new AudioPlayer (DEFAULT_SE_CHANNEL_COUNT);
			}
			return _instance;
		}
	}

	private GameObject rootObject;
	private string bgmFileName;
	public AudioSource bgmChannel;
	private int seChannelCount;

	private  AudioSource[] seChannels;
	private int seChannelIndex;
	public AudioSource seChannel;

	public float bgmVolume = 0.7f;
	public float seVolume = 1f;


	public AudioPlayer(int seChannelCount){
		rootObject = new GameObject ("AudioPlayer");
		GameObject.DontDestroyOnLoad (rootObject);

		bgmChannel = rootObject.AddComponent<AudioSource>() as AudioSource;
		this.seChannelCount = seChannelCount;
		//this.voiceChannelCount = voiceChannelCount;

		seChannels = new AudioSource[seChannelCount];
		for(int i = 0; i < seChannelCount; i++){
			seChannels[i] = rootObject.AddComponent<AudioSource>() as AudioSource;
		}

		seChannelIndex = 0;

	}


	//BGMの再生
	public static bool PlayBgm(string fileName){
		return Instance.DoPlayBgm (fileName);
	}

	//BGMの一時停止
	public static void PauseBgm(bool flag){
		Instance.DoPauseBgm (flag);
	}

	//BGMの停止
	public static void StopBgm(){
		Instance.DoStopBgm ();
	}

	//BGMの再生
	public bool DoPlayBgm(string fileName){

		if(fileName != bgmFileName){
			bgmChannel.Stop();
			bgmFileName = fileName;
			AudioClip clip = Resources.Load (fileName, typeof(AudioClip)) as AudioClip;
			if(clip == null)
				return false;
			
			bgmChannel.clip = clip;
			bgmChannel.loop = true;
			bgmChannel.volume = bgmVolume;
			bgmChannel.Play ();
		}
		return true;
	}


	//BGMの一時停止
	public void DoPauseBgm(bool flag){
		if (flag)
			bgmChannel.Pause ();
		else
			bgmChannel.Play ();
		
	}

	//BGMの停止
	public void DoStopBgm(){
		bgmChannel.Stop ();
		bgmFileName = null;
	}


	//SE再生
	public static AudioSource PlaySe(string fileName){
		return Instance.DoPlaySe (fileName);
	}

	public AudioSource DoPlaySe(string fileName){
		seChannel = seChannels [seChannelIndex];
		seChannel.Stop ();

		AudioClip clip = Resources.Load (fileName, typeof(AudioClip)) as AudioClip;
		if(clip == null)
			return null;

		if(++seChannelIndex >= seChannelCount){
			seChannelIndex = 0;
		}

		seChannel.clip = clip;
		seChannel.volume = seVolume;
		seChannel.pitch = 1.0f;
		seChannel.Play();
		return seChannel;
	}
		
	//ボリュームデータの保存
	public void SaveVolumeData(){
		ES2.Save (bgmVolume, "bgmVolume");
		ES2.Save (seVolume, "seVolume");
	}

	//ボリュームデータのロード
	public void VolumeDataLoad(){
		if (ES2.Exists ("bgmVolume")){
			bgmVolume = ES2.Load<float>("bgmVolume");
		}else{
			bgmVolume = 0.7f;
		}
		if (ES2.Exists ("seVolume")){
			seVolume = ES2.Load<float>("seVolume");
		}else{
			seVolume = 1f;
		}
	}

}

