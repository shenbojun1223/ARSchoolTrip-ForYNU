using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//调整一个参数，缩放物体
[ExecuteInEditMode]
public class Scaling : MonoBehaviour
{
    public float factor = 10000;
    // Update is called once per frame
    void Update()
    {
        if (this.transform.localScale.x != factor)
        {
            this.transform.localScale =Vector3.one* factor;
        }
    }
}
