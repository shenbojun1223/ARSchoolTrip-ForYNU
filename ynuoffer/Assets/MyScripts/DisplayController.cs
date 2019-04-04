using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;


//这个脚本主要用于控制一些全局的UI的开关
public class DisplayController : MonoBehaviour
{
    public Button toSandboxBtn;
    // public Button Close;
    public Camera ARCamera;

    public GameObject sandbox;
    public GameObject show;
    public GameObject[] buildingPrefabs;
    public string[] buildingNames;
    public bool hasMovedout;//是否脱卡
    public bool guide;
    void Start()
    {
        // Close.onClick.AddListener(()=>{
        //     Application.Quit();
        // });
        // buildingNames = new string[3] { "SchoolHistoryMuseum", "Library" ,""};
        // Debug.Log(buildingNames.Length);
        toSandboxBtn.onClick.AddListener(delegate ()
        {
            show.GetComponent<BuildingShow>().BuildingNode.transform.localScale=Vector3.one;//建筑物缩放重置
            show.GetComponent<BuildingShow>().BuildingNode.GetComponent<BuildingController>().bubble.SetActive(false);
            ResetDisplay();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void ResetDisplay()
    {
        //显示地图，关闭返回按钮，关闭建筑物介绍界面。

        toSandboxBtn.gameObject.SetActive(false);
        sandbox.SetActive(true);
        if(!guide)
        {

            guide=true;
        }
        show.SetActive(false);
    }

    public void CloseAll(bool close)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        toSandboxBtn.gameObject.SetActive(false);
        // if(close)
        //     Close.gameObject.SetActive(false);
    }


    public void ShowBuildingBtn(GameObject btn)
    {
        //建筑物上方的按钮点击后调用这个函数
        //点击查看建筑物。
        sandbox.SetActive(false);
        toSandboxBtn.gameObject.SetActive(true);
        show.SetActive(true);
        int index = Array.IndexOf(buildingNames, btn.tag);
        if (index != -1)
        {
            sandbox.SetActive(false);
            toSandboxBtn.gameObject.SetActive(true);
            show.SetActive(true);
            BuildingShow showComponent = show.GetComponent<BuildingShow>();
            Transform buildingNode = showComponent.BuildingNode.transform;
            for (int i = 0; i < buildingNode.childCount; i++)
            {
                Destroy(buildingNode.GetChild(0).gameObject);
            }
            GameObject building = GameObject.Instantiate(buildingPrefabs[index],Vector3.zero, Quaternion.Euler(0, 0, 0), buildingNode);

            // building.GetComponent<Scaling>().factor = 10;
            building.transform.localPosition = new Vector3(0,0.0f,0);
            switch (btn.tag)
            {
                case "SchoolHistoryMuseum":
                    // building.GetComponent<Scaling>().factor = 10;
                    showComponent.BuildingName = "校史馆";
                    showComponent.BuildingIntroduce = "云南大学于1992年成立档案馆，2005年12月学校当时校史研究室并入。" + "\n" +
                        "截至2016年底，共有直至档案103057卷，855958件，排列长度1679米，资料1164册，学籍卡片64000余张，所为胶片100盘，卷片长1189米，还有录音、录像带、光盘和2450件有保存价值的实物。";
                    break;
                case "Library":
                    building.GetComponent<Scaling>().factor = 6.55f;
                    showComponent.BuildingName = "图书馆";
                    showComponent.BuildingIntroduce = "云南大学图书馆始建于1923年，由校本部图书馆和呈贡校区图书馆两个部分组成，总面积53975平方米，总藏书量为2511748册，另有电子体图书433369册，"+
                        "自制电子图书327册，中文数据库5个，外文数据库10个，缩微资料511件。";
                    break;
                case "MingYuan":
                    showComponent.BuildingName="明远楼";
                    showComponent.BuildingIntroduce="云南大学呈贡校区明远楼为云南大学呈贡校区办公楼，占地面积为27485.5平方米，有地上5层、地下1层，气势恢宏，庄严肃穆，与图书馆、学生会堂同为校区标志性建筑。";
                    break;
                case "belfry":
                    showComponent.BuildingName="钟楼";
                    showComponent.BuildingIntroduce="1955年兴建配套工程水塔兼作钟楼。塔共七层，高26米，连塔顶钢架共高20米。\n"+
                        "“钟楼接回”为云南大学校园一景。\n"+"2005年列为昆明市历史文化遗产保护建筑。";
                    break;
                case "dormitry":
                    showComponent.BuildingName="宿舍";
                    showComponent.BuildingIntroduce="四人一间，独立卫浴";
                    break;
                default:
                    break;
            }

            //去掉下面这个for循环，物体的旋转可能会出问题。
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localRotation=Quaternion.identity;
            }
            building.transform.localRotation=Quaternion.Euler(0,0,0);
        }
    }
}
