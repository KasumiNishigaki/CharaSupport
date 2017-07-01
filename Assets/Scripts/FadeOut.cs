using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FadeOut : MonoBehaviour {

	public Dictionary<string, int> stampDic2;

	// Use this for initialization
	void Start () {
		
		Invoke("FadeTween",1);
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetMouseButtonDown(0)){
			if(dateManager.op_ == "false"){
				Application.LoadLevel ("OP");
			}else{
				Application.LoadLevel ("Title");
			}
		}*/
	}
		
	void FadeTween(){
		iTween.FadeTo(gameObject, iTween.Hash(
			"alpha", 0, 
			"time", 2.0f ,
			"oncomplete","EndAction",
			"oncompletetarget",gameObject));
	}

	void EndAction(){
		Application.LoadLevel ("Title");
	}

}
