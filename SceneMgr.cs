using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    private bool isLoadIng = false;
    private AsyncOperation asyncOperation;

    public float publicProgress = 0;
    public float publicSpeed = 0.01f;

    private bool isLoaded = false;
    private bool lelpFin = false;
    private bool needLerp = false;
    private Action lerpFinCallBack;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (isLoadIng)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                isLoadIng = false;
                isLoaded = true;
            }
        }

        if (needLerp)
        {
            float targetValue = asyncOperation.progress;
            if (targetValue >= 0.9f)
            {
                //operation.progress的值最大为0.9
                targetValue = 1.0f;
            }

            publicProgress = Mathf.Lerp(publicProgress, targetValue, Time.deltaTime * publicSpeed);
            if (Mathf.Abs(publicProgress - targetValue) < 0.01f)
            {
                publicProgress = targetValue;
            }

            if (publicProgress == 1)
            {
                lelpFin = true;
                needLerp = false;
                ToScene();
                if (lerpFinCallBack != null)
                {
                    lerpFinCallBack();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            LoadSceneAsync("B");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            LoadSceneAsync("C");
        }
    }

    public void LoadSceneAsync(string name, Action callBack = null)
    {
        if (!isLoadIng && !needLerp)
        {
            isLoaded = false;
            publicProgress = 0;
            isLoadIng = true;
            lelpFin = false;
            needLerp = true;
            StartCoroutine(LoadIE(name));
            lerpFinCallBack = callBack;
        }
    }
    private IEnumerator LoadIE(string name, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        asyncOperation = SceneManager.LoadSceneAsync(name, loadSceneMode);
        asyncOperation.allowSceneActivation = false;
        yield return asyncOperation;
    }
    public void ToScene()
    {
        if (isLoaded)
        {
            asyncOperation.allowSceneActivation = true;
            isLoaded = false;
            lelpFin = false;
            publicProgress = 0;
            isLoadIng = false;
        }
    }
}
