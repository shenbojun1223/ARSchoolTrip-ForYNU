using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.HelloAR;
using GoogleARCore.Examples.Common;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlaneVisualizationManager : MonoBehaviour
{
    //检测完成平面预制体
    public GameObject TrackedPlanePrefab;
    public Text StatusInfo;
    public GameObject ARCoreManager;
    public bool IsSending=false;

    private const float k_ModelRotation = 180.0f;
    //用于存储平面信息
    private List<DetectedPlane> _newPlanes = new List<DetectedPlane>();

    public void TestLoad()
    {
        SceneManager.LoadScene("BDTest");
    }

    void Update()
    {
        Session.GetTrackables<DetectedPlane>(_newPlanes, TrackableQueryFilter.New);


        for (int i = 0; i < _newPlanes.Count; i++)
        {
            //实例化可视化平面以及将其信息保存
            GameObject planeObject = Instantiate(TrackedPlanePrefab, Vector3.zero, Quaternion.identity, transform);
            planeObject.GetComponent<DetectedPlaneVisualizer>().Initialize(_newPlanes[i]);
        }

        if (GameObject.Find("DetectedPlaneVisualizer(Clone)")&&IsSending==false)
        {
            IsSending = true;
            ARCoreManager.GetComponent<ARCoreManager>().SendMessage("ShowMessage", 3);
        }
            


    }

}
