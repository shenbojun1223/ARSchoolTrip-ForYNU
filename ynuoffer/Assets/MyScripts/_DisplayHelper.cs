using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//该类用于处理显示哪些UI和物体，关闭哪些UI和物体，和显示是什么。

public class _DisplayHelper : MonoBehaviour
{
    public Camera ARCamera;
    public GameObject imageTarget;
    public GameObject histroyShow;
    public _UIManager UIManager;
    public _SoundManager soundManager;
    public GameObject display;
    public _BuildingHelper helper;
    public GameObject buildingShow;
    private GameObject show_instance;
    public bool isMovedOut;

    private GameObject campus;
    public void OnMoveOut()
    {
		isMovedOut = true;
        //脱卡后执行
        display.transform.Find("Campus").GetComponent<DragMap>().enabled = false;
        // display.SetActive(false);


        //以下设置transform只是为了方便观察，放置到一个易于观察的位置。

        display.transform.parent = ARCamera.transform;
        display.transform.localPosition = new Vector3(0.0f, -0.1f, 1.1f);
        display.transform.localRotation = Quaternion.Euler(-25, 0, 0);
        // display.transform.Find("Campus").localRotation=Quaternion.Euler(-65, 0, 0);

        foreach (var button in display.transform.Find("Campus").GetComponentsInChildren<Button>())
        {
            //将按钮面向摄像机
            button.gameObject.GetComponent<BuildingBtn>().enabled = false;
            button.gameObject.transform.localRotation = Quaternion.identity;
            button.transform.GetChild(0).transform.localRotation = Quaternion.identity;
            // button.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 180, 0);

        }
        // display.SetActive(true);
		ToggleDragMap();
        // Invoke("ToggleDragMap", 0.1f);
    }

