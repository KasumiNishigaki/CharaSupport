using UnityEngine;
using System.Collections;

public class SceneLoadManager : MonoBehaviour {

	DataManager dataManager;

	public string nextScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SceneLoad (){
		dataManager = DataManager.Instance;

		//SE再生
		AudioPlayer.PlaySe ("Sounds/SE/20160114Decision");

		//dataManager.LoadSaveData ();
		/*
		Debug.Log ("良いスタンプ　= " + dataManager.goodStampCount);
		if((dataManager.goodStampCount >= 3)||(dataManager.badStampCount >= 3)){
			dataManager.goodStampCount = 0;
			dataManager.UpdateSaveData ();
			if(dataManager.gender == "女"){
				Application.LoadLevel (nextScene);
			}else{
				Application.LoadLevel ("Event");
			}
		}else{
			Application.LoadLevel (nextScene);
		}*/

		if(dataManager.testEvent == 1){
			if(dataManager.gender == "女"){
				Application.LoadLevel (nextScene);
			}else{
				Application.LoadLevel ("Event");
				dataManager.testEvent = 0;
			}
		}else{
			Application.LoadLevel (nextScene);

		}





	}

	public void EventSceneLoad(){
		Debug.Log ("イベントです");
	}
		
}
