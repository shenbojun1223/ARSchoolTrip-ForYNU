using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//这个脚本使得用户能旋转和缩放建筑物
public class BuildingController : MonoBehaviour
{
    float xSpeed = 150f;
    bool hasMovedout=false;
    public GameObject bubble;
    public Camera ARCamera;
    public bool focusIntroduce;
    Vector2 oldPos1;
    Vector2 oldPos2;
    public bool isSizeSelfAdaption=false;
    private bool hasShowedBubble;
    // Use this for initialization
    void Awake() {
        ARCamera=GameObject.Find("ARCamera").GetComponent<Camera>();
    }
    public bool HasMovedout
    {
        set{hasMovedout=value;}
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //缩放模型
            if(!focusIntroduce)
            {

                //在editor中运行
                #if UNITY_EDITOR &&UNITY_IOS
                // Debug.Log(transform.up);
                transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * -xSpeed * Time.deltaTime,Space.Self);
                
                //在unity中运行
                #elif UNITY_ANDROID && UNITY_
                if(Input.touchCount==1)
                {
                    if(Input.GetTouch(0).phase==TouchPhase.Moved){
                        transform.Rotate(Vector3.up*Input.GetAxis("Mouse X")*-xSpeed*Time.deltaTime,Space.Self);
                        // transform.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * -xSpeed * Time.deltaTime);
                    }
                }
                #endif
            }
            if (Input.touchCount == 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    Vector2 temPos1 = Input.GetTouch(0).position;
                    Vector2 temPos2 = Input.GetTouch(1).position;

                    if (isEnLarge(oldPos1, oldPos2, temPos1, temPos2))
                    {
                        float oldScale = transform.localScale.x;
                        float newScale = oldScale * 1.050f;
                        transform.localScale = new Vector3(newScale, newScale, newScale);
                    }
                    else
                    {
                        float oldScale = transform.localScale.x;
                        float newScale = oldScale / 1.050f;
                        transform.localScale = new Vector3(newScale, newScale, newScale);
                    }
                    oldPos1 = temPos1;
                    oldPos2 = temPos2;
                }
            }
        }
        if(hasShowedBubble)
        {
            return;
        }
            //只显示一次操作提示
        if(Input.GetMouseButtonDown(0) &&bubble.activeSelf==false)
        {
            hasShowedBubble=true;
            #if UNITY_EDITOR
            Ray ray=ARCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                
                if(hit.collider.gameObject.transform.parent.gameObject.tag=="MiniBuilding")
                {
                    bubble.SetActive(true);
                    Invoke("CloseBubble",5f);
                }
            }
            #elif UNITY_ANDROID &&UNITY_IOS
            Ray ray=ARCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.gameObject.transform.parent.gameObject.tag=="MiniBuilding")
                {
                    bubble.SetActive(true);
                    Invoke("CloseBubble",5f);
                }
            }
            #endif
        }
        


    }


    void CloseBubble()
    {
        bubble.SetActive(false);
    }
    bool isEnLarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        float length1 = Mathf.Sqrt(Mathf.Pow((oP1.x - oP2.x), 2.0f) + Mathf.Pow((oP1.y - oP2.y), 2.0f));
        float length2 = Mathf.Sqrt(Mathf.Pow((nP1.x - nP2.x), 2.0f) + Mathf.Pow((nP1.y - nP2.y), 2.0f));

        if (length1 < length2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // //自动调整模型的大小，随着屏幕的改变
    // void SizeSelfAdaption()
    // {
    //     Vector3 size =transform.GetChild(0).gameObject.GetComponent<BoxCollider>().size;
    //     if (size.x * transform.GetChild(0).localScale.x != transform.parent.parent.gameObject.GetComponent<RectTransform>().sizeDelta.x * 0.45)
    //     {
    //        transform.GetChild(0).gameObject.GetComponent<Scaling>().factor=(int) (transform.parent.parent.gameObject.GetComponent<RectTransform>().sizeDelta.x * 0.45/size.x);
    //     }
    // }
}
