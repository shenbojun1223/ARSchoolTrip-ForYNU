using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//这个脚本用于使按钮看向摄像机
public class BuildingBtn : MonoBehaviour {

	public Camera ARCamera;
	public Text buildingName;

	public string BuildingName
	{
		set{buildingName.text=value;}
		get{return buildingName.text;}
	}
	void Awake() {
		ARCamera=GameObject.Find("ARCamera").GetComponent<Camera>();
		// GetComponent<Button>().onClick.AddListener(()=>{

		// });
	}
	void Update () {
		//建筑物的按钮看向摄像机
		transform.LookAt(ARCamera.transform.position);
	}


}
