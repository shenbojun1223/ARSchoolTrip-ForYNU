using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class _AppManager : MonoBehaviour {
	public static _AppManager instance=null;
	public _UIManager UIManager;
	public float setupTime=0f;
	public GameObject ImageTarget;
	public Camera ARCamera;
	public GameObject AppStartImage;
	public int TargetHeight=2048;
	public int TargetWidth=1536;
	// public GameObject buildingShow;
	private _MapManager mapManager;
	private _DisplayHelper displayHelper;
	[SerializeField]
	private bool starting=true;

	void Awake () {
		if(instance==null)
			instance=this;
		else if(instance!=this)
			Destroy(this.gameObject);
		DontDestroyOnLoad(this.gameObject);
		// ImageTarget=GameObject.Find("ImageTarget");
		// ImageTarget.SetActive(false);
		mapManager=GetComponent<_MapManager>();
		displayHelper=GetComponent<_DisplayHelper>();

		Application.targetFrameRate=30;
		// Screen.SetResolution(540,1110,false);

		InitApp();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

	void InitApp()
	{

		starting=true;
		// AppStartImage.SetActive(true);
		Invoke("HideStartImage",setupTime);
	}
	public void SetupScene()
	{
		// mapManager.SetupScene();
		
	}

	void HideStartImage(){
		// AppStartImage.SetActive(false);
		starting=false;
		// ImageTarget.GetComponent<MyDefaultTrackableEventHandler>().AppHasInitialized=!starting;
		ARCamera.gameObject.GetComponent<VuforiaBehaviour>().enabled=true;
		


		// ImageTarget.SetActive(true);
	}
}
