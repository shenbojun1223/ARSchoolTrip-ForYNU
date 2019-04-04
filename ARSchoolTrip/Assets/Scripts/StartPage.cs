using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour {

    void Start()
    {

    }

    void Update()
    {
        if (Input.touchCount == 1)
            if (Input.touches[0].phase == TouchPhase.Began)
                SceneManager.LoadScene("ARCore");
    }
}
