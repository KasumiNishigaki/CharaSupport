using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class CalendarManager : MonoBehaviour {

	DataManager dataManager;

	private string year;
	private string month;
	private string day;
	public string dateStr;

	public int daysInMonth;
	public int firstDayOfWeek;

	public Text yearText;

	public Text monthText;
	public int _month;

	int year_i;

	public Button RightBtn;
	public Button LeftBtn;

	public List<GameObject> calendarCell;
	public GameObject cell;
	public GameObject canvas;

	public List<Vector3> firstCellList;
	private int dayOfWeekCount;

	List<GameObject> listCubes;

	public List<Sprite> stampImages;
	string _day;

	string ca_dateStr;



	// Use this for initialization
	void Awake () {
		dataManager = DataManager.Instance;

		DateTime date = DateTime.Now;

		year = date.ToString ("yyyy");
		month = date.ToString ("MM");
		day = date.ToString ("dd");
		dateStr = year + "-" + month + "-" + day;
		//Debug.Log ("dateStr = " + dateStr);

		//DataManagerに保存
		dataManager.year = year;
		dataManager.month = month;
		dataManager.day = day;
		dataManager.dateStr = dateStr;

		//始めだけ今日の日付
		ca_dateStr = dateStr;

		//年
		yearText.text = year + "年";

		// 取得する値: 月
		_month = System.DateTime.Now.Month;
		monthText.text = _month + "月";


		//その月に何日あるか
		daysInMonth = CalendarUtil.GetDaysInMonth (int.Parse(year), int.Parse(month));
		//_daysInMonth = daysInMonth;
		Debug.Log ("daysInMonth = " + daysInMonth);

		//その月の１日は何曜日か
		firstDayOfWeek = CalendarUtil.GetFirstDayOfWeek (dateStr);
		//_firstDayOfWeek = firstDayOfWeek;
		Debug.Log("firstDayOfWeek = " + firstDayOfWeek);
	}

	void Start(){


		listCubes = new List<GameObject> ();

		firstCellList.Add (new Vector3 (-256f, 245f, 0.0f));
		firstCellList.Add (new Vector3 (-171f, 245f, 0.0f));
		firstCellList.Add (new Vector3 (-86f, 245f, 0.0f));
		firstCellList.Add (new Vector3 (-1f, 245f, 0.0f));
		firstCellList.Add (new Vector3 (84f, 245f, 0.0f));
		firstCellList.Add (new Vector3 (169f, 245f, 0.0f));
		firstCellList.Add (new Vector3 (254f, 245f, 0.0f));

		SetCalendar (daysInMonth,firstDayOfWeek);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCalendar(int daysInMonth , int firstDayOfWeek ){

		dataManager = DataManager.Instance;
		//データのロード
		//dataManager.LoadSaveData ();

		//始めのcellの座標を変数に入れる
		Vector3 placePosition = firstCellList[firstDayOfWeek];

		//曜日の数字を入れる
		dayOfWeekCount = firstDayOfWeek;

		//Vector3 placePosition = cell_1.GetComponent<RectTransform>().localPosition;
		//Debug.Log ("placePosition1 = " + placePosition);

		//何年何月までを取得する。先頭の文字のインデックスは0。
		string s1 = ca_dateStr;
		string s2 = s1.Substring(0, 8);
		//Debug.Log (s2);

		for(int i = 0; i < daysInMonth; i++){
			//日にち表示
			GameObject obj = (GameObject)Instantiate(cell, placePosition, Quaternion.identity);
			obj.transform.parent = canvas.transform;
			GameObject dayText = obj.transform.Find("dayText").gameObject;
			GameObject stampObject = obj.transform.Find("stamp").gameObject;
			int day = i + 1;
			dayText.GetComponent<Text>().text = day.ToString();

			if (day < 10){
				_day = "0" + day;
			}else{
				_day = day.ToString();
			}
				
			//スタンプ表示
			if(dataManager.stampDic.ContainsKey(s2 + _day)){
				Sprite stamp_sprite = stampImages[dataManager.stampDic [s2 + _day]];
				stampObject.GetComponent<Image>().sprite = stamp_sprite;
			}

			//1日以外はx座標を長さ分増やす
			if(i != 0){
				//placePositionのx座標を変更
				placePosition.x += 86;
			}

			//日曜になったら一段下げる
			if((i != 0)&&(dayOfWeekCount == 0)){
				placePosition.x = firstCellList[0].x;
				placePosition.y -= 89f;
			}

			//Debug.Log ("placePosition3 = " + placePosition);
			obj.GetComponent<RectTransform> ().localPosition = placePosition;
			obj.GetComponent<RectTransform> ().localScale = new Vector3(1,1,1);

			if(dayOfWeekCount == 6){
				dayOfWeekCount = 0;
			}else{
				dayOfWeekCount++;
			}

			calendarCell.Add (obj);
		}


	}

	public void OnTapRightBtn(){
		//calendarCellの中身を空にする
		foreach (GameObject go in calendarCell) {
			Destroy (go);
		}
		//calendarCellのリストを空にする
		calendarCell.Clear();

		Destroy (GameObject.Find("CalendarCell(Clone)"));

		//月を１つ増やす
		_month = _month + 1;

		//三角ボタンをおした時の年月を変える
		if(_month > 12){
			_month = 1;
			int year_i = int.Parse(year) + 1 ;
			year = year_i.ToString ();
		}else if(_month < 0){
			_month = 12;
			int year_i = int.Parse(year) - 1;
			year = year_i.ToString ();
		}

		yearText.text = year + "年";
		monthText.text = _month + "月";

		//二桁用monthString
		string s_month = "";
		//_monthが9以下なら二桁にする
		if(_month < 10){
			s_month = "0" + _month;
		}else{
			s_month = _month.ToString();
		}
			
		dateStr = year + "-" + s_month + "-" + day;
		ca_dateStr = dateStr;

		//その月の1日は何曜日か
		int nextDaysInMonth = CalendarUtil.GetDaysInMonth (int.Parse(year), _month);
		int nextFirstDayOfWeek = CalendarUtil.GetFirstDayOfWeek (dateStr);

		SetCalendar(nextDaysInMonth, nextFirstDayOfWeek);
	}

	public void OnTapLeftBtn(){
		//calendarCellの中身を空にする
		foreach (GameObject go in calendarCell) {
			Destroy (go);
		}
		//calendarCellのリストを空にする
		calendarCell.Clear();

		Destroy (GameObject.Find("CalendarCell(Clone)"));

		//月を１つ減らす
		_month = _month - 1;

		//三角ボタンをおした時の年月を変える
		if(_month > 12){
			_month = 1;
			int year_i = int.Parse(year) + 1 ;
			year = year_i.ToString ();
		}else if(_month < 1){
			_month = 12;
			int year_i = int.Parse(year) - 1 ;
			year = year_i.ToString ();
		}

		yearText.text = year + "年";
		monthText.text = _month + "月";

		//二桁用monthString
		string s_month = "";
		//_monthが9以下なら二桁にする
		if(_month < 10){
			s_month = "0" + _month;
		}else{
			s_month = _month.ToString();
		}

		dateStr = year + "-" + s_month + "-" + day;
		ca_dateStr = dateStr;

		int nextDaysInMonth = CalendarUtil.GetDaysInMonth (int.Parse(year), _month);
		int nextFirstDayOfWeek = CalendarUtil.GetFirstDayOfWeek (dateStr);

		SetCalendar(nextDaysInMonth, nextFirstDayOfWeek);
	}
}
