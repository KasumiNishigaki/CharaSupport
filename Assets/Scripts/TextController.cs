using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour 
{
	DataManager dataManager;

	string[] scenarios;
	[SerializeField] Text uiText;

	[SerializeField][Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;

	private string currentText = string.Empty;
	private float timeUntilDisplay = 0;
	private float timeElapsed = 1;
	private int currentLine = 0;
	private int lastUpdateCharacter = -1;

	private Animator anim;    // Animator
	public GameObject hakase;

	public GameObject yesBtn;
	public GameObject noBtn;

	public string nextScene;

	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText 
	{
		get { return  Time.time > timeElapsed + timeUntilDisplay; }
	}

	void Start()
	{
		dataManager = DataManager.Instance;

			scenarios = new string[]{
				"おい！そこのきみ！", "ここだ！下！！","にゃー","いきなりだが君、イケメンか美少女と一緒に生活してみたくはないか？",
				"そんな君にちょうどいい依頼があるんのだ！私の研究所に来たまえ！", "何！？そんなはずはない！とにかく私の研究所にくるのだ！",
				"研究所には男女がいる。", "この二人はロボットなのだ。私はロボット研究家。この二人は私が作った最新型家族・恋人ロボットだ。",
				"この家族・恋人ロボットに新たなオプションとしてダイエットサポート機能をつけてみたのだが・・・", 
				"いかんせん私だけの実験記録では心もとない、そこでだ、君にダイエットサポート機能の被験体になってほしいのだ。",
				"とにもかくにも一緒に住みたい方を選んでみてくれ"};

			// Animatorを取得
			anim = hakase.GetComponent<Animator>();

			
		SetNextLine();
	}

	void Update () 
	{
		// 文字の表示が完了してるならクリック時に次の行を表示する
		if( IsCompleteDisplayText ){
			anim.SetBool ("speak", false);
			if(currentLine < scenarios.Length && Input.GetMouseButtonDown(0)){
				SetNextLine();
			}
		}else{
			anim.SetBool ("speak", true);
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


	void SetNextLine()
	{	anim.SetBool ("speak", true);
		currentText = scenarios[currentLine];
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		currentLine ++;
		lastUpdateCharacter = -1;
	}

	public void YesBtn(){
		yesBtn.SetActive (true);
		noBtn.SetActive (true);
	}

	public void FinishEvent(){
		Application.LoadLevel (nextScene);
	}
}
