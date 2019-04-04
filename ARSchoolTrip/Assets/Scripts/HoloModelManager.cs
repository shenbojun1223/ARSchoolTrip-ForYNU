using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoloModelManager : MonoBehaviour {

    #region UIevents

    public Button fBtn_Fill;
    public Button fBtn_Switch;

    #endregion

    public GameObject resume;
    public GameObject buildingModel;
    public GameObject allMap;
    public GameObject bigMap;
    MeshRenderer meshRenderer;

    [SerializeField]
    public Material matHolo;

    [SerializeField]
    Material matRaw;

    Material[] mats = new Material[2];
       

    //旋转FX
    private bool _rotateFX = false;
    private void __BuildingRotate()
    {
        if (_rotateFX)
        {
            displayMList[curDispIndex].transform.eulerAngles += new Vector3(0f, Time.deltaTime * 5f, 0f);
           
        }
      
    }



    //材质FX
    private float _colorStrenth=0f;

    public void __BuildingMatFx_GetTouched()
    {
        //When get touched
        StartCoroutine(__cFxTurnLight());
    }

    private IEnumerator __cFxTurnLight()
    {
        while (_colorStrenth < 6)
        {
            yield return new WaitForSeconds(0.1f);
            _colorStrenth += 0.3f;
            matHolo.SetFloat("_Strength", _colorStrenth);
        }

        //添加双材质
        foreach (Transform child in buildingModel.transform)
        {
            child.gameObject.GetComponent<SpeciOwnMatManager>().setMatAsOwn();
        }
  

        StartCoroutine(__cFxFadeOut());
    }

    private IEnumerator __cFxFadeOut()
    {
        while (_colorStrenth > 0)
        {
            yield return new WaitForSeconds(0.2f);
            _colorStrenth = (_colorStrenth > 0.15f) ? _colorStrenth - 0.15f : 0f;
            matHolo.SetFloat("_Strength", _colorStrenth);
        }
    }

 

    #region 底座出现 地图出现 然后
    public GameObject lightFx;

    [SerializeField]
    Material matResume;

    private float rV;
    private void __AnimeInit()
    {
        rV = 0;
        //将底座变为隐形
        matResume.SetFloat("_Strength", rV);
        StartCoroutine(__cFxResumeFadeIn());

    }

    private IEnumerator __cFxResumeFadeIn()
    {
        while (rV < 0.9)
        {
            yield return new WaitForSeconds(0.05f);
            rV += 0.05f;
            matResume.SetFloat("_Strength", rV);
        }

        StartCoroutine(__cFxLightOn());
    }

    private IEnumerator __cFxLightOn()
    {
        lightFx.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        lightFx.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        lightFx.SetActive(true);
        yield return new WaitForSeconds(0.15f);

        startOtherFx();


    }


    private void startOtherFx()
    {
        foreach (Transform child in buildingModel.transform)
        {
            child.gameObject.GetComponent<MeshRenderer>().material = matHolo;
        }


      

        //开始旋转
        _rotateFX = true;
     
     //   buildingModel.GetComponent<MapManager>().allowStart = true;

    }



    #endregion

    #region 建筑轮播块
    public List<GameObject> createList;
    public List<GameObject> displayMList;
    public GameObject displayGroup;

    private int curDispIndex = 0;

    private void __InitBuildingDisplay()
    {
        Vector3[] arrayPos = new Vector3[9];
        arrayPos[0] = new Vector3(-35.2f, 25f, -36.5f);
        arrayPos[1] = new Vector3(-41.6f, 25f, -37.8f);
        arrayPos[2] = new Vector3(-46.8f, 25f, -20.1f);
        arrayPos[3] = new Vector3(-36.6f, 25f, -20.6f);
        arrayPos[4] = new Vector3(-39.2f, 25f, -50.1f);
        arrayPos[5] = new Vector3(-36.3f, 25f, -37.5f);
        arrayPos[6] = new Vector3(-53.2f, 25f, -29.4f);
        arrayPos[7] = new Vector3(-62.7f, 25f, -34.7f);
        

        curDispIndex = 8;
        for (int i = 0; i < 8; i++)
        {//displayGroup.transform.position,Quaternion.identity,
            GameObject dBuilding= Instantiate(createList[i], displayGroup.transform,false);
            displayMList.Add(dBuilding);
            dBuilding.transform.localPosition = arrayPos[i];
            dBuilding.SetActive(false);
        }
        displayMList.Add(allMap);
    }

    private void __ShippingList()
    {

    }

    private void __SwitchDisplaying()
    {
        displayMList[curDispIndex].SetActive(false);
       
        curDispIndex++;
        curDispIndex = curDispIndex % displayMList.Count;

        displayMList[curDispIndex].SetActive(true);

       
       
    }

 
    #endregion


    // Use this for initialization
    void Start () {


        __InitBuildingDisplay();

        fBtn_Fill = GameObject.Find("FuntionButton").GetComponent<Button>();
        fBtn_Fill.onClick.AddListener(__BuildingMatFx_GetTouched);
        fBtn_Switch = GameObject.Find("FuntionButton_switch").GetComponent<Button>();
        fBtn_Switch.onClick.AddListener(__SwitchDisplaying);

        //材质初始化
        matHolo.SetColor("_Color", new Color(0.2588235f, 0.4470588f, 0.8196079f, 1f));
        matHolo.SetFloat("_Strength", 1.7f);
        _colorStrenth = 0f;

        //双材质添加
        mats[0] = matHolo;
        mats[1] = matRaw;
        __AnimeInit();
    }

	
	// Update is called once per frame
	void Update () {
        __BuildingRotate();
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("terer");
            __BuildingMatFx_GetTouched();
        }
    }
}


