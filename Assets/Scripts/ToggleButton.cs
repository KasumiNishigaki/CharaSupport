using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;


public class ToggleButton : MonoBehaviour {
	DataManager dataManager;

	//連携するGameObject
	public Toggle morningToggle;
	public Toggle lunchToggle;
	public Toggle dinnerToggle;

	public GameObject finPanel;

	// Use this for initialization
	void Start () {
		dataManager = DataManager.Instance;

		//入力記録データのロード
		//dataManager.LoadEatedSaveData ();

		//食事回数データのロード
		int eatCount = dataManager.eatCount;

		if(dataManager.morningEated == true){
			morningToggle.GetComponent<Toggle> ().isOn = false;
			morningToggle.interactable = false;
		}
		if(dataManager.lunchEated == true){
			lunchToggle.GetComponent<Toggle> ().isOn = false;
			lunchToggle.interactable = false;
		}
		if(dataManager.dinnerEated == true){
			dinnerToggle.GetComponent<Toggle> ().isOn = false;
			dinnerToggle.interactable = false;
		}


		if((morningToggle.interactable == false)&&(eatCount == 1)){
			//朝のみ入力済の場合、昼をisOnにする
			lunchToggle.GetComponent<Toggle> ().isOn = true;
		}else if((dinnerToggle.interactable == true)&&(eatCount == 2)){
			//朝と昼入力済の場合、夜をisOnにする
			dinnerToggle.GetComponent<Toggle> ().isOn = true;
		}else if((lunchToggle.interactable == true)&&(eatCount == 2)){
			//朝と夜入力済の場合、をisOnにする
			lunchToggle.GetComponent<Toggle> ().isOn = true;

		}

		if(eatCount == 3){
			finPanel.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClick()
	{
		dataManager = DataManager.Instance;

		bool act_m = morningToggle.GetComponent<Toggle>().isOn;
		bool act_l = lunchToggle.GetComponent<Toggle>().isOn;
		bool act_d = dinnerToggle.GetComponent<Toggle>().isOn;

		if(act_m == true){
			//食事済
			dataManager.morningEated = true;
			dataManager.eatCount++;
		}

		if(act_l == true){
			//食事済
			dataManager.lunchEated = true;
			dataManager.eatCount++;
		}

		if(act_d == true){
			//食事済
			dataManager.dinnerEated = true;
			dataManager.eatCount++;
		}

		//入力データ保存
		dataManager.EatedSaveData ();

		//Debug.Log ("act_m = " + act_m);
		//Debug.Log ("act_l = " + act_l);
		//Debug.Log ("act_d = " + act_d);
		//SE再生
		AudioPlayer.PlaySe ("Sounds/SE/20160114Decision");
		Application.LoadLevel ("Result");
	}
}