    public void OnMoveIn()
    {
        //扫描到时执行
        isMovedOut = false;
        foreach (var button in display.transform.Find("Campus").GetComponentsInChildren<Button>())
        {
            //将按钮面向摄像机
            button.gameObject.GetComponent<BuildingBtn>().enabled = true;
            // button.transform.GetChild(0).transform.localRotation = Quaternion.identity;
			button.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 180, 0);

        }
        display.transform.Find("Campus").GetComponent<DragMap>().enabled = false;
        // display.SetActive(false);
        display.transform.SetParent(imageTarget.transform, true);
        // display.transform.parent = imageTarget.transform;
        display.transform.localPosition = Vector3.zero;
        display.transform.localRotation = Quaternion.identity;
        // display.SetActive(true);
        // Invoke("ToggleDragMap", 0.1f);
		ToggleDragMap();
    }

    void ToggleDragMap()
    {
        Transform campus = display.transform.Find("Campus");
        campus.localPosition = Vector3.zero;
        campus.GetComponent<DragMap>().enabled = true;
    }

    public void BuildingBtnClick(List<GameObject> btns)
    {
        //这个函数在建筑的按钮点击时调用
        string[] keys = new string[btns.Count];
        for (int i = 0; i < btns.Count; i++)
        {
            keys[i] = btns[i].tag;
        }
        foreach (GameObject btn in btns)
        {
            if (isMovedOut)
            {
                btn.GetComponent<BuildingBtn>().enabled = false;
                foreach (var button in display.transform.Find("Campus").GetComponentsInChildren<Button>())
                {
                    //将按钮面向摄像机
                    button.gameObject.GetComponent<BuildingBtn>().enabled = false;
                    button.transform.GetChild(0).transform.localRotation = Quaternion.identity;
                }
            }
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                soundManager.GetComponent<_SoundManager>().PlaySound("ButtonClick");

                display.transform.Find("Campus").GetComponent<Collider>().enabled = false;
                _MapManager.CloseMap();
                show_instance = GameObject.Instantiate(buildingShow, Vector3.zero, Quaternion.identity, display.transform);
                show_instance.transform.localPosition = new Vector3(0, 0.01f, 0.01751f);
                show_instance.transform.localRotation = Quaternion.identity;
                BuildingShow showComponent = show_instance.GetComponent<BuildingShow>();
                GameObject building = GameObject.Instantiate(helper.tag_prefab[btn.tag], Vector3.zero, Quaternion.identity,
                    show_instance.GetComponent<BuildingShow>().BuildingNode.transform);
                building.transform.localPosition = Vector3.zero;
                building.transform.localRotation = Quaternion.identity;
                string choice;
                if(btn.tag.StartsWith("dormitry"))
                    choice="dormitry";
                else
                    choice=btn.tag;
                Debug.Log(choice);
                switch (choice)
                {
                    case "SchoolHistoryMuseum":
                        showComponent.BuildingName = "校史馆";
                        showComponent.BuildingIntroduce = "云南大学于1992年成立档案馆，2005年12月学校当时校史研究室并入。" + "\n" +
                            "截至2016年底，共有直至档案103057卷，855958件，排列长度1679米，资料1164册，学籍卡片64000余张，所为胶片100盘，卷片长1189米，还有录音、录像带、光盘和2450件有保存价值的实物。";
                        break;
                    case "Library":
                        building.GetComponent<Scaling>().factor = 6.55f;
                        showComponent.BuildingName = "图书馆";
                        showComponent.BuildingIntroduce = "云南大学图书馆始建于1923年，由校本部图书馆和呈贡校区图书馆两个部分组成，总面积53975平方米，总藏书量为2511748册，另有电子体图书433369册，" +
                            "自制电子图书327册，中文数据库5个，外文数据库10个，缩微资料511件。";
                        break;
                    case "MingYuan":
                        showComponent.BuildingName = "明远楼";
                        showComponent.BuildingIntroduce = "云南大学呈贡校区明远楼为云南大学呈贡校区办公楼，占地面积为27485.5平方米，有地上5层、地下1层，气势恢宏，庄严肃穆，与图书馆、学生会堂同为校区标志性建筑。";
                        break;
                    case "belfry":
                        showComponent.BuildingName = "钟楼";
                        showComponent.BuildingIntroduce = "1955年兴建配套工程水塔兼作钟楼。塔共七层，高26米，连塔顶钢架共高20米。\n" +
                            "“钟楼接回”为云南大学校园一景。\n" + "2005年列为昆明市历史文化遗产保护建筑。";
                        break;
                    case "dormitry":
                        showComponent.BuildingName = "宿舍";
                        showComponent.BuildingIntroduce = "四人一间，独立卫浴";
                        break;
                    default:
                        break;
                }
                UIManager.RemoveAndDestroy(keys);
                UIManager.Toggle("ReturnBtn");
                UIManager.Toggle("ResetBtn");
                histroyShow.SetActive(false);
            });
        }
    }


    public void SetupMap()
    {
        //这个函数在建筑介绍界面返回到地图界面时调用
        display.transform.Find("Campus").GetComponent<Collider>().enabled = true;
        if (show_instance)
        {
            Destroy(show_instance);
        }
        GetComponent<_MapManager>().SetupScene();
        histroyShow.SetActive(true);
    }

    public void ReSetupMap(GameObject btn)
    {
        SetupMap();
        soundManager.GetComponent<_SoundManager>().PlaySound("ButtonClick");
        btn.SetActive(false);

    }
    // public void ReSetupMap(GameObject btn)
    // {
    // 	soundManager.GetComponent<_SoundManager>().PlaySound("ButtonClick");
    // 	//这个函数在建筑介绍界面返回到地图界面时调用

    //     display.transform.Find("Campus").GetComponent<Collider>().enabled = true;
    //     if (show_instance)
    //     {
    //         Destroy(show_instance);
    //     }
    //     GetComponent<_MapManager>().SetupScene();
    //     histroyShow.SetActive(true);
    //     if (btn)
    //         btn.SetActive(false);
    // }

    public void OnResetMap()
    {
        soundManager.GetComponent<_SoundManager>().PlaySound("ButtonClick");
        //重置按钮
        display.transform.Find("Campus").localPosition = Vector3.zero;
        display.transform.Find("Campus").localScale = Vector3.one;
    }
}
