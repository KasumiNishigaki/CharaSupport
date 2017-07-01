using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	//public List<Sprite> soundBtn;
	DataManager dataManager;
	AudioPlayer audioPlayer;
	VoicePlayer voicePlayer;

	public Slider bgmSlider;
	public Slider seSlider;
	public Slider voiceSlider;


	// Use this for initialization
	void Start () {

		dataManager = DataManager.Instance;
		audioPlayer = AudioPlayer.Instance;
		voicePlayer = VoicePlayer.Instance;
		Debug.Log (audioPlayer.bgmVolume);
		bgmSlider.value = audioPlayer.bgmVolume;
		seSlider.value = audioPlayer.seVolume;
		voiceSlider.value = voicePlayer.voiceVolume;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBgmSliderValueChange(float val){
		audioPlayer = AudioPlayer.Instance;
		audioPlayer.bgmChannel.volume = val;
		audioPlayer.bgmVolume = val;
		audioPlayer.SaveVolumeData ();
	}

	public void OnSeSliderValueChange(float val){
		audioPlayer = AudioPlayer.Instance;
		audioPlayer.seChannel.volume = val;
		audioPlayer.seVolume = val;
		audioPlayer.SaveVolumeData ();
	}


	public void OnVoiceSliderValueChange(float val){
		voicePlayer = VoicePlayer.Instance;
		voicePlayer.voiceVolume = val;
		voicePlayer.SaveVoiceVolumeData ();
	}
}
