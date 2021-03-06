using UnityEngine;
using System;
using System.Collections;
using live2d;
using live2d.framework;

//[ExecuteInEditMode]
public class SimpleModel : MonoBehaviour{

	DataManager dataManager;
	AudioPlayer audioPlayer;

    public TextAsset mocFile;
    public TextAsset physicsFile;
	public TextAsset mtnFile;
    public Texture2D[] textureFiles;

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

	private Animator anim;    // Animator
	private MotionBehaviour mtnBehaviour;    // MotionBehaviour

    private Live2DModelUnity live2DModel;
	private Live2DMotion motion;
	private MotionQueueManager motionMgr;
    private EyeBlinkMotion eyeBlink = new EyeBlinkMotion();
    private L2DTargetPoint dragMgr = new L2DTargetPoint();
    private L2DPhysics physics;
    private Matrix4x4 live2DCanvasPos;

	private float scaleVolume = 5.5f;
	private bool smoothing = true;
	private bool useMic = false;
	private float lastVolume = 0;

	private int count;

	public GameObject moyamoya;

	float mouth;
	bool mouth_on;
	public bool finSpeak;

    void Start()
    {
        Live2D.init();
		// Animatorを取得
		anim = GetComponent<Animator>();
		// State Machine Behavioursを取得
		mtnBehaviour = anim.GetBehaviour<MotionBehaviour>();
		//print (mtnBehaviour);
        load();
    }


