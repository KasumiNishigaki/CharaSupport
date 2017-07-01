using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour {

	DataManager dataManager;
	AudioPlayer audioPlayer;
	bool alpha_on;
	float alpha;
	public GameObject titleObject; //点滅させたい文字

	// Use this for initialization
	void Start () {
		
		dataManager = DataManager.Instance;
		audioPlayer = AudioPlayer.Instance;

		dataManager.LoadSaveDataTest();
		audioPlayer.VolumeDataLoad ();
		//dataManager.DummyData ();
		alpha = 1.0f;
		titleObject.GetComponent<CanvasRenderer> ().SetAlpha (alpha);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)){
			Application.LoadLevel ("Home");
		}

		if (alpha_on == true){
			if(titleObject.GetComponent<CanvasRenderer>().GetAlpha() >= 1.0f){
				alpha_on = false;
			}
			alpha += 0.02f; 
			titleObject.GetComponent<CanvasRenderer>().SetAlpha(alpha);
		}else{
			if(titleObject.GetComponent<CanvasRenderer>().GetAlpha() <= 0.1f){
				alpha_on = true;
			}
			alpha += -0.02f; 
			titleObject.GetComponent<CanvasRenderer>().SetAlpha(alpha);
		}
	}

	public void SelectBoy(){
		dataManager.gender = "男";
		dataManager.GenderSaveData ();
	}

	public void SelectGirl(){
		dataManager.gender = "女";
		dataManager.GenderSaveData ();

	}
}
