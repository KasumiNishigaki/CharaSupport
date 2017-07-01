using UnityEngine;
using System.Collections;
using live2d; //Live2Dライブラリを使用

public class Main : MonoBehaviour {

	DataManager dataManager;

	public TextAsset mocFile; // mocファイル
	public Texture2D[] textures; // テクスチャファイル
	private Live2DModelUnity live2DModel; //Live2Dモデルクラス

	public TextAsset mocFile_debu; // debu_mocファイル
	public Texture2D texture_debu;

	public TextAsset mocFile_bigDebu; // bigDebu_mocファイル
	public Texture2D texture_bigDebu;

	public TextAsset mocFile_gari; // gari_mocファイル
	public Texture2D texture_gari;

	public TextAsset mocFile_girl_normal; // girl_normalファイル
	public Texture2D[] texture_girl_normal;

	public TextAsset mocFile_girl_fat1; // girl_fat1ファイル
	public Texture2D[] texture_girl_fat1;

	public TextAsset mocFile_girl_fat2; // girl_fat2ファイル
	public Texture2D[] texture_girl_fat2;

	public TextAsset mocFile_girl_gari; // girl_gariファイル
	public Texture2D[] texture_girl_gari;

	public GameObject moyamoya;
	public GameObject asease;

	void Start ()
	{
		dataManager = DataManager.Instance;

		//初期化（Live2Dを使用する前に必ず１度だけ呼び出す）
		Live2D.init();
		//dataManager.LoadStyleSaveData ();

		int b_style = dataManager.beforeStyle;
			
		if (dataManager.gender == "女") {
			if(b_style >= 6){
				//大デブ
				live2DModel = Live2DModelUnity.loadModel (mocFile_girl_normal.bytes);
				for (int i = 0; i < texture_girl_normal.Length; i++) {
					live2DModel.setTexture (i, texture_girl_normal [i]);
				}
			}else if(b_style >= 3){
				//デブ
				live2DModel = Live2DModelUnity.loadModel (mocFile_girl_fat1.bytes);
				for (int i = 0; i < texture_girl_fat1.Length; i++) {
					live2DModel.setTexture (i, texture_girl_fat1 [i]);
				}

			}else if(b_style >= -2){
				//通常
				live2DModel = Live2DModelUnity.loadModel (mocFile_girl_normal.bytes);
				for (int i = 0; i < texture_girl_normal.Length; i++) {
					live2DModel.setTexture (i, texture_girl_normal [i]);
				}
			}else if(b_style >= -5){
				//もやアニメーション
				Instantiate(moyamoya, new Vector3(80.6f, -2f, 0), Quaternion.identity);
				//通常
				live2DModel = Live2DModelUnity.loadModel (mocFile_girl_normal.bytes);
				for (int i = 0; i < texture_girl_normal.Length; i++) {
					live2DModel.setTexture (i, texture_girl_normal [i]);
				}
			}else{
				//ガリ
				live2DModel = Live2DModelUnity.loadModel (mocFile_girl_normal.bytes);
				for (int i = 0; i < texture_girl_normal.Length; i++) {
					live2DModel.setTexture (i, texture_girl_normal [i]);
				}
			}
		}else{
			if(b_style >= 6){
				//大デブ
				live2DModel = Live2DModelUnity.loadModel ( mocFile_bigDebu.bytes );
				live2DModel.setTexture( 0, texture_bigDebu );

			}else if(b_style >= 3){
				//デブ
				live2DModel = Live2DModelUnity.loadModel ( mocFile_debu.bytes );
				live2DModel.setTexture( 0, texture_debu );

			}else if(b_style >= -2){
				//通常
				live2DModel = Live2DModelUnity.loadModel ( mocFile.bytes );
				for (int i = 0; i < textures.Length; i++)
				{
					live2DModel.setTexture(i, textures[i]);
				}
			}else if(b_style >= -5){
				//もやアニメーション
				Instantiate(moyamoya, new Vector3(80.6f, -2f, 0), Quaternion.identity);
				//通常
				live2DModel = Live2DModelUnity.loadModel ( mocFile.bytes );
				for (int i = 0; i < textures.Length; i++)
				{
					live2DModel.setTexture(i, textures[i]);
				}
			}else{
				//ガリ
				live2DModel = Live2DModelUnity.loadModel ( mocFile_gari.bytes );
				live2DModel.setTexture( 0, texture_gari );
			}
		}


		Invoke("ChangeModel",3);
	}

	void Update ()
	{

	}

