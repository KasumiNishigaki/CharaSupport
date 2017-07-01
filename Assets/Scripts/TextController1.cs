using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextController1 : MonoBehaviour 
{
	DataManager dataManager;

	//string[] scenarios;
	[SerializeField] Text uiText;
	public Text nameText;

	[SerializeField][Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;

	private string currentText = string.Empty;
	private float timeUntilDisplay = 0;
	private float timeElapsed = 1;
	//private int currentLine = 0;
	private int lastUpdateCharacter = -1;
	private int rdm;
	public Dictionary<int, string> voiceDic = new Dictionary<int, string> ();
	public Dictionary<int, string> scenarioDic = new Dictionary<int, string> ();

	public GameObject live2dCharacter;

	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText 
	{
		get { return  Time.time > timeElapsed + timeUntilDisplay; }
	}

	void Init(){
		rdm = UnityEngine.Random.Range (0,scenarioDic.Count);
	}

	void Start()
	{
		voiceDic.Clear ();
		scenarioDic.Clear ();

		dataManager = DataManager.Instance;
		//BGM再生
		AudioPlayer.PlayBgm ("Sounds/Common/degi_cre_01");

		if(dataManager.gender == "女"){
			nameText.text = "大江エト";
		}else{
			nameText.text = "健崎コウ";
		}

		Init ();

		AddScenarioDic ();
		AddVoiceDic ();
			
		SetNextLine();
	}

	void AddScenarioDic(){
		if (dataManager.gender == "女") {
			scenarioDic.Add(0,"くすぐったいです");
			scenarioDic.Add(1,"水分もしっかりとりましょうね！");
			scenarioDic.Add(2,"調子はどうですか？");
			scenarioDic.Add(3,"食事はゆっくりよく噛んで、です");
			scenarioDic.Add(4,"虫は苦手です");
			scenarioDic.Add(5,"疲れているときの甘いものは許してあげます");
			scenarioDic.Add(6,"洗濯物たたんでおきますね");
			scenarioDic.Add(7,"えへへ");
			scenarioDic.Add(8,"わんちゃんっていいですよねぇ");
			scenarioDic.Add(9,"さっきタケル（猫）と遊んでたんです");
			scenarioDic.Add(10,"今日テレビで面白い番組やってました！");
			scenarioDic.Add(11,"今日もお疲れさまです");
			scenarioDic.Add(12,"実は私ホラーゲーム好きなんですよ");
			scenarioDic.Add(13,"もっと撫でてもいいですよ？");
			scenarioDic.Add(14,"はかせが歩くと髪がピコピコ動いて可愛いんですよね〜");
			scenarioDic.Add(15,"充電切れ・・・です・・・ふふっ嘘ですよ");
			scenarioDic.Add(16,"おかえりなさい、なにしてたんですか？");
			scenarioDic.Add(17,"肩、揉んであげましょうか！");
			scenarioDic.Add(18,"ふぁ・・・あ・・・、ごめんなさい、ねむくってつい");
		}else{
			scenarioDic.Add(0,"よく噛んで食べるんだぞ");
			scenarioDic.Add(1,"食事は始めに野菜から食べると良いそうだ");
			scenarioDic.Add(2,"お疲れさん！");
			scenarioDic.Add(3,"ふう");
			scenarioDic.Add(4,"へ？！");
			scenarioDic.Add(5,"ハハッ、くすぐったいな");
			scenarioDic.Add(6,"早く寝ろよ");
			scenarioDic.Add(7,"睡眠不足はお肌の天敵だぞ？");
			scenarioDic.Add(8,"スマホ閉じて早く寝ろよな");
			scenarioDic.Add(9,"どうした？俺の顔に何かついてるか？");
			scenarioDic.Add(10,"俺も一度は外で飯を食べてみたいもんだ");
			scenarioDic.Add(11,"ふぁ〜ねみ...");
			scenarioDic.Add(12,"後...60...ッハ...っとごめん！筋トレ中さ");
			scenarioDic.Add(13,"嫌なことは寝て忘れようぜ");
			scenarioDic.Add(14,"何か用か？");
			scenarioDic.Add(15,"どうした？何かあったのか？");
			scenarioDic.Add(16,"俺、りんご飴食べたことないんだよなぁ");
			scenarioDic.Add(17,"今日も１日頑張ろうな！");
		}
	}

	void Update () 
	{
		var pos = Input.mousePosition;

		Vector2 worldPoint = Camera.main.ScreenToWorldPoint(pos);
		RaycastHit2D hit = Physics2D.Raycast(worldPoint,Vector2.zero);

		// 文字の表示が完了してるならクリック時に次の行を表示する
		if( IsCompleteDisplayText ){
			if(Input.GetMouseButtonDown(0)){
				if(hit.collider != null ){
					Init ();
					SetNextLine();
					}
			}
		}else{
			// 完了してないなら文字をすべて表示する
			if(Input.GetMouseButtonDown(0)){
				timeUntilDisplay = 0;
			}
		}

		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		if( displayCharacterCount != lastUpdateCharacter ){
			//anim.SetBool ("speak", true);
			uiText.text = currentText.Substring(0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}


	void SetNextLine(){
		PlayVoiceDic ();
		currentText = scenarioDic[rdm];
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		lastUpdateCharacter = -1;
	}

	void AddVoiceDic(){
		if (dataManager.gender == "女") {
			voiceDic.Add (0, "Sounds/Voice/girl_voice/normal/girl_1");
			voiceDic.Add (1, "Sounds/Voice/girl_voice/normal/girl_2");
			voiceDic.Add (2, "Sounds/Voice/girl_voice/normal/girl_3");
			voiceDic.Add (3, "Sounds/Voice/girl_voice/normal/girl_4");
			voiceDic.Add (4, "Sounds/Voice/girl_voice/normal/girl_5");
			voiceDic.Add (5, "Sounds/Voice/girl_voice/normal/girl_6");
			voiceDic.Add (6, "Sounds/Voice/girl_voice/normal/girl_7");
			voiceDic.Add (7, "Sounds/Voice/girl_voice/normal/girl_8");
			voiceDic.Add (8, "Sounds/Voice/girl_voice/normal/girl_9");
			voiceDic.Add (9, "Sounds/Voice/girl_voice/normal/girl_10");
			voiceDic.Add (10, "Sounds/Voice/girl_voice/normal/girl_11");
			voiceDic.Add (11, "Sounds/Voice/girl_voice/normal/girl_12");
			voiceDic.Add (12, "Sounds/Voice/girl_voice/normal/girl_13");
			voiceDic.Add (13, "Sounds/Voice/girl_voice/normal/girl_14");
			voiceDic.Add (14, "Sounds/Voice/girl_voice/normal/girl_15");
			voiceDic.Add (15, "Sounds/Voice/girl_voice/normal/girl_16");
			voiceDic.Add (16, "Sounds/Voice/girl_voice/normal/girl_17");
			voiceDic.Add (17, "Sounds/Voice/girl_voice/normal/girl_18");
			voiceDic.Add (18, "Sounds/Voice/girl_voice/normal/girl_19");
		} else{
			voiceDic.Add (0, "Sounds/Voice/boy_voice/normal/boyvoice_01");
			voiceDic.Add (1, "Sounds/Voice/boy_voice/normal/boyvoice_02");
			voiceDic.Add (2, "Sounds/Voice/boy_voice/normal/boyvoice_03");
			voiceDic.Add (3, "Sounds/Voice/boy_voice/normal/boyvoice_04");
			voiceDic.Add (4, "Sounds/Voice/boy_voice/normal/boyvoice_05");
			voiceDic.Add (5, "Sounds/Voice/boy_voice/normal/boyvoice_06");
			voiceDic.Add (6, "Sounds/Voice/boy_voice/normal/boyvoice_07");
			voiceDic.Add (7, "Sounds/Voice/boy_voice/normal/boyvoice_08");
			voiceDic.Add (8, "Sounds/Voice/boy_voice/normal/boyvoice_09");
			voiceDic.Add (9, "Sounds/Voice/boy_voice/normal/boyvoice_11");
			voiceDic.Add (10, "Sounds/Voice/boy_voice/normal/boyvoice_12");
			voiceDic.Add (11, "Sounds/Voice/boy_voice/normal/boyvoice_13");
			voiceDic.Add (12, "Sounds/Voice/boy_voice/normal/boyvoice_14");
			voiceDic.Add (13, "Sounds/Voice/boy_voice/normal/boyvoice_15");
			voiceDic.Add (14, "Sounds/Voice/boy_voice/normal/boyvoice_16");
			voiceDic.Add (15, "Sounds/Voice/boy_voice/normal/boyvoice_17");
			voiceDic.Add (16, "Sounds/Voice/boy_voice/normal/boyvoice_18");
			voiceDic.Add (17, "Sounds/Voice/boy_voice/normal/boyvoice_47");
		}
	}

	public void PlayVoiceDic(){
		VoicePlayer.PlayVoice (voiceDic[rdm]);
	}
		
		
}
