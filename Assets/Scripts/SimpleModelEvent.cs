using UnityEngine;
using System;
using System.Collections;
using live2d;
using live2d.framework;

//[ExecuteInEditMode]
public class SimpleModelEvent : MonoBehaviour{

	DataManager dataManager;

    public TextAsset mocFile;
    public TextAsset physicsFile;
	public TextAsset mtnFile;
    public Texture2D[] textureFiles;

	public TextAsset mocFile_bigDebu; // bigDebu_mocファイル
	public Texture2D texture_bigDebu;

	public TextAsset mocFile_girl_normal; // girl_normalファイル
	public Texture2D[] texture_girl_normal;

    private Live2DModelUnity live2DModel;
	private Live2DMotion motion;
	private MotionQueueManager motionMgr;
    private EyeBlinkMotion eyeBlink = new EyeBlinkMotion();
    private L2DTargetPoint dragMgr = new L2DTargetPoint();
    private L2DPhysics physics;
    private Matrix4x4 live2DCanvasPos;

	float mouth;
	bool mouth_on;
	public bool finSpeak;

    void Start()
    {
		//dataManager = DataManager.Instance;
		//dataManager.StyleSaveData ();
		//dataManager.LoadStyleSaveData ();
        Live2D.init();
	
        load();
    }


    void load()
    {
		dataManager = DataManager.Instance;

		//int _eventStyle = dataManager.eventStyle;
		int _eventStyle = 0;
		Debug.Log (_eventStyle);
		if (dataManager.gender == "女") {
			if(_eventStyle == 1){
				//大デブ
				//live2DModel = Live2DModelUnity.loadModel ( mocFile_bigDebu.bytes );
				//live2DModel.setTexture( 0, texture_bigDebu );
			}else{
				//通常
				live2DModel = Live2DModelUnity.loadModel ( mocFile_girl_normal.bytes );
				for (int i = 0; i < texture_girl_normal.Length; i++)
				{
					live2DModel.setTexture(i, texture_girl_normal[i]);
				}
			}
		}else{
			if(_eventStyle == 1){
				//大デブ
				live2DModel = Live2DModelUnity.loadModel ( mocFile_bigDebu.bytes );
				live2DModel.setTexture( 0, texture_bigDebu );
			}else{
				//通常
				live2DModel = Live2DModelUnity.loadModel ( mocFile.bytes );
				for (int i = 0; i < textureFiles.Length; i++)
				{
					live2DModel.setTexture(i, textureFiles[i]);
				}
			}
		}


        float modelWidth = live2DModel.getCanvasWidth();
        live2DCanvasPos = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);

        if (physicsFile != null) physics = L2DPhysics.load(physicsFile.bytes);

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

		double timeSec = UtSystem.getUserTimeMSec() / 1000.0;
		double t = timeSec * 2 * Math.PI;
		live2DModel.setParamFloat("PARAM_BREATH", (float)(0.5f + 0.5f * Math.Sin(t / 3.0)));

		eyeBlink.setParam(live2DModel);

		//テキストが表示完了していなかったら口をパクパクさせる
		if(finSpeak != true){
			if (mouth_on == true){
				if(mouth >= 1.0f){
					mouth_on = false;
				}
				mouth += 0.2f; 
			}else{
				if(mouth <= 0.1f){
					mouth_on = true;
				}
				mouth += -0.2f; 

			}
		}else{
			mouth = 0f; 
		}

		live2DModel.setParamFloat("PARAM_MOUTH_OPEN_Y", mouth);


		if (physics != null) physics.updateParam(live2DModel);

		live2DModel.update();
	}


    void OnRenderObject()
    {
        if (live2DModel == null) load();
        if (live2DModel.getRenderMode() == Live2D.L2D_RENDER_DRAW_MESH_NOW) live2DModel.draw();

    }

		
}