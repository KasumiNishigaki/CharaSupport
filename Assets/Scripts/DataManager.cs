using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {
	
	/*
	public static DataManager Instance { get; private set; }

	private void Awake() {
		if (Instance != null) {
			DestroyImmediate(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
	*/

	private static DataManager _instance;
	public static DataManager Instance{
		get{
			if(_instance == null){
				GameObject go = new GameObject ("DataManager");
				_instance = go.AddComponent<DataManager> ();
			}
			//初期化しない/シングルトンだけでつかってあげると良い
			DontDestroyOnLoad (_instance);
			return _instance;
		}
	}

	public int stampId;
	public bool calendarRoot;
	public int rice_balance = 3;
	public int meat_balance = 3;
	public int vegetable_balance = 4;

	public string year;
	public string month;
	public string day;
	public string dateStr;

	public bool morningEated;
	public bool lunchEated;
	public bool dinnerEated;

	public int eatCount;

	public int totalAll_1;
	public int totalAll_2;

	public string date_str;

	public int beforeStyle;
	public int nowStyle;

	public int eventStyle;

	public string testDateStr;

	public int goodStampCount;
	public int badStampCount;

	public string gender = "男";

	public int testEvent;


	public Dictionary<string, int> stampDic = new Dictionary<string, int> ();


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//データのロード
	public void LoadSaveDataTest(){
		/* ロードするデータがあるか確認 */
		if (ES2.Exists ("myStamp")){
			//カレンダーDicのロード.
			stampDic = ES2.LoadDictionary<string, int>("myStamp");
		}
		if (ES2.Exists ("goodStampCount")){
			//良いスタンプ何回続いているか
			goodStampCount = ES2.Load<int>("goodStampCount");
		}
		if (ES2.Exists ("badStampCount")){
			//悪いスタンプ何回続いているか
			badStampCount = ES2.Load<int>("badStampCount");
		}
		if(ES2.Exists("eatValue1")){
			//1回目の食事データのロード
			totalAll_1 = ES2.Load<int>("eatValue1");
		}
		if(ES2.Exists("eatValue2")){
			//2回目の食事データのロード
			totalAll_2 = ES2.Load<int>("eatValue2");
		}
		//朝食を食べたかのデータロード
		if(ES2.Exists("morningEated")){
			morningEated = ES2.Load<bool>("morningEated");
		}
		//昼食を食べたかのデータロード
		if(ES2.Exists("lunchEated")){
			lunchEated = ES2.Load<bool>("lunchEated");
		}
		//夜食を食べたかのでデータロード
		if(ES2.Exists("dinnerEated")){
			dinnerEated = ES2.Load<bool>("dinnerEated");
		}
		//1日に入力した回数のロード
		if(ES2.Exists("eatCount")){
			eatCount = ES2.Load<int>("eatCount");
		}
		//日付データのロード
		if(ES2.Exists("dateString")){
			date_str = ES2.Load<string>("dateString");
		}
		//現在のスタイルデータのロード
		if(ES2.Exists("styleInt")){
			nowStyle = ES2.Load<int>("styleInt");
		}
		//現在のスタイルデータのロード
		if(ES2.Exists("gender")){
			gender = ES2.Load<string>("gender");
		}
			
	}

	//スタンプ情報のセーブ
	public void UpdateSaveData(){
		ES2.Save (stampDic, "myStamp");
		ES2.Save (goodStampCount, "goodStampCount");
		ES2.Save (badStampCount, "badStampCount");
	}

	//食事バランス数値セーブ
	public void UpdateValueSaveData(){
		ES2.Save (totalAll_1, "eatValue1");
		ES2.Save (totalAll_2, "eatValue2");
	}


	//ダミー
	public void DummyData(){
		
		stampDic.Add ("2016-06-03",1);
		stampDic.Add ("2016-06-04",0);
		stampDic.Add ("2016-06-07",2);
		stampDic.Add ("2016-06-09",1);
		stampDic.Add ("2016-06-10",1);
		stampDic.Add ("2016-06-11",3);
		stampDic.Add ("2016-06-12",2);
		stampDic.Add ("2016-06-13",1);
		stampDic.Add ("2016-06-14",2);
		stampDic.Add ("2016-06-15",0);
		stampDic.Add ("2016-06-20",0);
		stampDic.Add ("2016-06-22",1);
		stampDic.Add ("2016-06-23",3);
		UpdateSaveData ();

	}

	//食事記録セーブ
	public void EatedSaveData(){
		ES2.Save (morningEated, "morningEated");
		ES2.Save (lunchEated, "lunchEated");
		ES2.Save (dinnerEated, "dinnerEated");
		ES2.Save (eatCount, "eatCount");
	}

	//食事記録リセット
	public void ResetEatedSaveData(){
		morningEated = false;
		lunchEated = false;
		dinnerEated = false;

		totalAll_1 = 0;
		totalAll_2 = 0;

		eatCount = 0;

		if(stampDic.ContainsKey(testDateStr)){
			stampDic.Remove(testDateStr);
		}

		//日付保存
		ES2.Save (date_str, "dateString");
		//保存
		EatedSaveData ();
		UpdateSaveData ();
	}

	public void StyleSaveData(){
		//スタイル保存
		ES2.Save (nowStyle, "styleInt");
	}

	public void TestStampFull(){
		goodStampCount = 3;
	}

	public void GenderSaveData(){
		//スタイル保存
		ES2.Save (gender, "gender");
	}


}
