using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro.Examples;

public class ARCoreManager : MonoBehaviour
{

    #region Prefabs
    public GameObject ImageManager;
    public GameObject PanelMananger;
    public GameObject Message1;
    private float UI_Alpha = 1;
    public float alphaSpeed = 1f;
    private CanvasGroup canvasgroup;
    public GameObject StatusInfo;
    public Button NextButton;
    public Button NextButton1;
    public Button NextSceneButton;
    //public static bool Loaded=false;
    public bool isEnteringFull;
    #endregion

    #region Variables
    public float MessageShowTime = 5;
    #endregion

    #region Methods
    public void setImageActive(bool isTrackingOver)
    {
        if (isTrackingOver == false)
            ImageManager.SetActive(true);
        else
            ImageManager.SetActive(false);
    }

    public void setPanelActive(bool Active)
    {
        if (Active == true)
            PanelMananger.SetActive(true);
        else
            PanelMananger.SetActive(false);
    }


    public void UIUpdate()
    {
        if (canvasgroup == null)
        {
            return;
        }

        if (UI_Alpha != canvasgroup.alpha)
        {
            canvasgroup.alpha = Mathf.Lerp(canvasgroup.alpha, UI_Alpha, alphaSpeed * Time.deltaTime);
            if (Mathf.Abs(UI_Alpha - canvasgroup.alpha) <= 0.01f)
            {
                canvasgroup.alpha = UI_Alpha;
            }
        }
    }

    public void UI_FadeIn_Event()
    {
        UI_Alpha = 1;
        canvasgroup.blocksRaycasts = true;      //可以和该对象交互
    }
    public void UI_FadeOut_Event()
    {
        Debug.Log("FadeOut");
        UI_Alpha = 0;
        canvasgroup.blocksRaycasts = false;     //不可以和该对象交互
    }


    /* public IEnumerator StartCounter(float time)
     {
         Debug.Log(time);
         time--;
         if (time == 0)
         {
             Message1.SetActive(false);
             yield break;
         }

         else if (time > 0)
             yield return new WaitForSeconds(1);
     }*/

    public void ShowMessage(int index)
    {
        switch (index)
        {
            case 1:
                //Message1.SetActive(true);
                Message1.GetComponentInChildren<Text>().text =
                    "请扫描一个高校校徽";
                StatusInfo.GetComponentInChildren<Text>().text =
                    "正在扫描校徽...";
                UI_FadeIn_Event();
                break;
            case 2:
                //Message1.SetActive(true);
                Message1.GetComponentInChildren<Text>().text =
                    "请移动你的手机搜索环境中的平面";
                StatusInfo.GetComponentInChildren<Text>().text =
                    "正在扫描平面...";
                UI_FadeIn_Event();
                break;
            case 3:
                //Message1.SetActive(true);
                Message1.GetComponentInChildren<Text>().text =
                    "已找到平面，请点击放置模型生成器";
                StatusInfo.GetComponentInChildren<Text>().text =
                    "点击放置...";
                UI_FadeIn_Event();
                break;
            case 4:
                //Message1.SetActive(true);
                Message1.GetComponentInChildren<Text>().text =
                    "放置完成，请进行浏览。右上角按钮可进入全景游览";
                StatusInfo.GetComponentInChildren<Text>().text =
                    " ";
                UI_FadeIn_Event();
                NextButton.gameObject.SetActive(true);
                isEnteringFull = true;
                //Loaded = true;
                break;
            case 5:
                Message1.GetComponentInChildren<Text>().text =
                     "点击寻找成功的平面放置全景模型";
                StatusInfo.GetComponentInChildren<Text>().text =
                    "点击放置...";
                UI_FadeIn_Event();
                break;
            case 6:
                Message1.GetComponentInChildren<Text>().text =
                     "放置完成，请进行浏览。右上角按钮可进入宿舍游览";
                StatusInfo.GetComponentInChildren<Text>().text =
                    " ";
                UI_FadeIn_Event();
                NextButton1.gameObject.SetActive(true);
                break;
            case 7:
                Message1.GetComponentInChildren<Text>().text =
                     "点击寻找成功的平面放置宿舍模型";
                StatusInfo.GetComponentInChildren<Text>().text =
                    "点击放置...";
                UI_FadeIn_Event();
                break;
            case 8:
                Message1.GetComponentInChildren<Text>().text =
                     "放置完成，请进行浏览。右上角按钮可进入手势识别";
                StatusInfo.GetComponentInChildren<Text>().text =
                    " ";
                UI_FadeIn_Event();
                NextSceneButton.gameObject.SetActive(true);
                break;




        }
    }

    private void LoadScene()
    {

    }

    #endregion

    // Use this for initialization
    void Start()
    {
        canvasgroup = Message1.GetComponent<CanvasGroup>();
        isEnteringFull = false;
       /* if (ARManager.Loaded == false)
        {*/
            StatusInfo.GetComponent<Text>().text = "寻找校徽中...";
        //}
        /*if (ARManager.Loaded==true)
        {
            ImageManager.SetActive(false);
            ShowMessage(5);

        }*/

    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate();
        if (Message1.GetComponent<CanvasGroup>().alpha == 1)
        {

            MessageShowTime -= Time.deltaTime;
            if (MessageShowTime <= 0)
            {
                // Message1.SetActive(false);
                UI_FadeOut_Event();
                MessageShowTime = 5;
            }
        }
    }
}
