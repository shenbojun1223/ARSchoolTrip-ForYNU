using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _UIManager : MonoBehaviour
{

	public static _UIManager instance=null;
    public pair[] ui_prefabs_list;
    public pair[] ui_instances_list;
    public Dictionary<string, GameObject> ui_prefabs = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> ui_instances = new Dictionary<string, GameObject>();
    [System.Serializable]
    public struct pair
    {
        public string name;
        public GameObject ui;
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        for (int i = 0; i < ui_prefabs_list.Length; i++)
        {
            if (!ui_prefabs.ContainsKey(ui_prefabs_list[i].name))
            {
                ui_prefabs.Add(ui_prefabs_list[i].name, ui_prefabs_list[i].ui);
            }
        }
        for (int i = 0; i < ui_instances_list.Length; i++)
        {
            if (!ui_instances.ContainsKey(ui_instances_list[i].name))
            {
                ui_instances.Add(ui_instances_list[i].name, ui_instances_list[i].ui);
            }
        }
    }

    public void Toggle(string name)
    {
        if (ui_instances[name] != null)
        {
            ui_instances[name].SetActive(!ui_instances[name].activeSelf);
        }
    }
    public void Toggle(string[] names)
    {
        foreach (var name in names)
        {
            if (ui_instances[name] != null)
            {
                ui_instances[name].SetActive(!ui_instances[name].activeSelf);
            }
        }
    }

    public void Add(string key,GameObject value)
    {
        // KeyValuePair<string, GameObject> kv=new KeyValuePair<string, GameObject>(key,value);
        ui_instances.Add(key, value);
    }

    public GameObject Create(string key,string tag)
    {
        GameObject ui=Instantiate(ui_prefabs[key],Vector3.zero,Quaternion.identity);
        ui_instances.Add(tag,ui);
        return ui;

    }
    public void RemoveAndDestroy(string[] keys)
    {
        foreach(string key in keys)
        {
            Destroy(ui_instances[key]);
            ui_instances.Remove(key);
        }
    }
}
