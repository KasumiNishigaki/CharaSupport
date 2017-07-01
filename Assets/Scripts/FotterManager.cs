using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Globalization;

public class FotterManager : MonoBehaviour {

	DataManager dataManager;

	public Text todayText;

	public GameObject up_btn;
	public List<Sprite> upBtnImage;
	public GameObject fotter;
	private bool fotter_iTween;
	Vector3 defaultPosition;
	Vector3 transPosition;
	public Image parameter;

	// Use this for initialization
	void Start () {
		dataManager = DataManager.Instance;

		//現在のスタイルをロード
		//dataManager.LoadStyleSaveData ();

		//パラメーターの角度を変える
		int style = dataManager.nowStyle;
		if(style >= 6){
			parameter.transform.Rotate(new Vector3(0, 0, 1), -92);
		}else if(style == 5){
			parameter.transform.Rotate(new Vector3(0, 0, 1), -75);
		}else if(style == 4){
			parameter.transform.Rotate(new Vector3(0, 0, 1), -60);
		}else if(style == 3){
			parameter.transform.Rotate(new Vector3(0, 0, 1), -45);
		}else if(style == 2){
			parameter.transform.Rotate(new Vector3(0, 0, 1), -30);
		}else if(style == 1){
			parameter.transform.Rotate(new Vector3(0, 0, 1), -15);
		}else if(style == -1){
			parameter.transform.Rotate(new Vector3(0, 0, 1), 15);
		}else if(style == -2){
			parameter.transform.Rotate(new Vector3(0, 0, 1), 30);
		}else if(style == -3){
			parameter.transform.Rotate(new Vector3(0, 0, 1), 45);
		}else if(style == -4){
			parameter.transform.Rotate(new Vector3(0, 0, 1), 60);
		}else if(style == -5){
			parameter.transform.Rotate(new Vector3(0, 0, 1), 75);
		}else if(style <= -6){
			parameter.transform.Rotate(new Vector3(0, 0, 1), 92);
		}

		DateTime date = DateTime.Now;

		string year = date.ToString ("yyyy");
		string month = date.ToString ("MM");
		string day = date.ToString ("dd");


		string dateStr = year + "-" + month + "-" + day;

		dataManager.testDateStr = dateStr;

		//日付データのロード
		//dataManager.LoadDateData ();

		//日付変更したときにeatCountを0にする
		if(dateStr != dataManager.date_str){
			dataManager.date_str = dateStr;
			dataManager.ResetEatedSaveData ();
		}

		CultureInfo info = new CultureInfo("ja-JP");
		string dayOfWeek = date.ToString ("ddd", info);

		todayText.text = year + "年" + month + "月" + day + "日" + "(" + dayOfWeek + ")";

		defaultPosition = fotter.transform.localPosition;
		transPosition = defaultPosition;
		transPosition.y += 155;

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void FotterUpButton(){

		if(fotter_iTween == false){
			Sprite setMenuBtn = upBtnImage [1];
			up_btn.GetComponent<Image> ().sprite = setMenuBtn;

			iTween.MoveTo(fotter,iTween.Hash (
				"position",transPosition,
				"time", 0.8f,
				"islocal",true,
				"oncompletetarget",this.gameObject,
				"easetype",iTween.EaseType.easeOutQuart));
			fotter_iTween = true;
		}else{
			fotter_iTween = false;
			Sprite setMenuBtn = upBtnImage [0];
			up_btn.GetComponent<Image> ().sprite = setMenuBtn;

			iTween.MoveTo(fotter,iTween.Hash (
				"position", defaultPosition,
				"time", 0.8f,
				"islocal",true,
				"oncompletetarget",this.gameObject,
				"easetype",iTween.EaseType.easeOutQuart));
		}

	}

	public void ScreenShotBtn(){
		dataManager = DataManager.Instance;
		dataManager.ResetEatedSaveData ();
	}

	public void TweetBtn(){
		//dataManager = DataManager.Instance;
		//dataManager.TestStampFull ();
		Application.LoadLevel ("Title");
	}

}
