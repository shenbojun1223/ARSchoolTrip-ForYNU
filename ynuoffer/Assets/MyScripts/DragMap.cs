using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

//脚本作用：使得用户可以移动地图
public class DragMap : MonoBehaviour
{
    // public Text text;
    public Camera ARCamera;
    public float speed = 0.001F;
    private Vector3 originalHitPosition;
    private Vector3 originalLocalMapPosition;
    private Transform mapTransform;
    private Transform parentRectTransform;
    bool hasClicked;
    void OnEnable() {
        mapTransform = transform;
        // parentRectTransform = mapTransform.parent as RectTransform;
    }
    void OnDisable() {
        hasClicked=false;
    }
    void Update()
    {
        //update中的代码是处理 触摸移动地图的


        if (Input.touchCount == 1)
        {
            if (hasClicked && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // text.text += "in moving;";
                if (mapTransform == null)
                    return;
                // transform.ro
                RaycastHit hit;
                Ray ray = ARCamera.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(ray, out hit))
                {
                    // text.text += "hit:";
                    if (hit.collider.name == "Campus")
                    {
                        // text.text += "hit the map in moving;";

                        Vector3 offsetToOriginal = hit.point - originalHitPosition;
                        mapTransform.localPosition = originalLocalMapPosition + new Vector3(offsetToOriginal.x, 0,  offsetToOriginal.z);
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // text.text += "Began;";
                originalLocalMapPosition = mapTransform.localPosition;
                RaycastHit hit;
                Ray ray = ARCamera.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(ray, out hit))
                {
                    // text.text += "hit;";
                    if (hit.collider.name == "Campus")
                    {
                        hasClicked=true;
                        // text.text += "hit the map Began;";
                        originalHitPosition = hit.point;
                    }
                }
            }
        }
    }
    //这个是处理鼠标点击的
    void OnMouseDown()
    {

        // Debug.Log("!!!");
        // originalLocalMapPosition = mapTransform.localPosition;
        originalLocalMapPosition = mapTransform.localPosition;
        RaycastHit hit;
        Ray ray = ARCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("map"))
            {
                hasClicked=true;
                originalHitPosition = hit.point;
            }
        }
    }
    //这个是处理鼠标移动的
    void OnMouseDrag()
    {
        if (!hasClicked || mapTransform == null )
            return;
        RaycastHit hit;
        Ray ray = ARCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("map"))
            {
                // originalHitPosition = hit.point;
                Vector3 offsetToOriginal = hit.point - originalHitPosition;

                mapTransform.localPosition = originalLocalMapPosition + new Vector3(offsetToOriginal.x, 0, offsetToOriginal.z);
            }
        }
    }
}