	// デフォルトでは DrawMeshNow でLive2Dモデルを描画するので OnRenderObject を使う
	void OnRenderObject ()
	{

		float modelWidth = live2DModel.getCanvasWidth();
		//表示位置と大きさの指定
		Matrix4x4 m1 = Matrix4x4.Ortho( 0, modelWidth, modelWidth, 0, -50.0f, 50.0f);
		Matrix4x4 m2 = transform.localToWorldMatrix;
		Matrix4x4 m3 = m2 * m1;
		live2DModel.setMatrix( m3 );		

		//頂点の更新
		live2DModel.update();

		//モデルの描画
		live2DModel.draw();
	}


	//「コルーチン」で呼び出すメソッド
	void ChangeModel(){
		dataManager = DataManager.Instance;
		//dataManager.LoadStyleSaveData ();

		/*
		if(dataManager.test == 1){
			dataManager.nowStyle = 7;
		}else{
			dataManager.nowStyle = -7;
		}*/

		int style = dataManager.nowStyle;
		Debug.Log ("食事style = " + style);
		//初期化
		Live2D.init();

		if (dataManager.gender == "女") {
			if(style >= 6){
				//大デブ
				live2DModel = Live2DModelUnity.loadModel (mocFile_girl_fat2.bytes);
				for (int i = 0; i < texture_girl_fat2.Length; i++) {
					live2DModel.setTexture (i, texture_girl_fat2 [i]);
				}
			}else if(style >= 3){
				//デブ
				live2DModel = Live2DModelUnity.loadModel (mocFile_girl_fat1.bytes);
				for (int i = 0; i < texture_girl_fat1.Length; i++) {
					live2DModel.setTexture (i, texture_girl_fat1 [i]);
				}

			}else if(style >= -2){
				//通常
				live2DModel = Live2DModelUnity.loadModel (mocFile_girl_normal.bytes);
				for (int i = 0; i < texture_girl_normal.Length; i++) {
					live2DModel.setTexture (i, texture_girl_normal [i]);
				}
			}else if(style >= -5){
				//もやアニメーション
				Instantiate(moyamoya, new Vector3(80.6f, -2f, 0), Quaternion.identity);
				//通常
				live2DModel = Live2DModelUnity.loadModel (mocFile_girl_normal.bytes);
				for (int i = 0; i < texture_girl_normal.Length; i++) {
					live2DModel.setTexture (i, texture_girl_normal [i]);
				}
			}else{
				//ガリ
				live2DModel = Live2DModelUnity.loadModel (mocFile_girl_gari.bytes);
				for (int i = 0; i < texture_girl_gari.Length; i++) {
					live2DModel.setTexture (i, texture_girl_gari [i]);
				}
			}
		}else{
			if(style >= 6){
				//大デブ
				live2DModel = Live2DModelUnity.loadModel ( mocFile_bigDebu.bytes );
				live2DModel.setTexture( 0, texture_bigDebu );

			}else if(style >= 4){
				//汗のアニメーション
				Instantiate(asease, new Vector3(-0.3f, 0.5f, 0), Quaternion.identity);
				//デブ
				live2DModel = Live2DModelUnity.loadModel ( mocFile_debu.bytes );
				live2DModel.setTexture( 0, texture_debu );
			}else if(style == 3){
				//デブ
				live2DModel = Live2DModelUnity.loadModel ( mocFile_debu.bytes );
				live2DModel.setTexture( 0, texture_debu );

			}else if(style >= 1){
				//汗のアニメーション
				Instantiate(asease, new Vector3(-0.3f, 0.5f, 0), Quaternion.identity);
				//通常
				live2DModel = Live2DModelUnity.loadModel ( mocFile.bytes );
				for (int i = 0; i < textures.Length; i++)
				{
					live2DModel.setTexture(i, textures[i]);
				}

			}else if(style >= -2){
				//通常
				live2DModel = Live2DModelUnity.loadModel ( mocFile.bytes );
				for (int i = 0; i < textures.Length; i++)
				{
					live2DModel.setTexture(i, textures[i]);
				}
			}else if(style >= -5){
				//もやアニメーション
				Instantiate(moyamoya, new Vector3(0f, 0f, 0), Quaternion.identity);
				//通常
				live2DModel = Live2DModelUnity.loadModel ( mocFile.bytes );
				for (int i = 0; i < textures.Length; i++)
				{
					live2DModel.setTexture(i, textures[i]);
				}
			}else{
				//ガリ
				live2DModel = Live2DModelUnity.loadModel ( mocFile_gari.bytes );
				live2DModel.setTexture( 0, texture_gari );
			}
		}


	}
		
}