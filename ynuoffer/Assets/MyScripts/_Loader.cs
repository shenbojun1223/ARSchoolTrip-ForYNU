using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Loader : MonoBehaviour {
	public GameObject appManager;
    public GameObject UIManager;

    void Awake()
    {
        if(_AppManager.instance==null)
        {
            Instantiate(appManager);
        }
        if(_UIManager.instance==null)
        {
            Instantiate(UIManager);
        }
    }
}