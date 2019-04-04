using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLoadingPanel : MonoBehaviour {
    static string NextScene;     //下一场景
    AsyncOperation async;
    public Slider LoadingBar;
    public Text text;
    public float CurrentProgress;
    public float m_timer = 0;
    public void LoadingScene(string NextSceneName)
    {
        NextScene = NextSceneName;
        SceneManager.LoadScene("LoadingScene");    
    }
    public void LoadingStartScene(string StartScene)
    {
        NextScene = StartScene;
        SceneManager.LoadScene("LoadingScene");
    }
	// Use this for initialization
	void Start () {
        CurrentProgress = 0;
        if (SceneManager.GetActiveScene().name == "LoadingScene")
        {
            async=SceneManager.LoadSceneAsync(NextScene);
            async.allowSceneActivation = false;
        }

	}
	
	// Update is called once per frame
	void Update () {
		if(text&&LoadingBar)
        {
            CurrentProgress = Mathf.Lerp(CurrentProgress, async.progress, Time.deltaTime);
            text.text = ((int)(CurrentProgress / 9*10* 100)).ToString() + "%";
            LoadingBar.value = CurrentProgress / 9*10;
            if(CurrentProgress/9*10>=0.998)
            {
                Debug.Log("text");
                text.text = 100.ToString() + "%";
                LoadingBar.value = 1;

                m_timer += Time.deltaTime;
                if(m_timer>=1)
                {
                    async.allowSceneActivation = true;
                }

            }       
        }
	}
}
