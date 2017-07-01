using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextController2 : MonoBehaviour 
{
	DataManager dataManager;

	string[] scenarios;
	[SerializeField] Text uiText;
	public Text nameText;

	[SerializeField][Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;

	private string currentText = string.Empty;
	private float timeUntilDisplay = 0;
	private float timeElapsed = 1;
	private int currentLine = 0;
	private int lastUpdateCharacter = -1;

	public GameObject boyStill;
	//public GameObject blackObject;

	public GameObject live2dCharacter;
	public Dictionary<int, string> eventBoyDic = new Dictionary<int, string> ();

	string[] nameString;
	private int i;

	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText 
	{
		get { return  Time.time > timeElapsed + timeUntilDisplay; }
	}

	void Start()
	{
		dataManager = DataManager.Instance;

		//if (dataManager.gender == "女") {
			/*nameString = new string[]{
				"大江エト","あなた"
			};
			scenarios = new string[]{
				"すぅ......はぁーーーー", "何をしているんですか？","ん？ああ、腹式呼吸だよ","は、はあ。またどうして突然...",
				"アハハ、君が健康的な食生活を心がけてくれているおかげ、かな。\nまあなんていうか...つい体を鍛えたくなったんだよ。",
				"なるほど...(ダイエットオプションがついた人型ロボットは真面目なんだなぁ)\nそうだ、喉渇いてませんか？よかったらスポーツドリンク買ってきますよ。", 
				"確かに渇いてるなぁ。それじゃ、お願いしようかな。", "はい！","......数分後",
				"戻りました。はい、どうぞ", "ありがとう！いただきます。\nゴク...ゴク...プハ〜〜美味しい。最高！","美味しいですか？",
				"うん、すっごく。君って気が利くんだね。ありがとう！"};
		}else{*/
			nameString = new string[]{
				"けんこう","あなた"
			};
			scenarios = new string[]{
				"すぅ......はぁーーーー", "何をしているんですか？","ん？ああ、腹式呼吸だよ","は、はあ。またどうして突然...",
				"アハハ、君が健康的な食生活を心がけてくれているおかげ、かな。\nまあなんていうか...つい体を鍛えたくなったんだよ。",
				"なるほど...(ダイエットオプションがついた人型ロボットは真面目なんだなぁ)\nそうだ、喉渇いてませんか？よかったらスポーツドリンク買ってきますよ。", 
				"確かに渇いてるなぁ。それじゃ、お願いしようかな。", "はい！","......数分後",
				"戻りました。はい、どうぞ", "ありがとう！いただきます。\nゴク...ゴク...プハ〜〜美味しい。最高！","美味しいですか？",
				"うん、すっごく。君って気が利くんだね。ありがとう！"};
		//}
			
		SetNextLine();
		SetNameText ();
	}


	void Update () 
	{
		// 文字の表示が完了してるならクリック時に次の行を表示する
		if( IsCompleteDisplayText ){
			//live2dモデルの口の動きを止める
			live2dCharacter.GetComponent<SimpleModelEvent>().finSpeak = true;
			if(currentLine < scenarios.Length && Input.GetMouseButtonDown(0)){
				if(currentLine >= 11){
					boyStill.SetActive (true);
				}
				SetNextLine();
				SetNameText ();
			}else if(currentLine >= scenarios.Length && Input.GetMouseButtonDown(0)){
				FinishEvent ();
			}
		}else{
			// 完了してないなら文字をすべて表示する
			if(Input.GetMouseButtonDown(0)){
				timeUntilDisplay = 0;
			}
		}

		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		if( displayCharacterCount != lastUpdateCharacter ){
			uiText.text = currentText.Substring(0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}

	}

	void SetNameText(){
		nameText.text = nameString [i];
		if(currentLine == 9){
			nameText.text = "";
		}
		if(i == 0){
			i = 1;
		}else{
			i = 0;
		}

	}

	void SetNextLine()
	{	
		if(i == 0){
			//live2dモデルの口の動きを始める
			live2dCharacter.GetComponent<SimpleModelEvent>().finSpeak = false;
		}
		currentText = scenarios[currentLine];
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		currentLine ++;
		lastUpdateCharacter = -1;
	}

	public void FinishEvent(){
		
		Application.LoadLevel ("Home");
	}
		
}
