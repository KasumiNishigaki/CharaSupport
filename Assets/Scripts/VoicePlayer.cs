using UnityEngine;
using System.Collections;
using System;

public class VoicePlayer : MonoBehaviour {

	private static VoicePlayer _instance;
	public static VoicePlayer Instance{
		get{
			if(_instance == null){
				GameObject go = new GameObject ("VoicePlayer");
				_instance = go.AddComponent<VoicePlayer> ();
			}
			//初期化しない/シングルトンだけでつかってあげると良い
			DontDestroyOnLoad (_instance);
			return _instance;
		}
	}

	private GameObject rootObject;
	private string voiceFileName;

	private GameObject live2DModel;

	public float voiceVolume = 1f;

	void Awake(){
		print ("awake");
		live2DModel = GameObject.Find ("Live2DModel");
	}

	//Voiceの再生
	public static AudioSource PlayVoice(string fileName){
		return Instance.DoPlayVoice (fileName);
	}
		
	//Voiceの再生
	public AudioSource DoPlayVoice(string fileName){
		if (live2DModel == null) live2DModel = GameObject.Find ("Live2DModel");

		AudioClip clip = Resources.Load (fileName, typeof(AudioClip)) as AudioClip;
		live2DModel.GetComponent<AudioSource>().clip = clip;
		if(clip == null)
			return null;

		live2DModel.GetComponent<AudioSource>().volume = voiceVolume;
		live2DModel.GetComponent<SimpleModel>().PlayVoice_ ();;

		return live2DModel.GetComponent<AudioSource>();
	}

	//ボイスボリュームデータの保存
	public void SaveVoiceVolumeData(){
		ES2.Save (voiceVolume, "voiceVolume");
	}
		
	//ボイスボリュームデータのロード
	public void VoiceVolumeDataLoad(){
		if (ES2.Exists ("voiceVolume")){
			voiceVolume = ES2.Load<float>("voiceVolume");
		}else{
			voiceVolume = 1f;
		}
	}
}
