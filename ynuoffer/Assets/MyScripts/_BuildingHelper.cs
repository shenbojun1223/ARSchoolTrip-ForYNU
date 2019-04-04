using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//该脚本用于获得 建筑物对象和建筑物标签组成的键值对dictionary

public class _BuildingHelper : MonoBehaviour
{
    public pair[] buildingPrefabs;
    public Dictionary<string, GameObject> tag_prefab = new Dictionary<string, GameObject>();

    public static Dictionary<string, string> EN2ZH = new Dictionary<string, string>{
    {"SchoolHistoryMuseum","校史馆"},
    {"belfry","钟楼"},
    {"MingYuan","明远楼"},
    {"Library","图书馆"},
    {"dormitry","宿舍"},
    {"dormitry_zi","宿舍"},
    {"dormitry_hua","宿舍"},
    {"dormitry_qiu","宿舍"},
    {"dormitry_nan","宿舍"}
    };

    [System.Serializable]
    public struct pair
    {
        public string tag;
        public GameObject prefab;
    }

    void Awake()
    {
        for (int i = 0; i < buildingPrefabs.Length; i++)
        {
            if (!tag_prefab.ContainsKey(buildingPrefabs[i].tag))
            {
                tag_prefab.Add(buildingPrefabs[i].tag, buildingPrefabs[i].prefab);
            }
        }
    }
}
