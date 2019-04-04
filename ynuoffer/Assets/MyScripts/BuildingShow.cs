using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class BuildingShow : MonoBehaviour
{
    // Use this for initialization
    public Camera ARCamera;
    // public
    [SerializeField] GameObject buildingNode;//建筑模型所在的节点
    // [SerializeField] GameObject buildingInfo;
    [SerializeField] Text buildingName;//建筑名
    [SerializeField] Text buildingIntroduce;//建筑介绍信息
    
    public GameObject BuildingNode
    {
        get { return buildingNode; }
    }
    // public GameObject BuildingInfo
    // {
    //     get { return buildingInfo; }
    // }
    public string BuildingIntroduce
    {
        set { buildingIntroduce.text = value; }
    }
    public string BuildingName
    {
        set { buildingName.text = value; }
    }
}
