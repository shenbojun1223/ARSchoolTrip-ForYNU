

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using GoogleARCoreInternal;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class AugmentedImageVisualizer : MonoBehaviour
{
    /// <summary>
    /// The AugmentedImage to visualize.
    /// </summary>
    public AugmentedImage Image;
    public GameObject HoloEffect;
    public GameObject ImageManager;
    public Text StatusInfo;

    /// <summary>
    /// A model for the lower left corner of the frame to place when an image is detected.
    /// </summary>
    public GameObject FrameLowerLeft;

    private float Playtime = 0;
    private float OverTime = 0;
    private bool state = false;
    private float AhlpaValue = 1;

    public bool isOver;
    /// <summary>
    /// The Unity Update method.
    /// </summary>
    /// 

    public void Start()
    {
        HoloEffect.GetComponent<ParticleSystem>().Play();
    }

    public void Update()
    {
        float halfWidth = Image.ExtentX / 3;
        float halfHeight = Image.ExtentZ / 2;

        if (Image == null || Image.TrackingState != TrackingState.Tracking)
        {
            FrameLowerLeft.SetActive(false);

            return;
        }
        if (HoloEffect.GetComponent<ParticleSystem>().isPlaying)
        {

            Playtime += Time.deltaTime;
        }
        if (Playtime >= 3 && Playtime <= 4)
        {

            FrameLowerLeft.GetComponent<VideoPlayer>().Play();
            isOver = true;
            FrameLowerLeft.SetActive(true);
            FrameLowerLeft.transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.back);

        }
        if (!FrameLowerLeft.GetComponent<VideoPlayer>().isPlaying)
        {

            if (!HoloEffect.GetComponent<ParticleSystem>().isPlaying&&isOver==true)
            {
                Debug.Log("Stop");
               GameObject.Find("ImageManager").GetComponent<AugmentedImageExampleController>().SendMessage("StopTracking");
                isOver = false;
            }
        }
    }



}


