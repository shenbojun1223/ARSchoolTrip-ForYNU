using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraSetting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var vuforia=VuforiaARController.Instance;
		vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
		// vuforia.RegisterOnPauseCallback(onPaused);
	}

	private void OnVuforiaStarted()
	{
		CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
	}

	private void onPaused(bool isPaused){

	}

	public void OnFocusModeClick(){
		CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void SwitchCameraDirection(CameraDevice.CameraDirection dir)
	{
		CameraDevice.Instance.Stop();
		CameraDevice.Instance.Deinit();

		CameraDevice.Instance.Init(dir);
		CameraDevice.Instance.Start();
	}

	public void FalshTourch(bool ON){
		CameraDevice.Instance.SetFlashTorchMode(ON);
	}
}
