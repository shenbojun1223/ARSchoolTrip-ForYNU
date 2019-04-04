//-----------------------------------------------------------------------
// <copyright file="AugmentedImageExampleController.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------


using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using UnityEngine;
using UnityEngine.UI;


public class AugmentedImageExampleController : MonoBehaviour
{

    public AugmentedImageVisualizer AugmentedImageVisualizerPrefab;

    public GameObject FitToScanOverlay;

    public GameObject HoloEffect;

    public GameObject ARCoreManager;

    public Text StatusInfo;

    private Dictionary<int, AugmentedImageVisualizer> m_Visualizers
        = new Dictionary<int, AugmentedImageVisualizer>();

    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

    public bool IsTrackingOver;

    /// <summary>
    /// 
    /// 

    #region Methods

    public void StopTracking()
    {
        if (Session.Status != SessionStatus.NotTracking)
        {
            
            AugmentedImageVisualizerPrefab.gameObject.SetActive(false);
            Destroy(GameObject.Find("Anchor"));
            IsTrackingOver = true;
            ARCoreManager.GetComponent<ARCoreManager>().SendMessage("setImageActive", IsTrackingOver);
            ARCoreManager.GetComponent<ARCoreManager>().SendMessage("ShowMessage", 2);
            ARCoreManager.GetComponent<ARCoreManager>().SendMessage("setPanelActive", true);
        }
            

    }

    #endregion

    public void Start()
    {
        AugmentedImageVisualizerPrefab.gameObject.SetActive(false);
        IsTrackingOver = false;
        ARCoreManager.GetComponent<ARCoreManager>().SendMessage("ShowMessage", 1);
    }

    public void Update()
    {


        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        if (!IsTrackingOver)
        {//获取正在追踪的图像
            Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);
            //为正在追踪的图像创建一个可视化对象
            foreach (var image in m_TempAugmentedImages)
            {
                AugmentedImageVisualizer visualizer = null;
                m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);
                if (image.TrackingState == TrackingState.Tracking && visualizer == null)
                {
                    AugmentedImageVisualizerPrefab.gameObject.SetActive(true);
                    
                    //创建一个锚点以便能够实时追踪
                    Anchor anchor = image.CreateAnchor(image.CenterPose);
                    visualizer = (AugmentedImageVisualizer)Instantiate(AugmentedImageVisualizerPrefab, anchor.transform);
                    if (image.DatabaseIndex == 0)
                        StatusInfo.text = "Yunnan University";
                    visualizer.Image = image;
                    m_Visualizers.Add(image.DatabaseIndex, visualizer);
                }
                else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
                {

                    m_Visualizers.Remove(image.DatabaseIndex);
                    GameObject.Destroy(visualizer.gameObject);
                }



            }

            
            foreach (var visualizer in m_Visualizers.Values)
            {
                if (visualizer.Image.TrackingState == TrackingState.Tracking)
                {
                    FitToScanOverlay.SetActive(false);
                    return;
                }

            }

            FitToScanOverlay.SetActive(true);

        }
    }
}

