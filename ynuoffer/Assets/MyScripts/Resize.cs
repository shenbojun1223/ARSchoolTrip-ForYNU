using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//脚本作用：在移动端 缩放物体
public class Resize : MonoBehaviour {

    Vector2 oldPos1;
    Vector2 oldPos2;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                Vector2 temPos1 = Input.GetTouch(0).position;
                Vector2 temPos2 = Input.GetTouch(1).position;

                if (isEnLarge(oldPos1, oldPos2, temPos1, temPos2))
                {
                    float oldScale = transform.localScale.x;
                    float newScale = oldScale * 1.025f;

                    transform.localScale = new Vector3(newScale, newScale, newScale);
                }
                else
                {
                    float oldScale = transform.localScale.x;
                    float newScale = oldScale / 1.025f;
                    transform.localScale = new Vector3(newScale, newScale, newScale);
                }

                oldPos1 = temPos1;
                oldPos2 = temPos2;
            }
        }
    }

    //判断手势
    bool isEnLarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        float length1 = Mathf.Sqrt(Mathf.Pow((oP1.x - oP2.x), 2.0f) + Mathf.Pow((oP1.y - oP2.y), 2.0f));
        float length2 = Mathf.Sqrt(Mathf.Pow((nP1.x - nP2.x), 2.0f) + Mathf.Pow((nP1.y - nP2.y), 2.0f));

        if (length1 < length2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
