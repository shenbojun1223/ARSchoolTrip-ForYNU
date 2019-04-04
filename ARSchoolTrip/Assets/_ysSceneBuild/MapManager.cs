using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{


    #region 地图模组动画效果

    //地图闪烁效果
    [SerializeField]
    private GameObject airScpae;

    [SerializeField]
    Material detctMat;

    //地图初始Scale
    Vector3 mapInitScale = new Vector3(1f, 0.001f, 1f);

    //建筑长出
    private IEnumerator __cFxBuildingGrow()
    {

        while (this.transform.localScale.y < 1)
        {

            yield return new WaitForSeconds(0.01f);
            this.transform.localScale += new Vector3(0f, 0.0066f, 0f) * Time.deltaTime;
        }

        //  blinkCt = StartCoroutine(__cFxMapBlink());
    }

    //private Coroutine blinkCt;
    ////地图闪烁
    //private IEnumerator __cFxMapBlink()
    //{
    //    float _value = 1f;
    //    float _step = 0f;

    //    while (true)
    //    {
    //        airScpae.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, _value);
    //        yield return new WaitForSeconds(0.05f);
    //        _step = (_step + 0.05f * 0.2f) % 1;
    //        _value = Mathf.Abs(_step - 1 / 2f) * 2f * 1 * 0.3f + 0.5f ;

    //    }

    //}

    //地图渐入
    private void __cFxMapFadeIn()
    {
        airScpae.GetComponent<SpriteRenderer>().color += new Color(1f, 1f, 1f, 0.2f) * Time.deltaTime;

    }
    #endregion


    // Use this for initialization
    void Start()
    {
        this.transform.localScale = mapInitScale;
        timer = 0f;

    }

    float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        if (detctMat.GetFloat("_Strength") < 0.9 || timer <=5f)
        {
           
            return;
        }
      
        __cFxMapFadeIn();

        if (airScpae.GetComponent<SpriteRenderer>().color.a >= 0.99f)
        {
            StartCoroutine(__cFxBuildingGrow());
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    
    }



}

