using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTap : MonoBehaviour
{

    // Use this for initialization
    private float touchTime;
    private bool newTouch = false;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;


            if (Physics.Raycast(ray, out hitInfo))
            {
                // if(hitInfo.collider.gameObject.name=="AiXi(Clone)")
                // {
                // 	Destroy(hitInfo.collider.gameObject);
                // }

                //双击
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if (Input.GetTouch(0).tapCount == 2)
                    {
                        Destroy(hitInfo.collider.gameObject);
                    }
                }

                //长按
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        newTouch = true;
                        touchTime = Time.time;
                    }
                    else if (touch.phase == TouchPhase.Stationary)
                    {
                        if (newTouch == true && Time.time - touchTime > 1f)
                        {
                            newTouch = false;
                            Destroy(hitInfo.collider.gameObject);
                        }
                    }
					else{
						newTouch=false;
					}
                }
            }



        }

    }
}
