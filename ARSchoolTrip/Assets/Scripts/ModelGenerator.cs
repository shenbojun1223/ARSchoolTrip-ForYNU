using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public class ModelGenerator : MonoBehaviour
{

    public Camera FirstPersonCamera;
    public GameObject DetectedPlanePrefab;
    public GameObject HoloScene;
    public GameObject FullScene;
    public GameObject PortalScene;
    public GameObject CurrentModel;
    public Button NextButton;
    public Button NextButton1;
    public Button NextSceneButton;
    private const float k_ModelRotation = 180.0f;
    private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
    public Material gMat;

    public void OnModelChanged(int index)
    {
        Destroy(GameObject.Find("Anchor"));
        if (index == 1)
        {
            CurrentModel = FullScene;
            NextButton.gameObject.SetActive(false);
            GameObject.Find("ARCoreManager").GetComponent<ARCoreManager>().SendMessage("ShowMessage", 5);
        }
        if (index == 2)
        {
            CurrentModel = PortalScene;
            NextButton1.gameObject.SetActive(false);
            GameObject.Find("ARCoreManager").GetComponent<ARCoreManager>().SendMessage("ShowMessage", 7);
        }
    }

    void Start()
    {
        CurrentModel = HoloScene;
    }

    void Update()
    {
        Session.GetTrackables<DetectedPlane>(m_AllPlanes);
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // 检测用户点击的平面
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            Debug.Log(hit.Pose.position);
            //判断是否需要生成描点
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                if (!GameObject.Find("Anchor"))
                    
                
                {

                    
                    //将模型进行实例化
                    var andyObject = Instantiate(CurrentModel, hit.Pose.position, hit.Pose.rotation);
                

                    // 设置模型的Rotation
                    andyObject.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

                    //创建一个锚点进行追踪
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                    // 将模型作为锚点的子对象
                    andyObject.transform.parent = anchor.transform;

                    if(CurrentModel==HoloScene)
                        GameObject.Find("ARCoreManager").GetComponent<ARCoreManager>().SendMessage("ShowMessage", 4);
                    if(CurrentModel==FullScene)
                        GameObject.Find("ARCoreManager").GetComponent<ARCoreManager>().SendMessage("ShowMessage", 6);
                    if (CurrentModel == PortalScene)
                        GameObject.Find("ARCoreManager").GetComponent<ARCoreManager>().SendMessage("ShowMessage", 8);
                }

            }
        }
    }
}
