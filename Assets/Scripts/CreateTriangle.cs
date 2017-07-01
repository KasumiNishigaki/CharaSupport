using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateTriangle : MonoBehaviour 
{	
	DataManager dataManager;

	private GameObject triangleObject;

	public Slider slider; //ごはん
	public Slider slider2; //肉
	public Slider slider3; //野菜

	public Image rice;
	public Image meat;
	public Image vegetable;

	private RectTransform hoge;

	private float level;
	private float level_2;
	private float level_3;

	Vector3[] vertices;
	Vector3[] _defaultVertices;


	int[] triangles;

	public Material _mat;

	void Start () 
	{	dataManager = DataManager.Instance;
		
		if(dataManager.eatCount != 3){
			vertices = new Vector3[]
			{
				new Vector3(0f, 3.4f, 0f),//上
				new Vector3(1.15f, 1.04f, 0f),//右
				new Vector3(-1.15f, 1.04f, 0f)//左
			};

			_defaultVertices = vertices;

			triangles = new int[] { 0, 1, 2 };

			triangleObject = new GameObject();
			triangleObject.name = "triangleObject";
			triangleObject.AddComponent<MeshFilter>();
			triangleObject.AddComponent<MeshRenderer>();

			slider.value = 3;
			slider2.value = 3;
			slider3.value = 4;

			level = slider.value;
			level_2 = slider2.value;
			level_3 = slider3.value;


			MeshFilter meshFilter = triangleObject.GetComponent<MeshFilter>();
			Mesh mesh = new Mesh();

			mesh.vertices = vertices;
			mesh.triangles = triangles;
			meshFilter.mesh = mesh;

			triangleObject.layer = LayerMask.NameToLayer("Triangle");

			// 変更箇所 : MeshRendererからMaterialにアクセスし、Materialをセットするようにする
			var renderer = triangleObject.GetComponent<MeshRenderer> ();
			renderer.material = _mat;
		}

	}

	void Update ()
	{	
		if (dataManager.eatCount != 3) {
			if (slider.value != level) {
				//ごはん
				level = slider.value;

				MeshFilter meshFilter = triangleObject.GetComponent<MeshFilter> ();
				Mesh mesh = new Mesh ();

				if (slider.value == 1) {
					dataManager.rice_balance = 1;
					//vertices [2].x = _defaultVertices [2].x + 1;
					vertices [2] = new Vector3 (-0.3f, 1.495f, 0f);
					hoge = rice.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (110, 90);

				} else if (slider.value == 2) {
					dataManager.rice_balance = 2;
					vertices [2] = new Vector3 (-0.67f, 1.25f, 0f);
					hoge = rice.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (150, 120);

				} else if (slider.value == 3) {
					dataManager.rice_balance = 3;
					vertices [2] = new Vector3 (-1.15f, 1.04f, 0f);
					hoge = rice.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (200, 170);

				} else if (slider.value == 4) {
					dataManager.rice_balance = 4;
					vertices [2] = new Vector3 (-1.55f, 0.8f, 0f);
					hoge = rice.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (240, 210);

				} else if (slider.value == 5) {
					dataManager.rice_balance = 5;
					vertices [2] = new Vector3 (-2.05f, 0.5f, 0f);
					hoge = rice.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (290, 260);
				}
				
				mesh.vertices = vertices;
				mesh.triangles = triangles;
				meshFilter.mesh = mesh;
			}


			if (slider2.value != level_2) {
				//肉
				level_2 = slider2.value;

				MeshFilter meshFilter = triangleObject.GetComponent<MeshFilter> ();
				Mesh mesh = new Mesh ();

				if (slider2.value == 1) {
					dataManager.meat_balance = 1;
					//vertices [2].x = _defaultVertices [2].x + 1;
					vertices [1] = new Vector3 (0.3f, 1.495f, 0f);
					hoge = meat.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (130, 100);

				} else if (slider2.value == 2) {
					dataManager.meat_balance = 2;
					vertices [1] = new Vector3 (0.67f, 1.25f, 0f);
					hoge = meat.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (160, 130);

				} else if (slider2.value == 3) {
					dataManager.meat_balance = 3;
					vertices [1] = new Vector3 (1.15f, 1.04f, 0f);
					hoge = meat.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (200, 160);

				} else if (slider2.value == 4) {
					dataManager.meat_balance = 4;
					vertices [1] = new Vector3 (1.55f, 0.8f, 0f);
					hoge = meat.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (250, 200);

				} else if (slider2.value == 5) {
					dataManager.meat_balance = 5;
					vertices [1] = new Vector3 (2.05f, 0.5f, 0f);
					hoge = meat.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (290, 240);

				}

				mesh.vertices = vertices;
				mesh.triangles = triangles;
				meshFilter.mesh = mesh;
			}

			if (slider3.value != level_3) {
				//野菜
				level_3 = slider3.value;

				MeshFilter meshFilter = triangleObject.GetComponent<MeshFilter> ();
				Mesh mesh = new Mesh ();

				if (slider3.value == 1) {
					dataManager.vegetable_balance = 1;

					//vertices [2].x = _defaultVertices [2].x + 1;
					//vertices [0].y = 0.5f;
					vertices [0] = new Vector3 (0f, 1.9f, 0f);
					hoge = vegetable.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (200, 100);

				} else if (slider3.value == 2) {
					dataManager.vegetable_balance = 2;
					vertices [0] = new Vector3 (0f, 2.4f, 0f);
					hoge = vegetable.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (240, 140);

				} else if (slider3.value == 3) {
					dataManager.vegetable_balance = 3;
					vertices [0] = new Vector3 (0f, 3f, 0f);
					hoge = vegetable.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (280, 180);

				} else if (slider3.value == 4) {
					dataManager.vegetable_balance = 4;
					vertices [0] = new Vector3 (0f, 3.4f, 0f);
					hoge = vegetable.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (310, 220);

				} else if (slider3.value == 5) {
					dataManager.vegetable_balance = 5;
					vertices [0] = new Vector3 (0f, 4.05f, 0f);
					hoge = vegetable.GetComponent<RectTransform> ();
					hoge.sizeDelta = new Vector2 (360, 270);

				}

				mesh.vertices = vertices;
				mesh.triangles = triangles;
				meshFilter.mesh = mesh;
			}

		}
	}
}