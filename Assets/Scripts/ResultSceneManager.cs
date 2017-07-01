using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ResultSceneManager : MonoBehaviour {

	DataManager dataManager;

	private RectTransform hoge;
	public Image rice;
	public Image meat;
	public Image vegetable;
	public List<Sprite> sara_fin; 
	public Image sara;

	public Text eatText;
	public Text nameText;

	public GameObject calendar;
	public GameObject btn;

	public List<GameObject> calendarCell;
	int i;
	int k;
	string _k;

	int total_r; //炭水化物の点数
	int total_m; //肉の点数
	int total_v; //野菜の点数

	public List<Sprite> stampImages; 


	//public GameObject calendarManager;

	private string year;
	private string month;
	private string day;
	private string dateStr;
	private int daysInMonth;
	private int firstDayOfWeek;
	public Text monthText;
	public int _month;


	void Awake () {
		dataManager = DataManager.Instance;

		DateTime date = DateTime.Now;

		year = date.ToString ("yyyy");
		month = date.ToString ("MM");
		day = date.ToString ("dd");
		dateStr = year + "-" + month + "-" + day;
		Debug.Log ("dateStr = " + dateStr);

		// 取得する値: 月
		_month = System.DateTime.Now.Month;
		monthText.text = _month + "月";


		//その月に何日あるか
		daysInMonth = CalendarUtil.GetDaysInMonth (int.Parse(year), int.Parse(month));

		//その月の１日は何曜日か
		firstDayOfWeek = CalendarUtil.GetFirstDayOfWeek (dateStr);

	}

	// Use this for initialization
	void Start () {
		dataManager = DataManager.Instance;

		if(dataManager.gender == "女"){
			nameText.text = "大江エト";
		}else{
			nameText.text = "健崎コウ";
		}

		int rice_b = dataManager.rice_balance;
		int meat_b = dataManager.meat_balance;
		int vegetable_b = dataManager.vegetable_balance;

		//ごはん
		if (rice_b == 1) {
			total_r = 1;
			hoge = rice.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 110, 90);

		} else if (rice_b == 2) {
			total_r = 2;
			hoge = rice.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 150, 120);

		} else if (rice_b == 3) {
			total_r = 3;
			hoge = rice.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 200, 170);

		} else if (rice_b == 4) {
			total_r = 4;
			hoge = rice.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 240, 210);

		} else if (rice_b == 5) {
			total_r = 5;
			hoge = rice.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 290, 260);
		}

		//肉
		if (meat_b == 1) {
			total_m = 1;
			hoge = meat.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 130, 100);

		} else if (meat_b == 2) {
			total_m = 2;
			hoge = meat.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 160, 130);

		} else if (meat_b == 3) {
			total_m = 3;
			hoge = meat.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 200, 160);

		} else if (meat_b == 4) {
			total_m = 4;
			hoge = meat.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 250, 200);

		} else if (meat_b == 5) {
			total_m = 5;
			hoge = meat.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 290, 240);
		}

		//野菜
		if (vegetable_b == 1) {
			total_v = 1;
			hoge = vegetable.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 200, 100);

		} else if (vegetable_b == 2) {
			total_v = 2;
			hoge = vegetable.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 240, 140);

		} else if (vegetable_b == 3) {
			total_v = 3;
			hoge = vegetable.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 280, 180);

		} else if (vegetable_b == 4) {
			total_v = 4;
			hoge = vegetable.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 310, 220);

		} else if (vegetable_b == 5) {
			total_v = 5;
			hoge = vegetable.GetComponent<RectTransform>();
			hoge.sizeDelta = new Vector2( 360, 270);
		}


		/*    記録    */
		int totalAll = total_r + total_m + total_v;
		int _totalAll;

		//変わる前のスタイル記憶
		dataManager.beforeStyle = dataManager.nowStyle;

		//1,2回目の食事の時
		if(dataManager.eatCount < 3){
			if(dataManager.eatCount == 1){
				//1回目
				dataManager.totalAll_1 = totalAll;
				//灰色スタンプ記録
				dataManager.stampDic.Add (dateStr, 3);
			}else{
				//2回目
				dataManager.totalAll_2 = totalAll;
			}

			if(totalAll >= 13){

				//テスト
				dataManager.testEvent = 0;

				//食べ過ぎた時
				if(dataManager.beforeStyle > 9){
					//8以上は増えない
				}else if(dataManager.beforeStyle > -3){
					//通常〜大デブ時は1ずつ増える
					//dataManager.nowStyle += 1;
					dataManager.nowStyle += 5;
				}else{
					//ガリ時は2ずつ増える
					//dataManager.nowStyle += 2;
					dataManager.nowStyle += 4;	
				}
			}else if(totalAll > 7){

				//テスト
				dataManager.testEvent = 1;

				//良い食事の時
				if(dataManager.beforeStyle > 0){
					//デブ予備軍〜大デブ時は1ずつ減る
					//dataManager.nowStyle -= 1;
					dataManager.nowStyle -= 4;
				}else if(dataManager.beforeStyle <= -3){
					//ガリ時は1ずつ増える
					//dataManager.nowStyle += 1;
					dataManager.nowStyle += 2;
				}else{
					//通常時は変化なし
				}
			}else{
				//テスト
				dataManager.testEvent = 0;

				//少なかった時
				if(dataManager.beforeStyle > 0){
					//デブ予備軍〜大デブ時は2ずつ減る
					//dataManager.nowStyle -= 2;
					dataManager.nowStyle -= 3;
				}else if(dataManager.beforeStyle > -8){
					//通常〜ガリ時は1ずつ減る
					//dataManager.nowStyle -= 1;
					dataManager.nowStyle -= 4;
				}else{
					//-8以下は減らない
				}
			}
				
		}else if(dataManager.eatCount == 3){
			_totalAll = dataManager.totalAll_1 + dataManager.totalAll_2 + totalAll;

			//1回目の灰スタンプ値を削除
			dataManager.stampDic.Remove (dateStr);
			dataManager.UpdateSaveData ();

			if(_totalAll >= 39){

				//テスト
				dataManager.testEvent = 0;

				//食べ過ぎた時
				dataManager.stampId = 2;
				//悪いスタンプ連続記録
				dataManager.badStampCount++;
				dataManager.goodStampCount = 0;
				if(dataManager.beforeStyle > 9){
					//8以上は増えない
				}else if(dataManager.beforeStyle > -3){
					//通常〜大デブ時は1ずつ増える
					//dataManager.nowStyle += 1;
					dataManager.nowStyle += 5;
				}else{
					//ガリ時は2ずつ増える
					//dataManager.nowStyle += 2;
					dataManager.nowStyle += 4;	
				}
			}else if(_totalAll > 21){

				//テスト
				dataManager.testEvent = 1;

				//良い食事の時
				dataManager.stampId = 1;
				//良いスタンプ連続記録
				dataManager.goodStampCount++;
				dataManager.badStampCount = 0;
				if(dataManager.beforeStyle > 0){
					//デブ予備軍〜大デブ時は1ずつ減る
					//dataManager.nowStyle -= 1;
					dataManager.nowStyle -= 4;
				}else if(dataManager.beforeStyle <= -3){
					//ガリ時は1ずつ増える
					//dataManager.nowStyle += 1;
					dataManager.nowStyle += 2;
				}else{
					//通常時は変化なし
				}
			}else{

				//テスト
				dataManager.testEvent = 0;
				//少なかった時
				dataManager.stampId = 0;
				dataManager.goodStampCount = 0;
				dataManager.badStampCount = 0;
				if(dataManager.beforeStyle > 0){
					//デブ予備軍〜大デブ時は2ずつ減る
					//dataManager.nowStyle -= 2;
					dataManager.nowStyle -= 3;
				}else if(dataManager.beforeStyle > -8){
					//通常〜ガリ時は1ずつ減る
					//dataManager.nowStyle -= 1;
					dataManager.nowStyle -= 4;
				}else{
					//-8以下は減らない
				}
			}

		}
		Debug.Log ("なうスタイル = " + dataManager.nowStyle);
		//nowStyleの保存
		dataManager.StyleSaveData();

		//1.2回目のtotalAll保存
		dataManager.UpdateValueSaveData ();
		//Dicを保存
		dataManager.UpdateSaveData ();

		Invoke("ChangeSara",3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//2秒後に呼び出す
	void ChangeSara(){
		dataManager = DataManager.Instance;

		//データのロード
		//dataManager.LoadSaveData ();
		//dataManager.LoadValueSaveData ();
		//dataManager.LoadStyleSaveData ();

		Sprite setSara = sara_fin [0];
		rice.GetComponent<Image> ().sprite = setSara;
		meat.GetComponent<Image> ().sprite = setSara;
		vegetable.GetComponent<Image> ().sprite = setSara;

		Sprite setSara_ = sara_fin [1];
		sara.GetComponent<Image> ().sprite = setSara_;

		eatText.text = "ごちそうさまでした。";

		//3回目ならカレンダー表示
		if (dataManager.eatCount == 3) {
			Invoke ("StampCalendar", 4);
		} else {
			Invoke ("ShowTapBtn", 3);
		}
	}

	void ShowTapBtn(){
		btn.SetActive (true);
	}

	void StampCalendar(){
		dataManager = DataManager.Instance;

		//データのロード
		//dataManager.LoadSaveData ();

		string s1 = dateStr;
		string s2 = s1.Substring(0, 8);

		//calendarのスタンプと日付を設定
		foreach (GameObject go in calendarCell) {
			if((firstDayOfWeek <= i)&&(k < daysInMonth)){
				k++;
				GameObject dayText = go.transform.Find("Text_day").gameObject;
				GameObject stamp = go.transform.Find("stamp_img").gameObject;
				dayText.GetComponent<Text>().text = k.ToString();
				//int day = k;
				if (k < 10){
					_k = "0" + k;
				}else{
					_k = k.ToString();
				}
				//スタンプ表示
				if(dataManager.stampDic.ContainsKey(s2 + _k)){
					Sprite stamp_sprite = stampImages[dataManager.stampDic [s2 + _k]];
					stamp.GetComponent<Image>().sprite = stamp_sprite;
				}
			}
			i++;
		}

		calendar.SetActive (true);

		Invoke("TodayStamp",1);

	}

	void TodayStamp(){
		dataManager = DataManager.Instance;

		//スタンプSE再生
		AudioPlayer.PlaySe ("Sounds/SE/button50");


		//今日のスタンプをDicに追加
		dataManager.stampDic.Add (dateStr, dataManager.stampId);

		i = 0;
		k = 0;
		_k = "";

		foreach (GameObject go in calendarCell) {
			if ((firstDayOfWeek <= i) && (k < daysInMonth)) {
				k++;

				if (k < 10) {
					_k = "0" + k;
				} else {
					_k = k.ToString ();
				}

				if (_k == day) {
					GameObject stamp = go.transform.Find ("stamp_img").gameObject;

					//スタンプ表示
					if (dataManager.stampDic.ContainsKey (dateStr)) {
						Sprite stamp_sprite = stampImages [dataManager.stampDic [dateStr]];
						stamp.GetComponent<Image> ().sprite = stamp_sprite;
					}
				}

			}
			i++;
		}
		//Dicを保存
		dataManager.UpdateSaveData ();
		btn.SetActive (true);
	}
}
