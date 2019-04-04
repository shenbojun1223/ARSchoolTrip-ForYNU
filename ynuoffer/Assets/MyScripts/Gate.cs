using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//脚本作用：播放gate的动画，并在播放完后执行调用一些函数
public class Gate : MonoBehaviour
{
    // GameObject display;
    Animator animator;
    public _DisplayHelper displayHelper;
    public GameObject fireworksPrefab;
    public GameObject soundManager;

    GameObject fireworks = null;
    void Start()
    {
        soundManager = GameObject.Find("SoundManager");
        // soundManager.GetComponent<_SoundManager>().PlaySound("fireworks1");
        // soundManager.GetComponent<AudioSource>().loop=true;
        //获取Animator组件
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //gate上升到一定位置结束
        if (transform.localPosition.y > 0.09f)
        {
            //获取动画的信息
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (!fireworks)
            {
                fireworks = GameObject.Instantiate(fireworksPrefab, new Vector3(0, 0.136f, 0.18f), Quaternion.Euler(-90, 0, 0), transform.parent);
                GenerateFireWorks();
            }
            // soundManager.GetComponent<_SoundManager>().PlaySound("fireworks1");
            // soundManager.GetComponent<AudioSource>().loop=true;
            // soundManager.GetComponent<_SoundManager>().PlaySound("fireworks1");
            // soundManager.GetComponent<AudioSource>().loop=true;

            //nomalizedTime是动画时间缩放到0-1的区间内。不是实际动画播放时长
            if (info.normalizedTime > 1.0f)
            {

                //摧毁这个物体
                Destroy(this.gameObject);
                //摧毁地面
                Destroy(GameObject.Find("Place(Clone)"));
                soundManager.GetComponent<AudioSource>().loop = false;
                Destroy(fireworks);
                displayHelper.SetupMap();
            }
            return;
        }
        //gate上升
        transform.Translate(new Vector3(0, 0.3f, 0) * Time.deltaTime);
    }

    // void PlaFireyWorks()
    // {
    //     Invoke("GenerateFireWorks", Random.Range(0.7f, 1.35f));
    // }

    void GenerateFireWorks()
    {
        fireworks = GameObject.Instantiate(fireworksPrefab, new Vector3(0, 0.136f, 0.18f), Quaternion.Euler(-90, 0, 0), transform.parent);
        Invoke("GenerateFireWorks", Random.Range(0.7f, 0.9f));
    }

}
