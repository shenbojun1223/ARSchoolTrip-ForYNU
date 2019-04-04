using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vuforia
{
    public class Camer : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            var vuforia = VuforiaARController.Instance;
            vuforia.RegisterBackgroundTextureChangedCallback(OnVuforiaStarted);
            // vuforia.RegisterOnPauseCallback(OnPaused);
            // foreach(var component in GetComponents<VuforiaBehaviour>())
            // {
            //     Debug.Log(component.ToString());
            // }
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnVuforiaStarted()
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }

        private void OnPaused(bool isPaused)
        {


        }

        public void OnFocusModeClick() {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }
}
