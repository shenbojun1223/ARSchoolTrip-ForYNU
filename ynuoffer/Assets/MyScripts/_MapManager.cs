using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _MapManager : MonoBehaviour
{
    public Camera ARCamera;
    // public _DisplayHelper displayHelper;

    public GameObject campus;
    public GameObject map;
    public pair[] buildingPosition_list;
    private static Dictionary<string, Transform> buildingPosition_dict = new Dictionary<string, Transform>();
    public _UIManager UIManager;
    public  static GameObject map_instance;    

    // private Dictionary<string, string> EN2ZH = new Dictionary<string, string>{
    //     {"SchoolHistoryMuseum","校史馆"},
    //     {"belfry","钟楼"},
    //     {"MingYuan","明远楼"},
    //     {"Library","图书馆"},
    //     {"dormitry","宿舍"},
    //     {"dormitry_zi","宿舍"},
    //     {"dormitry_hua","宿舍"},
    //     {"dormitry_qiu","宿舍"},
    //     {"dormitry_nan","宿舍"}
    //     };

    [System.Serializable]
    public struct pair
    {
        public string name;
        public Transform pos;
    }
    void MapSetup()
    {
        map_instance = Instantiate(map);
        map_instance.transform.SetParent(campus.transform, false);

        for (int i = 0; i < buildingPosition_list.Length; i++)
        {
            if (!buildingPosition_dict.ContainsKey(buildingPosition_list[i].name))
            {
                buildingPosition_dict.Add(buildingPosition_list[i].name, buildingPosition_list[i].pos);
            }
        }
    }
    List<GameObject> LayoutButton()
    {
        GameObject btn;
        List<GameObject> btns=new List<GameObject>();
        foreach (KeyValuePair<string, Transform> kv in buildingPosition_dict)
        {
            btn = UIManager.Create("BuildingButton", kv.Key);
            btn.transform.SetParent(kv.Value.transform, false);
            btn.transform.localPosition = new Vector3(0, 65, 0);
            // btn.transform.GetChild(0).localRotation=Quaternion.Euler(0,180,0);
            btn.GetComponent<BuildingBtn>().BuildingName = _BuildingHelper.EN2ZH[kv.Key];
            btn.GetComponent<BuildingBtn>().ARCamera = ARCamera;
            // if(kv.Key.StartsWith("dormitry"))
            //     btn.tag = "dormitry";
            // else
            //     btn.tag =kv.Key;
            btn.tag =kv.Key;
            btns.Add(btn);
        }
        return btns;
    }
    public void SetupScene()
    {
        // Debug.Log("!!!");
        MapSetup();
        List<GameObject> btns=LayoutButton();
        GetComponent<_DisplayHelper>().BuildingBtnClick(btns);
        UIManager.Toggle("ResetBtn");
        // SendMessage("BuildingBtnClick",btns);
    }
    public static void CloseMap()
    {
        Destroy(map_instance);
        buildingPosition_dict.Clear();
    }
}