    void load()
    {
		dataManager = DataManager.Instance;
		Debug.Log ("なうスタイル = " + dataManager.nowStyle);
		//dataManager.LoadStyleSaveData ();
		//dataManager.nowStyle = -3;
		int style = dataManager.nowStyle;
		Debug.Log (style);

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

			}else if(style >= 3){
				//デブ
				live2DModel = Live2DModelUnity.loadModel ( mocFile_debu.bytes );
				live2DModel.setTexture( 0, texture_debu );

			}else if(style >= -2){
				//通常
				live2DModel = Live2DModelUnity.loadModel ( mocFile.bytes );
				for (int i = 0; i < textureFiles.Length; i++)
				{
					live2DModel.setTexture(i, textureFiles[i]);
				}
			}else if(style >= -5){
				//もやアニメーション
				Instantiate(moyamoya, new Vector3(80.6f, -2f, 0), Quaternion.identity);
				//通常
				live2DModel = Live2DModelUnity.loadModel ( mocFile.bytes );
				for (int i = 0; i < textureFiles.Length; i++)
				{
					live2DModel.setTexture(i, textureFiles[i]);
				}
			}else{
				//ガリ
				live2DModel = Live2DModelUnity.loadModel ( mocFile_gari.bytes );

				live2DModel.setTexture( 0, texture_gari );
			}
		}


			
        float modelWidth = live2DModel.getCanvasWidth();
        live2DCanvasPos = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);

        if (physicsFile != null) physics = L2DPhysics.load(physicsFile.bytes);

		motionMgr = new MotionQueueManager();
		//motion = Live2DMotion.loadMotion(mtnFile.bytes);
    }


    void Update()
    {

        if (live2DModel == null) load();
        live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);
        if (!Application.isPlaying)
        {
            live2DModel.update();
            return;
        }

        var pos = Input.mousePosition;

		Vector2 worldPoint = Camera.main.ScreenToWorldPoint(pos);
		RaycastHit2D hit = Physics2D.Raycast(worldPoint,Vector2.zero);

        if (Input.GetMouseButtonDown(0))
		{	
			/*count++;
			//anim.SetInteger ("Motion", 1);
			if(count == 1){
				anim.SetTrigger("Mtn_happy");
			}else if(count == 2){
				anim.SetTrigger("Mtn_angry");
			}else if(count == 3){
			anim.SetTrigger("Mtn_sad");
			}else{
				anim.SetTrigger("Mtn_smile");
			}*/
			if(hit.collider != null ){
				//PlayVoice_ ();
			}


        }
        else if (Input.GetMouseButton(0))
        {
	
			//if(hit.collider != null ){
				dragMgr.Set(pos.x / Screen.width * 2 - 1, pos.y / Screen.height * 2 - 1);
			//}
				

        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragMgr.Set(0, 0);
        }
	

        dragMgr.update();
        live2DModel.setParamFloat("PARAM_ANGLE_X", dragMgr.getX() * 30);
        live2DModel.setParamFloat("PARAM_ANGLE_Y", dragMgr.getY() * 30);

        live2DModel.setParamFloat("PARAM_BODY_ANGLE_X", dragMgr.getX() * 10);


        live2DModel.setParamFloat("PARAM_EYE_BALL_X", -dragMgr.getX());
        live2DModel.setParamFloat("PARAM_EYE_BALL_Y", -dragMgr.getY());


		 // ドラッグによる目の向きの調整
		live2DModel.addToParamFloat("PARAM_EYE_BALL_X", dragMgr.getX(), 1);// -1から1の値を加える
		live2DModel.addToParamFloat("PARAM_EYE_BALL_X", dragMgr.getY(), 1);


        double timeSec = UtSystem.getUserTimeMSec() / 1000.0;
        double t = timeSec * 2 * Math.PI;
        live2DModel.setParamFloat("PARAM_BREATH", (float)(0.5f + 0.5f * Math.Sin(t / 3.0)));

        eyeBlink.setParam(live2DModel);

		float volume = 0;
		float currentVolume = GetCurrentVolume(GetComponent<AudioSource>());

		if (Mathf.Abs(lastVolume - currentVolume) < 0.2f)
		{
			volume = lastVolume * 0.9f + currentVolume * 0.1f;
		}
		else if (lastVolume - currentVolume > 0.2f)
		{
			volume = lastVolume * 0.7f + currentVolume * 0.3f;
		}
		else
		{
			volume = lastVolume * 0.2f + currentVolume * 0.8f;
		}
		lastVolume = volume;

		live2DModel.setParamFloat("PARAM_MOUTH_OPEN_Y", volume * scaleVolume);

        if (physics != null) physics.updateParam(live2DModel);

        live2DModel.update();
    }


    void OnRenderObject()
    {
        if (live2DModel == null) load();
        if (live2DModel.getRenderMode() == Live2D.L2D_RENDER_DRAW_MESH_NOW) live2DModel.draw();

		/*
		// モーションが終了していたら or フラグが更新されていたら
		if (motionMgr.isFinished() || mtnBehaviour.changeflg == true)
		{
			// モーションのロード
			//motion = Live2DMotion.loadMotion(mtnFile.bytes);
			// モーションスタート
			//motionMgr.startMotion(motion);
			// フラグをOFFにする
			mtnBehaviour.changeflg = false;
		}*/

		//motionMgr.updateParam(live2DModel);
    }



	private float GetCurrentVolume(AudioSource audio)
	{
		float[] data = new float[256];
		float sum = 0;
		audio.GetOutputData(data, 0);
		foreach (float s in data)
		{
			sum += Mathf.Abs(s);
		}
		return sum / 256.0f;
	}
		

	public void PlayVoice_(){

		if (GetComponent<AudioSource>().isPlaying)
		{
			if (useMic)
			{
				var deviceName = Microphone.devices[0];
				Debug.Log (deviceName);
				Microphone.End(deviceName);
			}
			GetComponent<AudioSource>().Stop();
		}
		GetComponent<AudioSource>().Play();


	}


	public bool SetupMic(AudioSource audio)
	{
		const int RECORDING_SEC = 20;
		const int FREQENCY = 44100;

		if (Microphone.devices.Length == 0)
		{
			Debug.Log("Mic is not found.");
			return false;
		}

		var deviceName = Microphone.devices[0];

		audio.clip = Microphone.Start(deviceName, false, RECORDING_SEC, FREQENCY);

		return true;
	}
		
}